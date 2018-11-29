using System;
using System.Diagnostics;

namespace  CityInfo.Services 
{
    public class LocalMailService : IMailService
    {
        private string _remetente  = "admin@mycompany.com";
        private string _destinatario { get; set; } = "noreply@mycompany.com";

        public void Enviar(string assunto, string mensagem)
        {
            Debug.WriteLine($"Email enviado para {_destinatario} de {_remetente}, com o LocalMailService");
            Debug.WriteLine($"Asunto : {assunto}");
            Debug.WriteLine($"Mensagem : {mensagem}");

        }
    }   

}

