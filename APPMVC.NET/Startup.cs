using APPMVC.NET.ExtendMethods;
using APPMVC.NET.Service;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APPMVC.NET
{
    public class Startup
    {
        public static string ContentRootPath { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddTransient(typeof(ILogger<>), typeof(ILogger<>));
            services.Configure<RazorViewEngineOptions>(options =>
            {
                //view/controller/action.cshtml
                //myview/controller/action.cshtml

                //{0} -> ten Action
                //{1} -> ten Controller
                //{3} -> ten Area
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            services.AddSingleton<PlanetService>();  
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.AddStatusCodePages(); // tuy bien response loi 400-599
            app.UseRouting();

            app.UseAuthentication(); //xác định danh tính 
            app.UseAuthorization();  // xác thực quyền truy cập

            _ = app.UseEndpoints(endpoints =>
              {
                  endpoints.MapGet("/sayhi", async (context) =>
                  {
                      await context.Response.WriteAsync($"Hello Aspnet MVC {DateTime.Now}");
                  });
                  //endpoints.MapControllers();
                  //endpoints.MapControllerRoute();
                  //endpoints.MapDefaultControllerRoute();
                  //endpoints.MapAreaControllerRoute();

                  // URL-start-here
                  //controller =>
                  //Action =>
                  //area =>

                  endpoints.MapControllerRoute(
                     name: "first",
                     pattern: "{url}/{id?}",
                     defaults: new
                     {
                         controller = "First",
                         action = "ViewProduct",
                     },
                     constraints: new
                     {
                         url = "xemsanpham",
                         id = new RangeRouteConstraint(2,4)
                     }


                     );
                  endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "/{controller=Home}/{action=Index}/{id?}"
                      //defaults: new
                      //{
                      //    controller = "first",
                      //    action ="ViewProduct",
                      //    id =3
                      //}

                      
                      );

              });
        }
    }
}
