using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Core.Entities;
using LeveInvestimentos.Core.Interfaces;
using LeveInvestimentos.Infrastructure.Data;

namespace LeveInvestimentos.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ApplicationDbContext _context;

        public TarefaRepository(ApplicationDbContext context) 
        { 
            _context = context; 
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync() 
            => await _context.Tarefas.Include(t => t.Subordinado).ToListAsync();

        public async Task AddAsync(Tarefa tarefa) 
            => await _context.Tarefas.AddAsync(tarefa);

        // --- IMPLEMENTAÇÃO DOS NOVOS MÉTODOS ---
        public async Task<Tarefa?> GetByIdAsync(int id) 
            => await _context.Tarefas.Include(t => t.Subordinado).FirstOrDefaultAsync(t => t.Id == id);

        public void Update(Tarefa tarefa) 
            => _context.Tarefas.Update(tarefa);

        public void Delete(Tarefa tarefa) 
            => _context.Tarefas.Remove(tarefa);
        // ---------------------------------------

        public async Task<bool> SaveChangesAsync() 
            => await _context.SaveChangesAsync() > 0;
    }
}