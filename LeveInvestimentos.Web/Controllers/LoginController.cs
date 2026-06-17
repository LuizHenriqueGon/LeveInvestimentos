using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LeveInvestimentos.Core.Interfaces;

namespace LeveInvestimentos.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(IUsuarioRepository usuarioRepository) { _usuarioRepository = usuarioRepository; }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null || usuario.Senha != senha) 
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NomeCompleto),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("IsGestor", usuario.IsGestor.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}