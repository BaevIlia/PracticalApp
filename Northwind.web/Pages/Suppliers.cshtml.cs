using Packt.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.web.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<Supplier>? Suppliers { get; set; }
        [BindProperty]
        public Supplier? Supplier { get; set; }



        private NorthwindContext db;

        public SuppliersModel(NorthwindContext injectedContext)
        {
            db = injectedContext;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";

            Suppliers = db.Suppliers.OrderBy(c => c.Country).ThenBy(c => c.CompanyName);
        }
        public IActionResult OnPost() 
        {
            if ((Supplier is not null) && ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }
            else 
            {
                return Page();
            }
        }

    }
}
