using System.Linq;
using TreinoAPI.Db_Context;
using TreinoAPI.DTO.Usuarios;

namespace TreinoAPI.DAO
{
    public class UsuariosDAO
    {
        TreinoConnectionString DbTreino = new TreinoConnectionString();

        public UsuariosDTO SelectUsuario(int IDUsuario)
        {
            return DbTreino.Usuarios.Where((usuario) => usuario.IDUsuario == IDUsuario)
                                    .FirstOrDefault();
        }

        public UsuariosDTO SelectUsuarioPorCredenciais(string Usuario, string Senha)
        {
            return DbTreino.Usuarios.Where((usuario) => usuario.Usuario == Usuario && usuario.Senha == Senha)
                                    .FirstOrDefault();
        }
    }
}
