using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => RedirectToAction("Index", "Residence");

    public IActionResult Support()            => Content("Area=Public, Controller=Home, Action=Support");
    public IActionResult CancellationPolicy() => Content("Area=Public, Controller=Home, Action=CancellationPolicy");
    public IActionResult Terms()              => Content("Area=Public, Controller=Home, Action=Terms");
    public IActionResult Cookies()            => Content("Area=Public, Controller=Home, Action=Cookies");
}
