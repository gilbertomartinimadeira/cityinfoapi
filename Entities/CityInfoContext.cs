using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.Entities 
{
    public class CityInfoContext : DbContext 
    {    
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options){ }
        public DbSet<Cidade> Cidades {get;set;}
        public DbSet<PontoTuristico> PontosTuristicos {get;set;}

        ///summary é usado para fins de prototipação
        public void PreencherDadosIniciais()
        {
            var cidades = new List<Cidade>() 
            {
                new Cidade
                {
                    Id = 1,
                    Nome = "Duque de Caxias",
                    Descricao = "Lugar Feio, mas Belford Roxo é pior"
                   
                },
                new Cidade 
                {
                    Id = 2,
                    Nome = "Belford Roxo",
                    Descricao = "Pense num lugar horrível ..."
                },
                new Cidade 
                {
                    Id = 3,
                    Nome = "Rio de Janeiro",
                    Descricao = "Só venha se tiver colete."
                },
                new Cidade 
                {
                    Id = 4,
                    Nome = "Niterói",
                    Descricao = "Do Outro lado da poça."
                }
            };            

            var pontosTuristicos = new List<PontoTuristico> {
                new PontoTuristico
                {
                    Id = 1, 
                    Nome = "Praça da Apoteose",
                    Descricao = "Melhor Praça de Duque de Caxias"
                },
                new PontoTuristico
                {
                    Id = 2, 
                    Nome = "Vila Olímpica",
                    Descricao = "Lugar pra praticar esportes em caxias"
                },
            };

            cidades[0].PontosTuristicos = pontosTuristicos;

            this.Cidades.AddRange(cidades);

            this.SaveChanges();
        
        }

    }
}