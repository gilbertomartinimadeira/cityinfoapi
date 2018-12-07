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
            List<CidadeSemPontosturisticosDTO> cidadesDTO = new List<CidadeSemPontosturisticosDTO>();


            foreach(var c in cidades)
            {
                cidadesDTO.Add(new CidadeSemPontosturisticosDTO{
                    Id = c.Id,
                    Nome = c.Nome, 
                    Descricao = c.Descricao,            
                });
            }

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

                var cidadeDTO = new CidadeDTO(){
                    Id = cidade.Id,
                    Nome = cidade.Nome,
                    Descricao = cidade.Descricao                
                };

                List<PontoTuristicoDTO> pontosTuristicosDTO = new List<PontoTuristicoDTO>();

                foreach(var p in cidade.PontosTuristicos)
                {
                    pontosTuristicosDTO.Add(new PontoTuristicoDTO(){
                        Id = p.Id,
                        Nome = p.Nome,
                        Descricao = p.Descricao                    
                    });
                }

                cidadeDTO.PontosTuristicos = pontosTuristicosDTO;

                return Ok(cidadeDTO);

            } else {
                 var cidadeSemPontoTuristicoDTO = new CidadeSemPontosturisticosDTO(){
                    Id = cidade.Id,
                    Nome = cidade.Nome,
                    Descricao = cidade.Descricao                
                };

                return Ok(cidadeSemPontoTuristicoDTO);
            }        
        }
    }
}