using CrudDapper.Dto;
using CrudDapper.Models;
using CrudDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int usuarioId)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);

            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(usuarioCriarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuarios = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }
    }
}
