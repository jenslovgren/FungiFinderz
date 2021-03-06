using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FungiFinder.Models;
using FungiFinder.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FungiFinder
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var constring = configuration.GetConnectionString("LiveConnection");
            services.AddDbContext<MyIdentityContext>(o => o.UseSqlServer(constring/*, c => c.EnableRetryOnFailure()*/));
            services.AddDbContext<FungiFinderContext>(o => o.UseSqlServer(constring/*, c => c.EnableRetryOnFailure()*/));
            services.AddTransient<AccountService>();
            services.AddTransient<FunctionsService>();
            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //PasswordHasher
            services.AddIdentity<MyIdentityUser, IdentityRole>(o =>
            {
                o.Password.RequiredLength = 6;
                o.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<MyIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
           

            services.ConfigureApplicationCookie(o => o.LoginPath = "/index");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error/exception");

            app.UseStatusCodePagesWithRedirects("/error/http/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
           
        }
    }
}
