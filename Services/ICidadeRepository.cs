using System.Collections.Generic;
using System.Linq;
using CityInfo.Entities;

namespace CityInfo.Services 
{
    public interface ICidadeRepository
    {
        IQueryable<Cidade> ObterCidades();    

        Cidade ObterCidade(int id, bool incluiPontosTuristicos);

        IQueryable<PontoTuristico> ObterPontosTuristicosDaCidade(int idCidade);

        PontoTuristico ObterPontoTuristicoDaCidade(int idCidade, int id);


    }
}