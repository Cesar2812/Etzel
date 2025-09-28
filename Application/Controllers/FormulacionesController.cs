using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class FormulacionesController : Controller
    {
        public IActionResult MisFormulaciones()
        {
            return View();
        }

        public IActionResult CalculadoraDePorciones()
        {
            return View();
        }
    }
}
