using System.ComponentModel.DataAnnotations;

namespace CityInfo.Models
{
    public class PontoTuristicoParaUpdateDTO
    {
        [StringLength(5,ErrorMessage="Nome grande demais")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

    }
}