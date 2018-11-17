using System;
using System.Collections.Generic;
using CityInfo.Models;

namespace CityInfo 
{
    public class CidadesDataStore 
    {
        
        public static List<CidadeDTO> Cidades { get; } = new List<CidadeDTO>() 
            {
                new CidadeDTO 
                {
                    Id = 1,
                    Nome = "Duque de Caxias",
                    Descricao = "Lugar Feio, mas Belford Roxo é pior",
                    PontosTuristicos = new List<PontoTuristicoDTO> {
                        new PontoTuristicoDTO 
                        {
                            Id = 1, 
                            Nome = "Praça da Apoteose",
                            Descricao = "Melhor Praça de Duque de Caxias"
                        },
                        new PontoTuristicoDTO 
                        {
                            Id = 2, 
                            Nome = "Vila Olímpica",
                            Descricao = "Lugar pra praticar esportes em caxias"
                        },
                    }
                },
                new CidadeDTO 
                {
                    Id = 2,
                    Nome = "Belford Roxo",
                    Descricao = "Pense num lugar horrível ..."
                },
                new CidadeDTO 
                {
                    Id = 3,
                    Nome = "Rio de Janeiro",
                    Descricao = "Só venha se tiver colete."
                },
                new CidadeDTO 
                {
                    Id = 4,
                    Nome = "Niterói",
                    Descricao = "Do Outro lado da poça."
                }
            };            
    }
}