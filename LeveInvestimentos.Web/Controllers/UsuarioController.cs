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

        // Injetamos o repositório e o ambiente web (para saber a pasta do upload da foto)
        public UsuarioController(IUsuarioRepository repository, IWebHostEnvironment webHostEnvironment) 
        { 
            _repository = repository; 
            _webHostEnvironment = webHostEnvironment;
        }

        // TELA DE LISTAGEM DE USUÁRIOS
        [HttpGet]
        public async Task<IActionResult> Listar() 
        {
            var usuarios = await _repository.GetAllAsync();
            return View(usuarios);
        }

        // TELA DE FORMULÁRIO (CADASTRAR)
        [HttpGet]
        public IActionResult Cadastrar()
        {
            // Bloqueia subordinados de verem a tela de cadastro
            if (User.FindFirst("IsGestor")?.Value != "True") 
            {
                return RedirectToAction("AcessoNegado", "Home");
            }
            return View();
        }

        // AÇÃO DE SALVAR O FORMULÁRIO NO BANCO DE DADOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(UsuarioViewModel model)
        {
            // Dupla checagem de segurança contra hackers
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();
            
            // Verifica se preencheu tudo certo
            if (!ModelState.IsValid) return View(model);

            // 🔴 NOVA VALIDAÇÃO: CHECAR E-MAIL DUPLICADO PARA NÃO QUEBRAR O BANCO 🔴
            var usuarioExistente = await _repository.GetByEmailAsync(model.Email);
            if (usuarioExistente != null)
            {
                // Devolve um aviso amigável na tela do usuário
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado no sistema. Tente utilizar outro.");
                return View(model);
            }

            // Nome padrão caso o usuário não envie foto
            string nomeArquivoFoto = "default-avatar.png";

            // Lógica profissional para salvar a foto na pasta do servidor
            if (model.FotoArquivo != null && model.FotoArquivo.Length > 0)
            {
                string pastaUploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                
                // Cria a pasta uploads caso ela não exista
                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                // Gera um nome único para o arquivo não sobrescrever fotos antigas
                nomeArquivoFoto = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.FotoArquivo.FileName);
                string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivoFoto);

                using (var fileStream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await model.FotoArquivo.CopyToAsync(fileStream);
                }
            }

            // Mapeia o ViewModel preenchido para a Entidade oficial do Banco de Dados
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
                FotoUsuario = nomeArquivoFoto // Grava o nome final da foto salva
            };

            // Salva no banco
            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();
            
            // Retorna para a tabela de listagem
            return RedirectToAction("Listar");
        }
    }
}