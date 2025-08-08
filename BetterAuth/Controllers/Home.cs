using Microsoft.AspNetCore.Mvc;

namespace BetterAuth.Controllers;

[Route("/")]
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}