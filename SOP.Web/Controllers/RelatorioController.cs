using Kendo.Mvc.UI;
using SOP.Web.Controllers.Abstratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SOP.Entidades;
using SOP.Domain.Logics;
using System.Collections;
using System.Security.Claims;
using SOP.Web.Controllers.CustomFilters;


namespace SOP.Web.Controllers.Cadastro
{
    public class RelatorioController : BaseController
    {
        ClienteBusiness clienteBusiness = new ClienteBusiness();
        RelatorioBusiness relatorioBusiness = new RelatorioBusiness();

        public RelatorioController()
        {
            List<Cliente> listaCliente = clienteBusiness.ObterCliente();
            var produtos = listaCliente.Select(i => new { id = i.Id_Cliente, descricao = i.Nm_Cliente });
            ViewData["ClienteControle_Data"] = new SelectList(produtos, "id", "descricao");
        }

        public ActionResult Consultar()
        {
            return View("~/Views/Relatorio/Index.cshtml");
        }

        public ActionResult CarregarGridOrcamento([DataSourceRequest] DataSourceRequest request)
        {
            var numOrcamento = (Request.Params["numOrcamento"]);
            var cliente = (Request.Params["cliente"]);
            var dataInicio = (Request.Params["dataInicio"]);
            var dataFim = (Request.Params["dataFim"]);
            List<Orcamento> listaOrcamento = new List<Orcamento>();

            if (string.IsNullOrEmpty(dataInicio) || string.IsNullOrEmpty(dataFim))
            {
                ModelState.AddModelError("", "O preenchimento das datas é obrigatório.");
                return Json(listaOrcamento.ToList().ToDataSourceResult(request, ModelState));
            }
            else
            {
                DateTime dataIni = Convert.ToDateTime(dataInicio);
                DateTime dataFinal = Convert.ToDateTime(dataFim);
                if (dataIni > dataFinal)
                {
                    ModelState.AddModelError("", "A data de início não pode ser maior que a data final.");
                    return Json(listaOrcamento.ToList().ToDataSourceResult(request, ModelState));
                }
                else
                {
                    try
                    {
                        listaOrcamento = relatorioBusiness.CarregarGridOrcamento(dataIni, dataFinal, numOrcamento, cliente);
                        return Json(listaOrcamento.ToList().ToDataSourceResult(request, ModelState));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return Json(listaOrcamento.ToList().ToDataSourceResult(request, ModelState));
                    }
                }
            }

        }

        public FileResult Exportar([DataSourceRequest] DataSourceRequest request, string arquivo, string colunas)
        {
            var lista = clienteBusiness.ObterCliente();
            return base.Exportar(request, lista != null ? (IEnumerable)lista : null, arquivo, colunas);
        }

    }
}