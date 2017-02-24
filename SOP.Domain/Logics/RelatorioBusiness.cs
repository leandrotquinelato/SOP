using System.Collections.Generic;
using SOP.Entidades;
using SOP.DAL.DAO;
using System;

namespace SOP.Domain.Logics
{
    public class RelatorioBusiness
    {

        public List<Orcamento> CarregarGridOrcamento(DateTime dataIni, DateTime dataFinal, string numOrcamento, string cliente)
        {
            return RelatorioDAO.CarregarGridOrcamento(dataIni, dataFinal, numOrcamento, cliente);
        }             
    }
}
