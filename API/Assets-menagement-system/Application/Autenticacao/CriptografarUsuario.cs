using System.Security.Cryptography;
using System.Text;

namespace Assets_menagement_system.Application.Autenticacao
{
    public class CriptografarUsuario
    {

        public static byte[] CriptografarSenha(string senha)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] byteSenha = Encoding.UTF8.GetBytes(senha);
            byte[] senhaCriptografada = sha256.ComputeHash(byteSenha);

            return senhaCriptografada;
        }
    }
}
