using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Web.Hubs;
using Data.DataProviders.MySqlHelpers;
using Data.Repositories;
using Data.DataProviders.Players;

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
            services.AddSingleton<GameWrapper, GameWrapper>();

            //Load data providers with physical DB connections.
            var gameDataConnectionString = Configuration["ConnectionStrings:GameData"];
            services.AddTransient<IPlayerDataProvider, PersistentPlayerData>(x => new PersistentPlayerData(new MySqlDb(gameDataConnectionString)));

            services.AddSignalR(option =>
            {
                option.JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSession();
            services.AddMvc()
                    .AddSessionStateTempDataProvider();
        }

        public void ConfigureMockService(IServiceCollection services)
        {
            //Mock data providers need no setup
            services.AddTransient<IPlayerDataProvider, MockedPlayerData>(x => new MockedPlayerData());
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
            app.UseSignalR(routes =>
            {
                routes.MapHub<PlayerHub>("PlayerHub");
                routes.MapHub<GameHub>("GameHub");
                routes.MapHub<MonsterHub>("MonsterHub");
                routes.MapHub<ChatHub>("ChatHub");
            });
        }
    }
}
