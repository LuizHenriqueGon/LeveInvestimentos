using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LeveInvestimentos.Core.Entities;
using LeveInvestimentos.Core.Interfaces;
using LeveInvestimentos.Web.ViewModels;
using System.IO;

namespace LeveInvestimentos.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuarioController(IUsuarioRepository repository, IWebHostEnvironment webHostEnvironment) 
        { 
            _repository = repository; 
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Listar() 
        {
            var usuarios = await _repository.GetAllAsync();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            if (User.FindFirst("IsGestor")?.Value != "True") 
            {
                return RedirectToAction("AcessoNegado", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(UsuarioViewModel model)
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();
            
            if (!ModelState.IsValid) return View(model);

            var usuarioExistente = await _repository.GetByEmailAsync(model.Email);

            if (usuarioExistente != null)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado no sistema. Tente utilizar outro.");
                return View(model);
            }

            string nomeArquivoFoto = "default-avatar.png";

            if (model.FotoArquivo != null && model.FotoArquivo.Length > 0)
            {
                string pastaUploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                
                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                nomeArquivoFoto = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.FotoArquivo.FileName);
                string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivoFoto);

                using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await model.FotoArquivo.CopyToAsync(fileStream);
                }
            }

            var usuario = new Usuario
            {
                NomeCompleto = model.NomeCompleto,
                DataNascimento = model.DataNascimento,
                TelefoneFixo = model.TelefoneFixo,
                TelefoneCelular = model.TelefoneCelular,
                Email = model.Email,
                Endereco = model.Endereco,
                Senha = model.Senha,
                IsGestor = model.IsGestor,
                FotoUsuario = nomeArquivoFoto 
            };

            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();
            
            return RedirectToAction("Listar");
        }
    }
}