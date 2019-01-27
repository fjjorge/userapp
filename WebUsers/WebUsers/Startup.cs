using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebUsers.Managers;
using WebUsers.Models;
using WebUsers.Services;

namespace WebUsers
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
            services.AddDbContext<WebUserContext>(opt => opt.UseInMemoryDatabase(databaseName: "users"), ServiceLifetime.Singleton);
            services.AddSingleton<UserService>();
            services.AddSingleton<UsersManager>();
            services.AddSingleton<ExternalUserDataService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            var context = app.ApplicationServices.GetService<WebUserContext>();
            var externalUserDataService = app.ApplicationServices.GetService<ExternalUserDataService>();
            externalUserDataService.AddTestData();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
