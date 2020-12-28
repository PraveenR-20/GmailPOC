using Gmail_POC.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace Gmail_POC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

  
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gmail POC", Version = "v1" });
            });
            services.AddHealthChecks();
            RegisterServices(services, Configuration);
        }

        private void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DependencyContainer.RegisterServices(services, configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization(GetRequestLocalizationOptions());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gmail POC V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {               
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
        private RequestLocalizationOptions GetRequestLocalizationOptions()
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),

                // for number formats, dates
                SupportedCultures = supportedCultures,

                // for UI strings
                SupportedUICultures = supportedCultures
            };

            return options;
        }
    }
}
