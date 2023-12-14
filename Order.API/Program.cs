using EventBus.Messages.Common;
using MassTransit;
using Order.API.EventBusConsumer;
using Microsoft.OpenApi.Models;
using Order.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Order.Infrastructure;
using Order.Application;


namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Add services to the container.
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<BasketCheckoutConsumer>();
                
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
                    // Additional configuration for RabbitMQ can be done here

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            builder.Services.AddScoped<BasketCheckoutConsumer>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v1" });
            });

            builder.Services.AddHealthChecks()
                            .AddDbContextCheck<OrderContext>();

            var app = builder.Build();

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

            // Migrate and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<OrderContext>();
                var logger = services.GetService<ILogger<OrderContextSeed>>();
                OrderContextSeed.SeedAsync(context, logger).Wait();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
