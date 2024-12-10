using Microsoft.AspNetCore.Mvc;

namespace area_adm.Areas.Adm.Controllers
{

    [Area("Adm")]
    public class AdmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
