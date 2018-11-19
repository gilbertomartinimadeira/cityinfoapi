using System;
using System.Linq;
using System.Collections.Generic;
using CityInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.Controllers 
{
    [Route("api/cidades/{idCidade}/pontosturisticos")]
    public class PontosTuristicosController : ControllerBase 
    {
        [HttpGet()]
        public IActionResult GetPontosTuristicosDaCidade(int idCidade)
        {
            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null) {
                return NotFound($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
            }

            var pontosTuristicos = cidade.PontosTuristicos;
            
            return Ok(pontosTuristicos);
        }

        [HttpGet("{id:int}", Name = "ObterPontoTuristico")]
        
        public IActionResult GetPontoTuristicoDaCidadePorId(int idCidade, int id)
        {
            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null) {
                return NotFound($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
            }

            var pontoTuristico = cidade.PontosTuristicos.FirstOrDefault(p => p.Id == id);

            return Ok(pontoTuristico);
        }

        [HttpPost]
        public IActionResult CriarPontoTuristico(int idCidade, [FromBody]PontoTuristicoNovoDTO pontoTuristico)
        {
            if(pontoTuristico == null)
            {
                return BadRequest("Objeto para criação inválido");
            }

            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null) {
                return NotFound($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
            }

            // if(pontoTuristico.Nome == null) 
            // {
            //     ModelState.AddModelError("Nome","O Nome do ponto turístico não foi encontrado");
            // }

            // if(pontoTuristico.Descricao == null) 
            // {
            //     ModelState.AddModelError("Descricao","A Descrição do ponto turístico não foi encontrada");
            // }

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            
            var pontosTuristicos = CidadesDataStore.Cidades.SelectMany(c => c.PontosTuristicos);

            var idMaxParaPontoTuristico = pontosTuristicos.Max(p => p.Id);       


            var novoPontoTuristico = new PontoTuristicoDTO() {
                Id = ++idMaxParaPontoTuristico,
                Nome = pontoTuristico.Nome,
                Descricao = pontoTuristico.Descricao    
            };
            
            cidade.PontosTuristicos.Add(novoPontoTuristico);

            return CreatedAtRoute("ObterPontoTuristico",new {idCidade = idCidade,id = novoPontoTuristico.Id }, novoPontoTuristico);
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizarPontoTuristico(int idCidade, int id, [FromBody]PontoTuristicoParaUpdateDTO pontoTuristico)
        {

            if(pontoTuristico == null)
            {
                return BadRequest("Objeto para atualização inválido");
            }
                        
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null) 
            {
                return NotFound($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
            }       

            var pontoTuristicoArmazenado = cidade.PontosTuristicos.FirstOrDefault(p => p.Id == id);

            if(pontoTuristicoArmazenado == null)
            {
                return NotFound($"Ponto turístico com o id {id} não foi encontrado para a cidade {idCidade}");
            }

            pontoTuristicoArmazenado.Nome = pontoTuristico.Nome;
            pontoTuristicoArmazenado.Descricao = pontoTuristico.Descricao;


            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult AtualizarPontoTuristicoParcialmente(int idCidade, int id, [FromBody]JsonPatchDocument<PontoTuristicoParaUpdateDTO> patchDocument)
        {
            if(patchDocument == null){
                return BadRequest("patchDocument invalido");
            }

            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null)
            {
                return NotFound("Cidade não encontrada, verificar o ID");
            }
            
            var pontoTuristicoArmazenado = cidade.PontosTuristicos.FirstOrDefault(p => p.Id == id);

            if(pontoTuristicoArmazenado == null) 
            {
                return NotFound();
            }

            var pontoTuristicoParaPatch = new PontoTuristicoParaUpdateDTO(){
                Nome = pontoTuristicoArmazenado.Nome,
                Descricao = pontoTuristicoArmazenado.Descricao
            };

            if(pontoTuristicoParaPatch.Nome == pontoTuristicoParaPatch.Descricao)
            {
                ModelState.AddModelError("Nome", "Nome e Descrição não podem ser iguais");
            }

            TryValidateModel(pontoTuristicoParaPatch);


            patchDocument.ApplyTo(pontoTuristicoParaPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pontoTuristicoArmazenado.Nome = pontoTuristicoParaPatch.Nome;
            pontoTuristicoArmazenado.Descricao = pontoTuristicoParaPatch.Descricao;

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public IActionResult ExcluirPontoTuristicoPorId(int idCidade, int id)
        {

            var cidade = CidadesDataStore.Cidades.FirstOrDefault(c => c.Id == idCidade);

            if(cidade == null)
            {
                return NotFound("Cidade não encontrada, verificar o ID");
            }
            
            var pontoTuristicoArmazenado = cidade.PontosTuristicos.FirstOrDefault(p => p.Id == id);

            if(pontoTuristicoArmazenado == null) 
            {
                return NotFound();
            }

            cidade.PontosTuristicos.Remove(pontoTuristicoArmazenado);

            return NoContent();
        }
    }
}