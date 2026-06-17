using LeveInvestimentos.Core.Entities;

namespace LeveInvestimentos.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Usuarios.Any(u => u.Email == "ti@leveinvestimentos.com.br"))
            {
                var usuarioTI = new Usuario
                {
                    NomeCompleto = "Administrador TI",
                    DataNascimento = new DateTime(1990, 1, 1),
                    TelefoneFixo = "(11) 2537-7777",
                    TelefoneCelular = "(11) 99999-9999",
                    Email = "ti@leveinvestimentos.com.br",
                    Endereco = "Praça Maastricht, nº 200",
                    FotoUsuario = "default-avatar.png",
                    Senha = "teste123",
                    IsGestor = true 
                };

                context.Usuarios.Add(usuarioTI);
                context.SaveChanges();
            }
        }
    }
}