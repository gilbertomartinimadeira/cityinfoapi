using System;
using System.Collections.Generic;

namespace CityInfo.Models 
{
    public class CidadeDTO 
    {
        public int Id { get; set; }
        public String Nome { get; set; }

        public String Descricao { get; set; }

        public int QuantidadeDePontosTuristicos { 
            get 
            {
                return PontosTuristicos.Count;
            } 
        }

        public List<PontoTuristicoDTO> PontosTuristicos { get; set; } = new List<PontoTuristicoDTO>();
    }
}