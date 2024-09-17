using AutoMapper;
using CrudDapper.Dto;
using CrudDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudDapper.Services
{
    public class UsarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<Usuario>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<Usuario> response = new ResponseModel<Usuario>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where Id = @Id", new {Id = usuarioId});

                if (usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                }
                else { 
                    //var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBanco);

                    response.Dados = usuarioBanco;
                    response.Mensagem = "Usuário localizado com sucesso!";

                }
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");

                if (usuariosBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                } else {
                    var usuarioMapeado = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);

                    response.Dados = usuarioMapeado;
                    response.Mensagem = "Usuários localizados com sucesso!";
                }
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("insert into Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                    "values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                } else
                {
                    var usuarios = await ListarUsuarios(connection);

                    var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                    response.Dados = usuariosMapeados;
                    response.Mensagem = "Usuários listados com sucesso!";
                }
            }

            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, " +
                    "Salario = @Salario, Situacao = @Situacao, CPF = @CPF where Id = @Id", usuarioEditarDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                }
                else
                {
                    var usuarios = await ListarUsuarios(connection);

                    var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                    response.Dados = usuariosMapeados;
                    response.Mensagem = "Usuários listados com sucesso!";
                }
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from Usuarios where Id = @Id", new {Id = usuarioId });

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a remoção!";
                    response.Status = false;
                }
                else
                {
                    var usuarios = await ListarUsuarios(connection);

                    var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                    response.Dados = usuariosMapeados;
                    response.Mensagem = "Usuários listados com sucesso!";
                }
            }

            return response;
        }
    }
}
