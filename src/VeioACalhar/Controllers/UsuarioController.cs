using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Services;
using VeioACalhar.ViewModels;

namespace VeioACalhar.Controllers;

public class UsuarioController : Controller
{
    private readonly IUserService userService;

    public UsuarioController(IUserService userService)
    {
        this.userService = userService;
    }

    public IActionResult SignIn()
    {
        return View(new SignInViewModel());
    }

    [HttpPost]
    public IActionResult SignIn(IFormCollection collection)
    {
        try
        {
            var user = collection["User"];
            var password = collection["Password"];

            userService.SignIn(user, password);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception ex)
        {
            return View(nameof(SignIn), new SignInViewModel()
            {
                Error = ex.Message
            });
        }
    }

    [Route("Usuario/SignOut")]
    public IActionResult UserSignOut()
    {
        userService.SignOut();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public IActionResult SignUp()
    {
        return View(new SignUpViewModel());
    }

    [HttpPost]
    public IActionResult SignUp(IFormCollection collection)
    {
        try
        {
            var user = collection["User"];
            var password = collection["Password"];

            userService.SignUp(user, password);

            userService.SignIn(user, password);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception ex)
        {
            return View(nameof(SignUp), new SignUpViewModel()
            {
                Error = ex.Message
            });
        }
    }
}
