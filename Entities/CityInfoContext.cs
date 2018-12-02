using Microsoft.EntityFrameworkCore;

namespace CityInfo.Entities 
{
    public class CityInfoContext : DbContext 
    {    
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options){ }
        public DbSet<Cidade> Cidades {get;set;}
        public DbSet<PontoTuristico> PontosTuristicos {get;set;}

    }
}