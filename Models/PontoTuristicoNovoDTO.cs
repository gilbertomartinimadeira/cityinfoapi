using System;
using System.ComponentModel.DataAnnotations;


namespace CityInfo.Models 
{
    public class PontoTuristicoNovoDTO
    {    
        [Required]
        [MinLength(3, ErrorMessage="O Nome informado é muito curto")]
        public String Nome { get; set; }
        [Required(ErrorMessage="O Campo 'Descrição' é obrigatório")]
        public String Descricao { get; set; }

    }
}