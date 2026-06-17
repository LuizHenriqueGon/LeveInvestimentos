using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LeveInvestimentos.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Usuarios.AnyAsync(u => u.Email == "ti@leveinvestimentos.com.br"))
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

                await context.Usuarios.AddAsync(usuarioTI);
                await context.SaveChangesAsync();
            }
        }
    }
}