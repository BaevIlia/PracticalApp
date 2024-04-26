using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared;
using Northwind.WebApi.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.HttpLogging;
using System.Net.Http.Headers;
namespace Northwind.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("https://localhost:5002/");

            // Add services to the container.
            builder.Services.AddHttpClient(name: "Northwind.WebApi", configureClient: options =>
            {
                options.BaseAddress = new Uri("https:localhost:5002/");
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            });
            builder.Services.AddNorthwindContext();
            builder.Services.AddControllers(options=>
            {
                Console.WriteLine("Default output formatters:");
                foreach (IOutputFormatter formatter in options.OutputFormatters) 
                {
                    OutputFormatter? mediaFormatter = formatter as OutputFormatter;
                    if (mediaFormatter == null)
                    {
                        Console.WriteLine($"  {formatter.GetType().Name}");
                    }
                    else 
                    {
                        Console.WriteLine(" {0}, Media types: {1}",
                            arg0: mediaFormatter.GetType().Name,
                            arg1:string.Join(", ", mediaFormatter.SupportedMediaTypes));
                    }
                }
            }).AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new() { Title = "Northwind Service Api", Version = "v1" });
            });
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.All;
                options.RequestBodyLogLimit = 4096;
                options.ResponseBodyLogLimit = 4096;
            });

            var app = builder.Build();
            app.UseHttpLogging();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Northwind Service API Version 1");
                    c.SupportedSubmitMethods(new[]
                    {
                        SubmitMethod.Get, SubmitMethod.Post,
                        SubmitMethod.Put, SubmitMethod.Delete,
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
           

            app.MapControllers();

            app.Run();
        }
    }
}