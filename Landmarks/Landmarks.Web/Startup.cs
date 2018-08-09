using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Landmarks.Data;
using Landmarks.Interfaces.Admin;
using Landmarks.Models;
using Landmarks.Services.Admin;
using Landmarks.Web.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ILandmarkService, LandmarkService>();

           

            services.AddMvc()
                .AddRazorPagesOptions(options =>
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

                    options.Conventions.AuthorizePage("/Add", "Administrator");
                    options.Conventions.AuthorizeFolder("/Landmark", "Administrator");
                    options.Conventions.AuthorizePage("/Edit", "Administrator");
                    options.Conventions.AuthorizeFolder("/Landmark", "Administrator");
                    options.Conventions.AuthorizePage("/List", "Administrator");
                    options.Conventions.AuthorizeFolder("/Landmark", "Administrator");

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.SeedDatabase();

            app.UseAuthentication();

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
    }
}
