using Packt.Shared;
namespace Northwind.mvc.Models
{
    public record HomeIndexViewModel
    (
        int VisitorCount,
        IList<Category> Categories,
        IList<Product> Products
    );
}
