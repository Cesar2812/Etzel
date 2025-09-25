using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Models;  // <- importante: para usar Product

namespace Application.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            // Productos de prueba (luego vendrán de la BD)
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Chocolate Truffles", Description = "Hecho a mano", Price = 24.99m, ImageUrl = "/images/chocolates.jpg" },
                new Product { Id = 2, Name = "Sourdough Bread", Description = "Pan artesanal", Price = 8.99m, ImageUrl = "/images/pan.jpg" },
                new Product { Id = 3, Name = "Berry Preserves", Description = "Mermelada orgánica", Price = 12.99m, ImageUrl = "/images/mermelada.jpg" }
            };

            return View(products);
        }
    }
}
