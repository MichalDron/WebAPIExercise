using CustomerAddressManager.BusinessDomain.Services;
using CustomerAddressManager.BusinessDomain.Validators;
using CustomerAddressManager.Dal;
using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Repositories;
using CustomerAddressManager.WebApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CustomerAddressManager.WebApi
{
    public class Startup
    {
        private const string ConnectionStringKey = "CustomerAddressManagerDb";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddDbContext<CustomerAddressContext>(options => options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringKey)));

            services.AddScoped(typeof(DbContext), typeof(CustomerAddressContext));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAddressService), typeof(AddressService));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));

            services.AddTransient(typeof(IValidator<Customer>), typeof(CustomerValidator));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();
        }
    }
}
