using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WarehouseAPI.Data;
using WarehouseAPI.Data.Interfaces;
using WarehouseAPI.Repositories;
using WarehouseAPI.Repositories.Interfaces;

namespace WarehouseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IWarehouseContext, WarehouseContext>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var mongoDbConnectionString = builder.Configuration["DatabaseSettings:ConnectionString"];

            builder.Services.AddHealthChecks().AddMongoDb(
                    mongoDbConnectionString,
                    "MongoDB Health Check",
                    HealthStatus.Degraded);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            app.MapControllers();

            app.Run();
        }
    }
}