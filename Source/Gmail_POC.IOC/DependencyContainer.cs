using Gmail_POC.Application.Interfaces;
using Gmail_POC.Application.Services;
using Gmail_POC.Utility.Configurations;
using Gmail_POC.Data.Interfaces;
using Gmail_POC.Data.Repositories;
using Microsoft.Extensions.Configuration;
using msExteisons = Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gmail_POC.Data.Context;
using Microsoft.EntityFrameworkCore;
using Gmail_POC.Utility.ConfigurationSettings;

namespace Gmail_POC.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, msExteisons.IConfiguration configuration)

        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DbConnString"]));

            services.Configure<UserSecretSetting>
                (options =>
                {
                    options.GoogleClientSecret = configuration["Authentication:Google:ClientSecret"];
                    options.GoogleClientId = configuration["Authentication:Google:ClientId"];
                    options.EncryptionKey = configuration["EncryptionKey"];
                });

            services.AddTransient<UserSecretSetting>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
