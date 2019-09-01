using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsBLL.Abstract;
using NewsBLL.Concrete;
using NewsDA.Abstract;
using NewsDA.Concrete;
using NewsDA.Context;
using NewsSiteVersion2.Entities;
using NewsDA.UnitOfWork;

namespace NewsSiteVersion2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddScoped<INewsService, NewsService>();
            //services.AddScoped<INewsDal, NewsDal>();
            //services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<INewsModal, NewsModalService>();
            services.AddSingleton<HttpContextAccessor>();

            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<CustomIdentityDbContext>
                (
                options=>options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NewsSiteUsers;Trusted_Connection=true")
                );
            services.AddIdentity<ApplicationUser,CustomRole>
                (options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                  
})
                          
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );
            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Identity/Account/Login");
                x.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
                x.LogoutPath = new PathString("/Identity/Account/Logout");
            });
            services.AddSession();
            services.AddDistributedMemoryCache();
        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
