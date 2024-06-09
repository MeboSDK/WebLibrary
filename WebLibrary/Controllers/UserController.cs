using Microsoft.AspNetCore.Mvc;
using WebLibrary.Models.UserModels;

namespace WebLibrary.Controllers;
public class UserController : Controller
{
    [HttpGet]
    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LogIn(LogInViewModel model)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        return View();
    }
}
