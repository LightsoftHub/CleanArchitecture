using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers;

#if DEBUG
[Route("/")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
#endif