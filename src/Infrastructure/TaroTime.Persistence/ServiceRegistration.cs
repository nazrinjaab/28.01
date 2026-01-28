using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Contexts;
using TaroTime.Persistence.Implementations.Repositories;
using TaroTime.Persistence.Implementations.Services;

namespace TaroTime.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>

            opt.UseSqlServer(config.GetConnectionString("Default")));

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail=true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);


            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ITarotRepository, TarotRepository>();
            services.AddScoped<ICoffeeRepository, CoffeeRepository>();
            services.AddScoped<IPalmRepository, PalmRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITarotService, TarotService>();
            services.AddScoped<ICoffeeService, CoffeeService>();
            services.AddScoped<IPalmService, PalmService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<AppDbContextInitializer>();


            return services;




        }


        public static async Task<IApplicationBuilder> UseAppDbContextInitializer(this IApplicationBuilder app, IServiceScope scope)
        {
              var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

                await initializer.InitializeDbContext();
                await initializer.InitalizeRoleAsync();
                await initializer.InitalizeAdmin();
        
               return app;
        }

    }
}
