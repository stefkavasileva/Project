using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Landmarks.Common.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Landmarks.Data;
using Landmarks.Interfaces.Admin;
using Landmarks.Interfaces.Operator;
using Landmarks.Models;
using Landmarks.Services.Admin;
using Landmarks.Web.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Landmarks.Web
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
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("bg"),
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LandmarksDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LandmarksConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<LandmarksDbContext>();

            services.AddAuthentication()
                .AddFacebook(option =>
                {
                    option.AppId = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
                    option.AppSecret = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
                })
                .AddGoogle(option =>
                {
                    option.ClientId = this.Configuration.GetSection("ExternalAuthentication:Google:ClientId").Value;
                    option.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:Google:ClientSecret")
                        .Value;
                })
                .AddGitHub(option =>
                {
                    option.ClientId = this.Configuration.GetSection("ExternalAuthentication:GitHub:ClientId").Value;
                    option.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:GitHub:ClientSecret").Value;
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 4,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredUniqueChars = 1
                };

                //options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<Interfaces.Admin.ILandmarkService,LandmarkService>();
            services.AddScoped<Interfaces.Operator.ILandmarkService,Services.Operator.LandmarkService>();


            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider =
                        (type, factory) => factory.Create(typeof(ValidationResources));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddRazorPagesOptions(options =>
                {
                    AddAuthorizationPages(options);

                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });
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

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.SeedDatabase();

            app.UseAuthentication();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddAuthorizationPages(Microsoft.AspNetCore.Mvc.RazorPages.RazorPagesOptions options)
        {
            options.Conventions.AuthorizePage("/Add", "Administrator");
            options.Conventions.AuthorizeFolder("/Category", "Administrator");
            options.Conventions.AuthorizePage("/Edit", "Administrator");
            options.Conventions.AuthorizeFolder("/Category", "Administrator");
            options.Conventions.AuthorizePage("/List", "Administrator");
            options.Conventions.AuthorizeFolder("/Category", "Administrator");

            options.Conventions.AuthorizePage("/Add", "Administrator");
            options.Conventions.AuthorizeFolder("/Region", "Administrator");
            options.Conventions.AuthorizePage("/Edit", "Administrator");
            options.Conventions.AuthorizeFolder("/Region", "Administrator");
            options.Conventions.AuthorizePage("/List", "Administrator");
            options.Conventions.AuthorizeFolder("/Region", "Administrator");

            options.Conventions.AuthorizePage("/Admin/Pages/Landmark/Add", "Administrator");
            options.Conventions.AuthorizeFolder("/Admin/Pages/Landmark", "Administrator");
            options.Conventions.AuthorizePage("/Edit", "Administrator");
            options.Conventions.AuthorizeFolder("/Landmark", "Administrator");
            options.Conventions.AuthorizePage("/List", "Administrator");
            options.Conventions.AuthorizeFolder("/Landmark", "Administrator");
        }

    }
}
