using System;
using System.ComponentModel.DataAnnotations;

namespace LeveInvestimentos.Web.ViewModels
{
    public class TarefaViewModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string MensagemDescritiva { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data limite é obrigatória.")]
        public DateTime DataLimite { get; set; }

        [Required(ErrorMessage = "Selecione um subordinado.")]
        public int SubordinadoId { get; set; }
    }
}