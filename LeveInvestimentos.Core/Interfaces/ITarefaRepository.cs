using LeveInvestimentos.Core.Entities;

namespace LeveInvestimentos.Core.Interfaces
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetAllAsync();
        Task AddAsync(Tarefa tarefa);
        
        // --- NOVOS MÉTODOS ---
        Task<Tarefa?> GetByIdAsync(int id);
        void Update(Tarefa tarefa);
        void Delete(Tarefa tarefa);
        // ---------------------
        
        Task<bool> SaveChangesAsync();
    }
}