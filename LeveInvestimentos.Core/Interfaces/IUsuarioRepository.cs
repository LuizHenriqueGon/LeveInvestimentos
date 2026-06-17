using LeveInvestimentos.Core.Entities;

namespace LeveInvestimentos.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        
        Task<Usuario?> GetByIdAsync(int id);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
        
        Task<bool> SaveChangesAsync();
    }
}