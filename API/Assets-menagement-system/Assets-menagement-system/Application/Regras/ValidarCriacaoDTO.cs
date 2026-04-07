using Assets_menagement_system.Exceptions;

namespace Assets_menagement_system.Application.Regras
{
    public class ValidarCriacaoDTO
    {
        public static void ValidarNome(string nome)
        {
            if (nome == null || string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome é obrigatório!");
            }
        }   

        public static void ValidarSenha(string senha)
        {
            if(string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória!");
            }

        }
    }
}
