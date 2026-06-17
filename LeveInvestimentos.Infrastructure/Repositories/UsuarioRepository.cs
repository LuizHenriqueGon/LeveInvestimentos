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

        // Busca um usuário pelo e-mail (usado no Login e na validação de duplicidade)
        public async Task<Usuario?> GetByEmailAsync(string email) 
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        // Busca todos os usuários cadastrados (usado na tela de Listagem de Usuários)
        public async Task<IEnumerable<Usuario>> GetAllAsync() 
            => await _context.Usuarios.ToListAsync();

        // Adiciona um novo usuário no contexto
        public async Task AddAsync(Usuario usuario) 
            => await _context.Usuarios.AddAsync(usuario);

        // --- MÉTODOS QUE ESTAVAM FALTANDO E CAUSARAM O ERRO DE BUILD ---

        // Busca um usuário pelo ID único (usado antes de Editar ou Deletar)
        public async Task<Usuario?> GetByIdAsync(int id) 
            => await _context.Usuarios.FindAsync(id);

        // Marca o usuário como modificado para o Entity Framework atualizar no banco
        public void Update(Usuario usuario) 
            => _context.Usuarios.Update(usuario);

        // Remove o usuário do contexto do Entity Framework
        public void Delete(Usuario usuario) 
            => _context.Usuarios.Remove(usuario);

        // -----------------------------------------------------------------

        // Salva fisicamente todas as alterações pendentes no banco de dados SQL Server
        public async Task<bool> SaveChangesAsync() 
            => await _context.SaveChangesAsync() > 0;
    }
}