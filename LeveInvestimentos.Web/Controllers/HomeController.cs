using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeveInvestimentos.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        [AllowAnonymous]
        public IActionResult AcessoNegado() => View();
    }
}