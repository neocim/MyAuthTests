using Microsoft.AspNetCore.Mvc;

namespace BetterAuth.Controllers;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}