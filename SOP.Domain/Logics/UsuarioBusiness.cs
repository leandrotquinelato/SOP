using System.Collections.Generic;
using SOP.Entidades;
using SOP.DAL.DAO;

namespace SOP.Domain.Logics
{
    public class UsuarioBusiness
    {

        public List<Usuario> ObterUsuarios()
        {
            return UsuarioDAO.ObterUsuarios();
        }

        public void InserirUsuario(Usuario item)
        {
            UsuarioDAO.InserirUsuario(item);
        }

        public void RemoveUsuario(Usuario item)
        {
            UsuarioDAO.RemoveUsuario(item);
        }

        public void AtualizaUsuario(Usuario item)
        {
            UsuarioDAO.AtualizaUsuario(item);
        }
    }
}
