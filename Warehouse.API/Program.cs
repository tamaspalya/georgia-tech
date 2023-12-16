using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddScoped<IWarehouseContext, WarehouseContext>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse.API", Version = "v1" });
            });

            /*
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<BookAddedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("book_added_queue", e =>
                    {
                        e.ConfigureConsumer<BookAddedEventConsumer>(context);
                    });
                });
            });
            */

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse.API v1"));
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
