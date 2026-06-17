using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Core.Entities;
using LeveInvestimentos.Core.Interfaces;
using LeveInvestimentos.Infrastructure.Data;

namespace LeveInvestimentos.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) 
        { 
            _context = context; 
        }

        public async Task<Usuario?> GetByEmailAsync(string email) 
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<Usuario>> GetAllAsync() 
            => await _context.Usuarios.ToListAsync();

        public async Task AddAsync(Usuario usuario) 
            => await _context.Usuarios.AddAsync(usuario);

        public async Task<Usuario?> GetByIdAsync(int id) 
            => await _context.Usuarios.FindAsync(id);

        public void Update(Usuario usuario) 
            => _context.Usuarios.Update(usuario);

        public void Delete(Usuario usuario) 
            => _context.Usuarios.Remove(usuario);

        public async Task<bool> SaveChangesAsync() 
            => await _context.SaveChangesAsync() > 0;
    }
}