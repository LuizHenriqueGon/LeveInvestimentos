using System;

namespace LeveInvestimentos.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string TelefoneFixo { get; set; } = string.Empty;
        public string TelefoneCelular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string FotoUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool IsGestor { get; set; }
    }
}