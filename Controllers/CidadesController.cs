using System;
using System.Linq;
using System.Collections.Generic;
using CityInfo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers 
{
    [Route("api/cidades")]
    public class CidadesController : ControllerBase 
    {
        public IActionResult GetCidades()
        {
            return Ok(CidadesDataStore.Cidades);
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