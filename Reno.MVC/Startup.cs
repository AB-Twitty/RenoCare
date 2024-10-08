using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reno.MVC.Helpers;
using Reno.MVC.Helpers.Contracts;
using Reno.MVC.Services.Base;
using Reno.MVC.Services.Base.Contracts;
using RenoCare.MVC.Middlewares;
using System;

namespace Reno.MVC
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
            services.AddSingleton<ILocalStorageService, LocalStorageService>();

            services.AddHttpClient<IClient, Client>(client =>
            {
                //https://renocareapi.azurewebsites.net
                //http://localhost:6982/
                client.BaseAddress = new Uri("https://renocareapi.azurewebsites.net/");
            });

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options => options.MinimumSameSitePolicy = SameSiteMode.None);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/Login";
                    config.AccessDeniedPath = "/Login";
                });

            services.AddControllersWithViews();

            services.AddTransient<IDataTableInfoExtractor, DataTableExtractor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
