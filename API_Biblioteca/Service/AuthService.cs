using API_Biblioteca.Data;
using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.Service
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaService;

        public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaService = senhaInterface;
        }
        public async Task<UsuarioCriacaoDTO> Registrar(UsuarioCriacaoDTO usuarioRegistro)
        {
            if (!VerificaUsuarioeEmail(usuarioRegistro))
            {
                throw new BadRequestException("Nome de usuário ou email já existe");
            }
            _senhaService.CriarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            UsuarioModel usuario = new UsuarioModel()
            {
                NomeUsuario = usuarioRegistro.NomeUsuario,
                Email = usuarioRegistro.Email,
                Cargo = usuarioRegistro.Cargo,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt

            };
            _context.Add(usuario);
            await _context.SaveChangesAsync();

            throw new BadRequestException("Erro ao registrar usuário");
        }

        public async Task<UsuarioModel> Login(UsuarioLoginDTO UsuarioLogin)
        {   
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(uBanco => uBanco.Email == UsuarioLogin.Email);
            if(usuario == null)
            {
                throw new BadRequestException("Usuário incorreto");
            }
                
            if(!_senhaService.VerificaSenhaHash(UsuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                throw new BadRequestException("Senha incorreta");
            }

            var token = _senhaService.CriarToken(usuario);
            

            return usuario;
            throw new BadRequestException("Erro ao realizar login");
        }
        public bool VerificaUsuarioeEmail(UsuarioCriacaoDTO usuarioRegistro)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == usuarioRegistro.Email || u.NomeUsuario == usuarioRegistro.NomeUsuario);
            if(usuario != null)
            {
                throw new BadRequestException("Nome de usuário ou email já existe");
            }
            _senhaService.CriarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);
            return true;
        }
    }
}
