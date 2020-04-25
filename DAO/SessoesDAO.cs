using ApresentacoesAPI.DTO.Auth;
using TreinoAPI.Db_Context;

namespace TreinoAPI.DAO
{
    public class SessoesDAO
    {
        private TreinoConnectionString DbTreino = new TreinoConnectionString();

        public void InsertSessao(SessoesDTO Sessao)
        {
            DbTreino.Add(Sessao);
            DbTreino.SaveChanges();
        }
    }
}
