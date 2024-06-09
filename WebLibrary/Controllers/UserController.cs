using Application.Commands.UserC.Commands;
using Application.Queries.UserQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Models.UserModels;

namespace WebLibrary.Controllers;
public class UserController : Controller
{
    public readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult LogIn()
    {
        if(User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInViewModel model)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            UserLogInQuery query = new(model.Email, model.Password);

            var token = await _mediator.Send(query);

            Response.Cookies.Append("authToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Password or UserName is wrong!");
            return View();
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            UserRegisterCommand query = new(model.UserName, model.Email, model.Password);

            var token = await _mediator.Send(query);

            Response.Cookies.Append("authToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Remove the token cookie
        Response.Cookies.Delete("authToken");
        return RedirectToAction("Index", "Home");
    }
}
