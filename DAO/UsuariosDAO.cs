using System;
using System.Collections.Generic;
using System.Linq;
using TreinoAPI.Db_Context;
using TreinoAPI.DTO.Auth;
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

        public UsuariosDTO SelectUsuarioPorCredenciais(string Email, string Senha)
        {
            return DbTreino.Usuarios.Where((usuario) => usuario.Email == Email && usuario.Senha == Senha)
                                    .FirstOrDefault();
        }

        public UsuariosDTO SelectUsuarioPorEmail(string Email)
        {
            return DbTreino.Usuarios.Where((usuario) => usuario.Email == Email)
                                    .FirstOrDefault();
        }

        public List<UsuarioDisplayDTO> SelectUsuarioDisplay(int IDUsuario, Nullable<DateTime> DataAtualizacao)
        {
            return DbTreino.Usuarios.Where((usuario) => usuario.IDUsuario == IDUsuario && usuario.LastEditDate > DataAtualizacao)
                                     .Select((usuario)=> new UsuarioDisplayDTO
                                     {
                                         Foto64 = usuario.Foto64,
                                         Display = usuario.Nome  + " " + usuario.Sobrenome

                                     }).ToList();
        }

        public int InsertUsuario(RegisterDTO Register)
        {
            UsuariosDTO _usuario = PreparaUsuario(Register);

            DbTreino.Add(_usuario);
            DbTreino.SaveChanges();
            return _usuario.IDUsuario;
        }

        private UsuariosDTO PreparaUsuario(RegisterDTO Register)
        {
            UsuariosDTO _usuario = new UsuariosDTO();

            _usuario.Nome = Register.FirstName;
            _usuario.Sobrenome = Register.LastName;
            _usuario.Email = Register.Email;
            _usuario.Senha = Register.Password;
            return _usuario;
        }

        private string GetDisplayName(string Nome)
        {
            string[] names = Nome.Split(' ');

            if (names.First() == names.Last())
            {
                return names.First();

            }
            return names.First() + ' ' + names.Last();
        }
    }
}
