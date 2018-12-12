using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using CityInfo.Models;
using Microsoft.Extensions.Logging;
using CityInfo.Services;
using CityInfo.Entities;

namespace CityInfo.Controllers 
{
    [Route("api/cidades/{idCidade}/pontosturisticos")]
    public class PontosTuristicosController : ControllerBase 
    {
        
        private ILogger<PontosTuristicosController> _logger;
        private IMailService _mailService;
        private ICidadeRepository _repository;

        public PontosTuristicosController(ILogger<PontosTuristicosController> logger, IMailService mailService, ICidadeRepository repository)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
        }

        [HttpGet()]    
        public IActionResult GetPontosTuristicosDaCidade(int idCidade)
        {
            try{
                if(!_repository.CidadeExiste(idCidade))
                {
                    _logger.LogInformation($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
                    return NotFound();
                }            

                var pontosTuristicos = _repository.ObterPontosTuristicosDaCidade(idCidade);
                var pontosTuristicosDTO = AutoMapper.Mapper.Map<List<PontoTuristicoDTO>>(pontosTuristicos);
            

                return Ok(pontosTuristicosDTO);


            } catch (Exception ex){
                _logger.LogCritical($" *** Exceção ao tentar obter pontos turísticos para a cidade {idCidade} ***",ex);            
                return StatusCode(500, "Um Problema ocorreu ao tentar processar sua requisição.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterPontoTuristico")]    
        public IActionResult GetPontoTuristicoDaCidadePorId(int idCidade, int id)
        {
           

            if(!_repository.CidadeExiste(idCidade))
            {
                return NotFound();
            }

            var pontoTuristico = _repository.ObterPontoTuristicoDaCidade(idCidade,id);

            if(pontoTuristico == null)
            {
                return NotFound();
            }

            var pontoTuristicoDTO = AutoMapper.Mapper.Map<PontoTuristicoDTO>(pontoTuristico);

            return Ok(pontoTuristicoDTO);


        }

        [HttpPost]
        public IActionResult CriarPontoTuristico(int idCidade, [FromBody]PontoTuristicoNovoDTO pontoTuristico)
        {
            if(pontoTuristico == null)
            {
                return BadRequest("Objeto para criação inválido");
            }

            if(!_repository.CidadeExiste(idCidade))
            {
                return NotFound($"Nenhuma Cidade Com id {idCidade} foi encontrada na base");
            }

            var cidade = _repository.ObterCidade(idCidade, false);



            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            

            var novoPontoTuristico = AutoMapper.Mapper.Map<PontoTuristico>(pontoTuristico);

            _repository.AdicionaPontoTuristicoNaCidade(idCidade,novoPontoTuristico);        

            

            if(!_repository.Salvar())
            {
                return StatusCode(500,"Não foi possível persistir o ponto turístico");
            }

            var pontoTuristicoCriadoDTO = AutoMapper.Mapper.Map<PontoTuristicoDTO>(novoPontoTuristico);


            return CreatedAtRoute("ObterPontoTuristico",new {idCidade = idCidade,id = pontoTuristicoCriadoDTO.Id }, pontoTuristicoCriadoDTO);
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

            if(!_repository.CidadeExiste(idCidade))
            {
                return NotFound();
            }
            
            var pontoTuristicoArmazenado = _repository.ObterPontoTuristicoDaCidade(idCidade,id);

            if(pontoTuristicoArmazenado == null)
            {
                return NotFound($"Ponto turístico com o id {id} não foi encontrado para a cidade {idCidade}");
            }

            AutoMapper.Mapper.Map(pontoTuristico, pontoTuristicoArmazenado);        

            if(!_repository.Salvar())
            {
                return StatusCode(500,"Não foi possível persistir o ponto turístico");
            }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult AtualizarPontoTuristicoParcialmente(int idCidade, int id, [FromBody]JsonPatchDocument<PontoTuristicoParaUpdateDTO> patchDocument)
        {
            if(patchDocument == null){
                return BadRequest("patchDocument invalido");
            }        

            if(!_repository.CidadeExiste(idCidade))
            {
                return NotFound("Cidade não encontrada, verificar o ID");
            }
            
            var pontoTuristicoArmazenado = _repository.ObterPontoTuristicoDaCidade(idCidade,id);

            if(pontoTuristicoArmazenado == null) 
            {
                return NotFound();
            }

            var pontoTuristicoParaPatch = AutoMapper.Mapper.Map<PontoTuristicoParaUpdateDTO>(pontoTuristicoArmazenado);                

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

            AutoMapper.Mapper.Map(pontoTuristicoParaPatch, pontoTuristicoArmazenado );

            if(!_repository.Salvar())
            {
                return StatusCode(500, "Erro durante o processamento da requisição");
            }

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public IActionResult ExcluirPontoTuristicoPorId(int idCidade, int id)
        {        

            if(!_repository.CidadeExiste(idCidade))
            {
                return NotFound("Cidade não encontrada, verificar o ID");
            }
            
            var pontoTuristicoArmazenado = _repository.ObterPontoTuristicoDaCidade(idCidade,id);

            if(pontoTuristicoArmazenado == null) 
            {
                return NotFound();
            }

            _repository.ExcluirPontoTuristicoPorId(id);

            if(!_repository.Salvar())
            {
                return StatusCode(500, "Um erro ocorreu durante o processamento da requisição");
            }

            _mailService.Enviar($"Ponto turístico removido", $@"O Ponto turístico  {pontoTuristicoArmazenado.Nome} 
                                                                com Id {pontoTuristicoArmazenado.Id} foi excluído da base.");
            
            return NoContent();
        }
    }
} 