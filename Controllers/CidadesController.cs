using System;
using System.Linq;
using System.Collections.Generic;
//using CityInfo.Models;
using Microsoft.AspNetCore.Mvc;
using CityInfo.Entities;
using CityInfo.Services;

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

            return Ok(cidades);
        }
        

        [HttpGet("{id:int}")]
        public IActionResult GetCidadePorId(int id)
        {
            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == id);

            if(cidade == null) {
                return NotFound($"Nenhuma Cidade Com id {id} foi encontrada na base");
            }
            return Ok(cidade);
        }
    }
}