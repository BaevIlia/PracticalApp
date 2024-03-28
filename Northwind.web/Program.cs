using Northwind.web;
namespace Northwind.web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).Build().Run();

            Console.WriteLine("This executes after the web server has stopped!");

            
        }
    }
}