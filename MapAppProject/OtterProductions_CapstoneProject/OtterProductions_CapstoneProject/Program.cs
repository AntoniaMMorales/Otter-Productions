using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OtterProductions_CapstoneProject.Areas.Identity.Data;
using OtterProductions_CapstoneProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using OtterProductions_CapstoneProject.Utilities;

namespace OtterProductions_CapstoneProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AuthenticationConnection") ?? throw new InvalidOperationException("Connection string 'AuthenticationConnection' not found.");

            builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<MapAppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MapAppConnection")));
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
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


            var app = builder.Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;


            //https://www.stevejgordon.co.uk/aspnet-core-dependency-injection-what-is-the-iserviceprovider-and-how-is-it-built
            //var serviceProvider = builder.Services.BuildServiceProvider();
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    // Get the IConfiguration service that allows us to query user-secrets and 
                    // the configuration on Azure

                    // Set password with the Secret Manager tool, or store in Azure app configuration
                    // dotnet user-secrets set SeedUserPW <pw>

                    var testUserPw = builder.Configuration["SeedUserPW"];
                    var adminPw = builder.Configuration["SeedAdminPW"];

                    SeedUsers.Initialize(app.Services, SeedData.UserSeedData, testUserPw).Wait();
                    SeedUsers.InitializeAdmin(app.Services, "admin@example.com", "admin", adminPw, "The", "Admin").Wait();
                }
                catch (Exception ex)
                {
                    var logger = app.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            app.SeedData();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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