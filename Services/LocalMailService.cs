using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace  CityInfo.Services 
{
    public class LocalMailService : IMailService
    {
        private string _remetente;
        private string _destinatario;
        private IConfiguration _config;

        

        public LocalMailService(IConfiguration config)
        {
            _config = config;

        }
        public void Enviar(string assunto, string mensagem)
        {
            _destinatario = _config["MailProperties:To"];
            _remetente = _config["MailProperties:From"];

            Debug.WriteLine($"Email enviado para {_destinatario} de {_remetente}, com o LocalMailService");
            Debug.WriteLine($"Asunto : {assunto}");
            Debug.WriteLine($"Mensagem : {mensagem}");        
        }
    }   

}

