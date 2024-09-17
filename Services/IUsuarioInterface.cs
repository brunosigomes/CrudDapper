using CrudDapper.Dto;
using CrudDapper.Models;

namespace CrudDapper.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios();
        Task<ResponseModel<Usuario>> BuscarUsuarioPorId(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId);
    }
}
