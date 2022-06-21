using VeioACalhar.Services;

namespace VeioACalhar.Middlewares;

public class LoginRequiredMiddleware
{
    private readonly IUserService userService;
    private readonly RequestDelegate next;
    private readonly List<string> allowedRoutes;

    public LoginRequiredMiddleware(IUserService userService, RequestDelegate next)
    {
        this.userService = userService;
        this.next = next;

        allowedRoutes = new()
        {
           "/Usuario/SignIn",
           "/Usuario/SignUp",
           "/Usuario/SignUp/SignUp",
           "/Home/Error",
        };
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (userService.CurrentUser.Id > 0 || IsLoginRequest(httpContext))
            return next.Invoke(httpContext);

        httpContext.Response.Redirect("/usuario/signin");
        return Task.CompletedTask;
    }

    private bool IsLoginRequest(HttpContext httpContext)
    {
        var path = httpContext.Request.Path;
        return allowedRoutes.Any(r => r.Equals(path, StringComparison.OrdinalIgnoreCase));
    }
}
