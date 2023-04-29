using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OtterProductions_CapstoneProject.Areas.Identity.Data;
using OtterProductions_CapstoneProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using OtterProductions_CapstoneProject.Utilities;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace OtterProductions_CapstoneProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("AuthenticationConnection") ?? throw new InvalidOperationException("Connection string 'AuthenticationConnection' not found.");
            var connectionString1 = builder.Configuration.GetConnectionString("AuthenticationConnectionAzure");
            

            builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString1));
            //var builder= new SqlConnectionStringBuilder(Configuration.GetConnectionString("AuthenticationConnectionAzure"));
            //builder.Password = Configuration["OPAuth:DBPassword"];
            //var connectionString2 = builder.Configuration.GetConnectionString("ApplicationConnectionAzure");


            //builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString2));


             builder.Services.AddDbContext<MapAppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MapAppConnection")));
                 //options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionAzure")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>();

            builder.Services.Configure<IdentityOptions>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedPhoneNumber = false;
                config.SignIn.RequireConfirmedEmail = false;
                config.SignIn.RequireConfirmedAccount = false;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = true;
                config.Password.RequiredLength = 8;
                config.Password.RequiredUniqueChars = 5;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddAuthorization(opts =>
            {
                opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
            });

            var app = builder.Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Get the IConfiguration service that allows us to query user-secrets and 
                    // the configuration on Azure

                    // Set password with the Secret Manager tool, or store in Azure app configuration
                    // dotnet user-secrets set SeedUserPW <pw>

                    var config = app.Services.GetRequiredService<IConfiguration>();

                    var testUserPw = config["SeedUserPW"];
                    var adminPw = config["SeedAdminPW"];
                    var orgPw = config["SeedOrganizationPW"];

                    SeedUsers.InitializeUser(services, "testUser@example.com", "user", testUserPw, "Test", "User").Wait();
                    SeedUsers.InitializeOrganization(services, "testOrg@example.com", "organization", orgPw, "Test", "Organization").Wait();
                    //SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPw).Wait();
                    SeedUsers.InitializeAdmin(services, "admin@example.com", "admin", adminPw, "The", "Admin").Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            //app.SeedData();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}