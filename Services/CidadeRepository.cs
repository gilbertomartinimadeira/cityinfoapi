using System.Linq;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Services 
{
    public class CidadeRepository : ICidadeRepository
    {
        private CityInfoContext _context;

        public CidadeRepository(CityInfoContext context)
        {
            _context = context;
            
            if(!_context.Cidades.Any())
            {
                _context.PreencherDadosIniciais();
            }
        }


        public IQueryable<Cidade> ObterCidades()
        {
            return _context.Cidades.AsQueryable();
        }

        public Cidade ObterCidade(int id, bool incluiPontosTuristicos)
        {
            if(incluiPontosTuristicos) 
            {
                return _context.Cidades.Include(c => c.PontosTuristicos)
                                       .FirstOrDefault(c => c.Id == id);
            }

            return _context.Cidades.FirstOrDefault(c => c.Id == id);


        }


        public IQueryable<PontoTuristico> ObterPontosTuristicosDaCidade(int idCidade)
        {
            var cidade = _context.Cidades.SingleOrDefault(c => c.Id == idCidade);

            return cidade.PontosTuristicos.AsQueryable();

        }

        public PontoTuristico ObterPontoTuristicoDaCidade(int idCidade, int id)
        {
            return _context.PontosTuristicos
                           .Where(p => p.Id == id && p.CidadeId == idCidade)
                           .FirstOrDefault();
        }
    }
}