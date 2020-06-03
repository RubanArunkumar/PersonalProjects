using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MortgageCalculator.Core.Providers;
using MortgageCalculator.Core.Validator;
using MortgageCalculator.Data;
using MortgageCalculator.Data.Repository;
using MortgageCalculator.WebApi.Mapper;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MortgageCalculator.WebApi
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
            services.AddControllers();
            services.AddDbContext<MortgageCalculatorDbContext>
                (options => options.UseInMemoryDatabase(databaseName: "MortgageInterestRate"));
            services.AddTransient<IMortgageCalculatorRepository, MortgageCalculatorRepository>();
            services.AddTransient<IInterestRateProvider, InterestRateProvider>();
            services.AddTransient<IMortgageCalculateProvider, MortgageCalculateProvider>();
            services.AddTransient<IEligibilityCheckProvider, EligibilityCheckProvider>();
            services.AddTransient<IRequestValidator, RequestValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mortgage API", Version = "v1" });
            });

            var configurationExpression = new MapperConfigurationExpression();
            configurationExpression.AddProfile<ApiToCoreMapper>();
            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            mapperConfiguration.AssertConfigurationIsValid();
            services.AddSingleton<IMapper>(new AutoMapper.Mapper(mapperConfiguration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mortgage API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
