using Microsoft.AspNetCore.Mvc;
using Northwind.mvc.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Packt.Shared;

namespace Northwind.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;



        public HomeController(ILogger<HomeController> logger, NorthwindContext injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        
        [ResponseCache(Duration =10, Location =ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            _logger.LogError("This is a serious error (not really)!");
            _logger.LogWarning("This is your first warning");
            _logger.LogWarning("Second warning!");
            _logger.LogInformation("I'm in the Index method of the HomeController");

            HomeIndexViewModel model = new
                (
                    VisitorCount: (new Random().Next(1, 1001)),
                    Categories: db.Categories.ToList(),
                    Products: db.Products.ToList()
                );
            return View(model);
        }
        [Route("private")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}