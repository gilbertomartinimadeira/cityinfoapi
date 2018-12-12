using System.Collections.Generic;
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


        public IEnumerable<PontoTuristico> ObterPontosTuristicosDaCidade(int idCidade)
        {
            var cidade = _context.Cidades.Include(c => c.PontosTuristicos)
                                         .SingleOrDefault(c => c.Id == idCidade);

            return cidade.PontosTuristicos;

        }

        public PontoTuristico ObterPontoTuristicoDaCidade(int idCidade, int id)
        {
            return _context.PontosTuristicos
                           .Where(p => p.Id == id && p.CidadeId == idCidade)
                           .FirstOrDefault();
        }
        
        public void AdicionaPontoTuristicoNaCidade(int idCidade, PontoTuristico pontoTuristico)
        {
            var cidade = ObterCidade(idCidade, false);
            cidade.PontosTuristicos.Add(pontoTuristico);
            

        }
        

        public bool CidadeExiste(int idCidade)
        {
            return _context.Cidades.Any(c => c.Id == idCidade);
        }

        public void ExcluirPontoTuristicoPorId(int id)
        {
            var pontoTuristico = _context.PontosTuristicos.First(p => p.Id == id);

            _context.PontosTuristicos.Remove(pontoTuristico);
            
        }

        public bool Salvar()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}