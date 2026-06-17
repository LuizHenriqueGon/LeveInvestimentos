using LeveInvestimentos.Core.Entities;
using LeveInvestimentos.Core.Interfaces;

namespace LeveInvestimentos.Application.Services
{
    public class TarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<bool> AgendarTarefaAsync(Tarefa tarefa)
        {
            if (tarefa.DataLimite < DateTime.Now)
                throw new ArgumentException("A data limite da tarefa não pode ser retroativa.");

            await _tarefaRepository.AddAsync(tarefa);
            return await _tarefaRepository.SaveChangesAsync();
        }
    }
}