using System.Diagnostics;

namespace LeveInvestimentos.Application.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string assunto, string mensagem);
    }

    public class EmailService : IEmailService
    {
        public Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
        {
            Debug.WriteLine($"E-mail para {destinatario}: {assunto}");
            return Task.CompletedTask;
        }
    }
}