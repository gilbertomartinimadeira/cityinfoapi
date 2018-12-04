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

       

    }
}