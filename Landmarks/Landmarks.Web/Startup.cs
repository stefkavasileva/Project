using System.Collections.Generic;
using System.Globalization;
using System.Text.Unicode;
using AutoMapper;
using Landmarks.Common.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Landmarks.Data;
using Landmarks.Interfaces;
using Landmarks.Interfaces.Admin;
using Landmarks.Interfaces.Main;
using Landmarks.Models;
using Landmarks.Services;
using Landmarks.Services.Admin;
using Landmarks.Services.Main;
using Landmarks.Web.Common;
using Landmarks.Web.Common.Constants;
using Landmarks.Web.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using IRegionService = Landmarks.Interfaces.Admin.IRegionService;
using LandmarkService = Landmarks.Services.Admin.LandmarkService;
using RegionService = Landmarks.Services.Admin.RegionService;

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
            ConfigLocalizer(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LandmarksDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(ConfigConstants.ConnectionString)));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<LandmarksDbContext>();

            services.AddAuthentication()
                .AddFacebook(option =>
                {
                    option.AppId = this.Configuration.GetSection(ConfigConstants.ConfigFacebookAppId).Value;
                    option.AppSecret = this.Configuration.GetSection(ConfigConstants.ConfigFacebookAppSecret).Value;
                })
                .AddGoogle(option =>
                {
                    option.ClientId = this.Configuration.GetSection(ConfigConstants.ConfigGoogleClentId).Value;
                    option.ClientSecret = this.Configuration.GetSection(ConfigConstants.ConfigGoogleClentSecret)
                        .Value;
                })
                .AddGitHub(option =>
                {
                    option.ClientId = this.Configuration.GetSection(ConfigConstants.ConfigGitHubClentId).Value;
                    option.ClientSecret = this.Configuration.GetSection(ConfigConstants.ConfigGitHunClentSecret).Value;
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

            ConfigServices(services);

            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = NamesConstants.ResourcesPathName; })
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
                options.AddPolicy(NamesConstants.PolicyName, policy => policy.RequireRole(NamesConstants.RoleAdmin));
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
                app.UseExceptionHandler(RedirectURL.ToErrorPage);
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

        private static void ConfigLocalizer(IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = NamesConstants.ResourcesPathName; });
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo(NamesConstants.CultureInfoEn),
                        new CultureInfo(NamesConstants.CultureInfoGb),
                    };

                    opts.DefaultRequestCulture = new RequestCulture(NamesConstants.CultureInfoEn);
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });
        }

        private static void ConfigServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddScoped<IMainService, MainService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IVisitationService, VisitationService>();
            services.AddScoped<Interfaces.Admin.ILandmarkService, LandmarkService>();
            services.AddScoped<Interfaces.Main.IRegionService, Services.Main.RegionService>();
            services.AddScoped<Interfaces.Main.ICommentsService, Services.Main.CommentsService>();
            services.AddScoped<Interfaces.Main.ILandmarkService, Services.Main.LandmarkService>();
            services.AddScoped<Interfaces.Operator.ILandmarkService, Services.Operator.LandmarkService>();
        }
    }
}
