﻿using Data.DataProviders;
using Data.DataProviders.Players;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Web.Hubs;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommon(services);

            //Load data providers with physical DB connections.
            var gameDataConnectionString = Configuration["ConnectionStrings:GameData"];
            services.AddEntityFrameworkNpgsql().AddDbContextPool<GameDataContext>(options => options.UseNpgsql(gameDataConnectionString));
            services.AddTransient<IPlayerDataProvider, PersistentPlayerData>();

        }
        
        public void ConfigureMockService(IServiceCollection services)
        {
            ConfigureCommon(services);
            //Mock data providers need no setup
            services.AddTransient<IPlayerDataProvider, MockedPlayerData>();
        }

        private void ConfigureCommon(IServiceCollection services)
        {
            services.AddSingleton<GameWrapper, GameWrapper>();
            services.AddTransient<PlayerRepository, PlayerRepository>();
            services.AddSignalR()
                    .AddJsonProtocol(options => {
                        options.PayloadSerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    });

            services.AddSession();
            services.AddMvc()
                    .AddSessionStateTempDataProvider();

            services.AddCors(options => 
                options.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins("http://localhost:55830")
                           .AllowCredentials();
                })
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //TODO: Gremlins have added code here. Investigate if we need to purge it.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
            app.UseWebSockets();
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<PlayerHub>("/PlayerHub");
                routes.MapHub<GameHub>("/GameHub");
                routes.MapHub<MonsterHub>("/MonsterHub");
                routes.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
