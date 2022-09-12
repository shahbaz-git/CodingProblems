using CustomerApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace CustomerApi
{
    public class Startup
    {
        public  IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerApi", Version = "v1" });
            });

            services.AddScoped<ICustomerService,CustomerService>();

            static void SqlServerOptionsAction(SqlServerDbContextOptionsBuilder o)
            {
                o.CommandTimeout(30);
                o.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
            }
            void DbContextOptionsBuilder(IServiceProvider s,
                                       DbContextOptionsBuilder o)
            {
                var cnn = _configuration.GetConnectionString("DefaultConnection");

                o.UseSqlServer(cnn, SqlServerOptionsAction);
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
