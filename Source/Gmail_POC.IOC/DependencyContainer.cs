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
using AutoMapper;

namespace Gmail_POC.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, msExteisons.IConfiguration configuration)
        
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            //dbcontext initialization
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DbConnString"]));            
            
            // appstting configurations
            services.Configure<ConnectionStringSettings>
                (options => configuration.GetSection("Connectionstrings").Bind(options));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Application Services
            services.AddTransient<IUserService, UserService>();

            //Data
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
