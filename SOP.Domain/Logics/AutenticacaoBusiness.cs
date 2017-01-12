using System.Collections.Generic;
using SOP.Entidades;
using SOP.DAL.DAO;

namespace SOP.Domain.Logics
{
    public class AutenticacaoBusiness
    {
        public string ValidaUsuarioLogin(string usuario, string senha)
        {
            return AutenticacaoDAO.ValidaUsuarioLogin(usuario, senha);
        }
    }
}
