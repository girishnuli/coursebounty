using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using coursebounty.Models;
using coursebounty.Services;
using AspNetCore.Identity.LiteDB.Data;
using AspNetCore.Identity.LiteDB;
using LiteDbModels = AspNetCore.Identity.LiteDB.Models;
using LiteDB;

namespace coursebounty
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment _env { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add LiteDB Dependency
            services.AddSingleton<LiteDbContext>(db => 
            new LiteDbContext(_env) {
                LiteDatabase = new LiteDatabase(Configuration.GetConnectionString("DefaultConnection"))
            });

            services.AddIdentity<LiteDbModels.ApplicationUser, AspNetCore.Identity.LiteDB.IdentityRole>(options =>
                            {
                                options.Password.RequireDigit = false;
                                options.Password.RequireUppercase = false;
                                options.Password.RequireLowercase = false;
                                options.Password.RequireNonAlphanumeric = false;
                                options.Password.RequiredLength = 6;
                            })
            .AddUserStore<LiteDbUserStore<LiteDbModels.ApplicationUser>>()
            .AddRoleStore<LiteDbRoleStore<AspNetCore.Identity.LiteDB.IdentityRole>>()
            .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _env = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
