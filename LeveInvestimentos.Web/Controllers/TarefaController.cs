using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LeveInvestimentos.Core.Entities;
using LeveInvestimentos.Core.Interfaces;
using LeveInvestimentos.Web.ViewModels;
using LeveInvestimentos.Application.Services;

namespace LeveInvestimentos.Web.Controllers
{
    [Authorize]
    public class TarefaController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly TarefaService _tarefaService;

        public TarefaController(
            IUsuarioRepository usuarioRepository, 
            ITarefaRepository tarefaRepository, 
            TarefaService tarefaService)
        {
            _usuarioRepository = usuarioRepository;
            _tarefaRepository = tarefaRepository;
            _tarefaService = tarefaService;
        }

        // TELA DE LISTAGEM
        [HttpGet]
        public async Task<IActionResult> Listar() 
        {
            return View(await _tarefaRepository.GetAllAsync());
        }

        // TELA DE AGENDAR
        [HttpGet]
        public async Task<IActionResult> Agendar()
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return RedirectToAction("Listar");
            
            ViewBag.Subordinados = await _usuarioRepository.GetAllAsync();
            return View();
        }

        // AÇÃO DE SALVAR NOVA TAREFA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agendar(TarefaViewModel model)
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();

            if (!ModelState.IsValid)
            {
                ViewBag.Subordinados = await _usuarioRepository.GetAllAsync();
                return View(model);
            }

            var gestorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int gestorId = string.IsNullOrEmpty(gestorIdClaim) ? 1 : int.Parse(gestorIdClaim);

            var tarefa = new Tarefa
            {
                MensagemDescritiva = model.MensagemDescritiva,
                DataLimite = model.DataLimite,
                StatusTarefa = "Pendente",
                GestorId = gestorId,
                SubordinadoId = model.SubordinadoId
            };

            try
            {
                if (await _tarefaService.AgendarTarefaAsync(tarefa)) return RedirectToAction("Listar");
                ModelState.AddModelError("", "Ocorreu um erro ao salvar a tarefa no banco de dados.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.Subordinados = await _usuarioRepository.GetAllAsync();
            return View(model);
        }

        // TELA DE EDITAR TAREFA
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();

            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null) return NotFound();

            ViewBag.Subordinados = await _usuarioRepository.GetAllAsync();
            return View(tarefa);
        }

        // AÇÃO DE SALVAR A EDIÇÃO NO BANCO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Tarefa tarefaAtualizada)
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();

            if (id != tarefaAtualizada.Id) return BadRequest();

            var tarefaNoBanco = await _tarefaRepository.GetByIdAsync(id);
            if (tarefaNoBanco == null) return NotFound();

            // Atualiza os dados permitidos
            tarefaNoBanco.MensagemDescritiva = tarefaAtualizada.MensagemDescritiva;
            tarefaNoBanco.DataLimite = tarefaAtualizada.DataLimite;
            tarefaNoBanco.SubordinadoId = tarefaAtualizada.SubordinadoId;
            tarefaNoBanco.StatusTarefa = tarefaAtualizada.StatusTarefa;

            _tarefaRepository.Update(tarefaNoBanco);
            await _tarefaRepository.SaveChangesAsync();

            return RedirectToAction("Listar");
        }

        // AÇÃO DE DELETAR TAREFA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            if (User.FindFirst("IsGestor")?.Value != "True") return Forbid();

            var tarefa = await _tarefaRepository.GetByIdAsync(id);
            if (tarefa == null) return NotFound();

            _tarefaRepository.Delete(tarefa);
            await _tarefaRepository.SaveChangesAsync();

            return RedirectToAction("Listar");
        }
    }
}