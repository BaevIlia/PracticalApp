using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.web.Pages
{
    public class CustomersModel : PageModel
    {

        public ILookup<string?, Customer>? CustomersByCountry; 

        private NorthwindContext db;

        public CustomersModel(NorthwindContext injectedContext) 
        {
            db = injectedContext;
        }
        public void OnGet()
        {
            CustomersByCountry = db.Customers.ToLookup(c => c.Country);
        }
    }
}
