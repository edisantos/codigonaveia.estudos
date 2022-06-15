using edylemos.sistemamaster.estudos.Domain.Entidades;
using edylemos.sistemamaster.estudos.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace edylemos.sistemamaster.estudos.Api.Controllers
{
    [Route("api/v1/Usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarios;

        public UsuarioController(IUsuario usuarios)
        {
            _usuarios = usuarios;
        }
        [HttpPost]
        [Route("Registrar")]
        public IActionResult Registrar(Usuarios mod)
        {
            try
            {
                _usuarios.Registrar(mod);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Created("",mod);
        }
        [HttpGet]
        [Route("ObterTodosUsuarios")]
        public IActionResult ObterTodosUsuarios()
        {
            var result = _usuarios.ObterTodosUsuario().ToList();
            return Ok(result);
        }
        [HttpDelete]
        [Route("Excluir/{Id}")]
        public IActionResult Excluir(int Id)
        {
            if(Id == 0)
            {
                return BadRequest("Usuário não encontrado");
            }
            _usuarios.Excluir(Id);
            return Ok("Usuário excluido com sucesso!");
        }
        [HttpPut]
        [Route("Alterar/{Id:int}")]
        public IActionResult Alterar(Usuarios usuarios)
        {
            //var Id = _usuarios.ObterUsuarioPorId(usuarios.Id);
            try
            {
                //    if (Id != usuarios.Id)
                //    {
                //        BadRequest("Houve um erro ao tentar alterar o usuário");
                //    }
                //    var userUpdate = _usuarios.ObterUsuarioPorId(Id);
                //    if(userUpdate == null)
                //    {
                //        return NotFound($"O Usuário com o Id {Id} não foi encontrado");
                //    }

                var us = new Usuarios
                {
                    Id = usuarios.Id,
                    PrimeiroNome = usuarios.PrimeiroNome,
                    SegundoNome = usuarios.SegundoNome,
                    Email = usuarios.Email,
                    Usuario = usuarios.Usuario
                   
                    
                };
               _usuarios.Alterar(usuarios);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao conectar com o banco de dados. Detalhes: {ex.Message}");
            }
           
           
            
           return Ok("Usuario alterado com sucesso!");
        }
        [HttpGet]
        [Route("ObterUsuarioPorId/{Id}")]
        public IActionResult ObterUsuarioPorId(int Id)
        {
            var result = _usuarios.ObterUsuarioPorId(Id);
            if(result == null)
            {
                return BadRequest("Houve um erro");
            }
            return Ok(result);
        }
    }
}
