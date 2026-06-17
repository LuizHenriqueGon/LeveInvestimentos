using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LeveInvestimentos.Web.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        public string TelefoneFixo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone celular é obrigatório.")]
        public string TelefoneCelular { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;

        public bool IsGestor { get; set; }

        // Propriedade que recebe o arquivo do botão de upload
        public IFormFile? FotoArquivo { get; set; }
    }
}