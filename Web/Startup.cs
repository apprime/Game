using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TEMP
            //services.AddSingleton<IPostRepository, PostRepository>();
            // END TEMP
            services.AddSingleton<GameWrapper, GameWrapper>();

            services.AddSignalR(option =>
            {
                option.JsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Add framework services.
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
