using System.Collections.Generic;
using SOP.Entidades;
using SOP.DAL.DAO;

namespace SOP.Domain.Logics
{
    public class ClienteBusiness
    {

        public List<Cliente> ObterCliente()
        {
            return ClienteDAO.ObterCliente();
        }

        public void InserirCliente(Cliente item)
        {
            ClienteDAO.InserirCliente(item);
        }

        public void RemoveCliente(Cliente item)
        {
            ClienteDAO.RemoveCliente(item);
        }

        public void AtualizaCliente(Cliente item)
        {
            ClienteDAO.AtualizaCliente(item);
        }
    }
}
