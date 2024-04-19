using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared;
using Northwind.WebApi.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace Northwind.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
         
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new() { Title = "Northwind Service Api", Version = "v1" });
            });
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}