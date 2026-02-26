using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
