namespace VeioACalhar.Middlewares;

public class NotFoundMiddleware
{
    private readonly RequestDelegate next;

    public NotFoundMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await next.Invoke(httpContext);

        if (httpContext.Response.StatusCode == 404)
            httpContext.Response.Redirect("/NotFound");
    }
}

