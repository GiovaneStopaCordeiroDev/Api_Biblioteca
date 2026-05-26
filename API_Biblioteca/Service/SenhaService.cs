using API_Biblioteca.Errors;
using API_Biblioteca.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_Biblioteca.Service
{
    public class SenhaService : ISenhaInterface
    {
        private readonly IConfiguration _config;
        public SenhaService(IConfiguration config)
        {
            _config = config;
        }
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // Chave que precisamos ter para criar a senha Hash
                senhaSalt = hmac.Key;

                // senha Criptografada
                senhaHash = hmac.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(senha)
                );
            }
        }

        public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                // Senha criptografada a ser comparada
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

                // Comparação entre a senha criptografada e a senha hash armazenada
                return computedHash.SequenceEqual(senhaHash);

            }
        }
        public string CriarToken(UsuarioModel usuario)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Cargo", usuario.Cargo.ToString()),   
                new Claim("Email", usuario.Email),
                new Claim("Usuario", usuario.NomeUsuario)
            };
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}


