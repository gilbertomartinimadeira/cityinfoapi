using System;
using System.Linq;
using System.Collections.Generic;
//using CityInfo.Models;
using Microsoft.AspNetCore.Mvc;
using CityInfo.Entities;
using CityInfo.Services;
using CityInfo.Models;

namespace CityInfo.Controllers 
{
    [Route("api/cidades")]
    public class CidadesController : ControllerBase 
    {
        private ICidadeRepository _repositorio;

        public CidadesController(ICidadeRepository repositorio )
        {                
            _repositorio = repositorio;
        }

        public IActionResult GetCidades()
        {
        
            var cidades = _repositorio.ObterCidades();

            var cidadesDTO = AutoMapper.Mapper.Map<List<CidadeSemPontosturisticosDTO>>(cidades);


            return Ok(cidadesDTO);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetCidadePorId(int id, bool incluiPontosTuristicos = false)
        {        
            var cidade = _repositorio.ObterCidade(id,incluiPontosTuristicos);

            if(cidade == null) {
                return NotFound($"Nenhuma Cidade Com id {id} foi encontrada na base");
            }

            if(incluiPontosTuristicos) {                        

                var cidadeDTO = AutoMapper.Mapper.Map<CidadeDTO>(cidade);                        
                return Ok(cidadeDTO);

            } else {

                var cidadeSemPontosturisticosDTO = AutoMapper.Mapper.Map<CidadeSemPontosturisticosDTO>(cidade);                            
                return Ok(cidadeSemPontosturisticosDTO);
            }        
        }
    }
}