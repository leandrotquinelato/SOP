using System.Collections.Generic;
using SOP.Entidades;
using SOP.DAL.DAO;

namespace SOP.Domain.Logics
{
    public class OrcamentoBusiness
    {

        public List<ItemOrcamento> ObterItemOrcamento(int idOrcamento)
        {
            return OrcamentoDAO.ObterItemOrcamento(idOrcamento);
        }

        public void InserirItemOrcamento(ItemOrcamento item)
        {
            OrcamentoDAO.InserirItemOrcamento(item);
        }

        public int InserirOrcamento(Orcamento item)
        {
            return OrcamentoDAO.InserirOrcamento(item);
        }

        public void RemoveItemOrcamento(ItemOrcamento item)
        {
            OrcamentoDAO.RemoveItemOrcamento(item);
        }

        public void AtualizaItemOrcamento(ItemOrcamento item)
        {
            OrcamentoDAO.AtualizaItemOrcamento(item);
        }

        public int RecuperarUltimoId()
        {
            return OrcamentoDAO.RecuperarUltimoId();
        }

        public double RecuperarValorProduto(ItemOrcamento item)
        {
            return OrcamentoDAO.RecuperarValorProduto(item);
        }

        public double RecuperarValorTipo(ItemOrcamento item)
        {
            return OrcamentoDAO.RecuperarValorTipo(item);
        }

        public double RecuperarValorAcabamento(ItemOrcamento item)
        {
            return OrcamentoDAO.RecuperarValorAcabamento(item);
        }
    }
}
