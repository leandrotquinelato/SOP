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
    public class OrcamentoController : BaseController
    {
        ClienteBusiness clienteBusiness = new ClienteBusiness();
        ProdutoBusiness produtoBusiness = new ProdutoBusiness();
        AcabamentoBusiness acabamentoBusiness = new AcabamentoBusiness();
        TipoPedraBusiness tipoPedraBusiness = new TipoPedraBusiness();
        OrcamentoBusiness orcamentoBusiness = new OrcamentoBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();
        Orcamento viewModel = null;

        public OrcamentoController()
        {
            List<Produto> listaProduto = produtoBusiness.ObterProduto();
            var produtos = listaProduto.Select(i => new { id = i.Id_Produto, descricao = i.Nm_Produto });
            ViewData["ProdutoControle_Data"] = new SelectList(produtos, "id", "descricao");

            List<Acabamento> listaAcabamento = acabamentoBusiness.ObterAcabamento();
            var acabamento = listaAcabamento.Select(i => new { id = i.Id_Acabamento, descricao = i.Nm_Acabamento });
            ViewData["AcabamentoControle_Data"] = new SelectList(acabamento, "id", "descricao");

            List<TipoPedra> listaTipoPedra = tipoPedraBusiness.ObterTipoPedra();
            var tipoPedra = listaTipoPedra.Select(i => new { id = i.Id_TpPedra, descricao = i.Nm_TpPedra });
            ViewData["TipoPedraControle_Data"] = new SelectList(tipoPedra, "id", "descricao");
        }

        public ActionResult Consultar()
        {
            viewModel = new Orcamento();
            Session["idOrcamento"] = null;
            return View("~/Views/Orcamento/Index.cshtml", viewModel);
        }

        public ActionResult ObterCliente([DataSourceRequest] DataSourceRequest request)
        {
            var listaClientes = clienteBusiness.ObterCliente().Select(i => new { Id = i.Id_Cliente, Descricao = i.Nm_Cliente });

            return Json(listaClientes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, JsonExceptionFilter]
        public ActionResult Salvar(Orcamento viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Orcamento/Index.cshtml", viewModel);
                }
                else
                {
                    if (viewModel.Id_Orcamento_Capa == 0 && Session["idOrcamento"] == null)
                    {
                        ModelState.AddModelError("", "É necessário inserir itens para salvar o orçamento.");

                        return View("~/Views/Orcamento/Index.cshtml", viewModel);
                    }
                    else
                    {
                        if (viewModel.Id_Orcamento_Capa == 0)
                            viewModel.Id_Orcamento_Capa = (int)Session["idOrcamento"];

                        int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                        if (codigoUsuario > 0)
                            viewModel.Cd_Usua_Altr = codigoUsuario;
                        else
                            viewModel.Cd_Usua_Altr = null;

                        double valorTotalItens = orcamentoBusiness.RecuperarValorTotalItensOrcamento(viewModel.Id_Orcamento_Capa);

                        viewModel.ValorTotal = valorTotalItens + viewModel.Frete;

                        orcamentoBusiness.AtualizarOrcamento(viewModel);

                        TempData["successMessage"] = "Orçamento " + viewModel.Id_Orcamento_Capa + " salvo com sucesso;";
                        ModelState.Clear();
                        Session["idOrcamento"] = null;
                        viewModel = new Orcamento();

                        return View("~/Views/Orcamento/Index.cshtml", viewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View("~/Views/Orcamento/Index.cshtml", viewModel);
            }
        }

        [HttpPost, JsonExceptionFilter]
        public ActionResult GerarIdOrcamento(string cliente)
        {
            Orcamento viewModel = new Orcamento();
            if (!string.IsNullOrEmpty(cliente))
            {
                int idOrcamento = 0;
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (Session["idOrcamento"] != null)
                {
                    idOrcamento = (int)Session["idOrcamento"];
                    viewModel.Id_Cliente = Convert.ToInt32(cliente);
                    viewModel.Id_Orcamento_Capa = idOrcamento;
                }
                else
                {
                    viewModel.Cd_Usua_Rgst = codigoUsuario;
                    viewModel.Id_Cliente = Convert.ToInt32(cliente);
                    idOrcamento = orcamentoBusiness.InserirOrcamento(viewModel);
                    Session["idOrcamento"] = idOrcamento;
                    viewModel.Id_Orcamento_Capa = idOrcamento;
                }

                return Json(viewModel);
            }
            else
            {
                throw new Exception("Informar uma cliente válido.");
            }
        }

        public ActionResult EditarOrcamento(string id)
        {
            Orcamento retorno = new Orcamento();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao acesso o orçamenro, tente novamente.");

                    return View("~/Views/Orcamento/Index.cshtml", retorno);
                }
                else
                {
                    retorno = orcamentoBusiness.CarregarOrcamento(id);
                    Session["idOrcamento"] = Convert.ToInt32(id);
                    return View("~/Views/Orcamento/Index.cshtml", retorno);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View("~/Views/Orcamento/Index.cshtml", retorno);
            }
        }

        public double RecuperarValorTotal(ItemOrcamento item)
        {
            double valorProduto = 0;
            double valorTipoPedra = 0;
            double valorAcabamento = 0;

            if (item != null && item.Id_Pedra > 0)
                valorProduto = orcamentoBusiness.RecuperarValorProduto(item);

            if (item != null && item.Id_Tipo_Pedra > 0)
                valorTipoPedra = orcamentoBusiness.RecuperarValorTipo(item);

            if (item != null && item.Id_Tipo_Acabamento > 0)
                valorAcabamento = orcamentoBusiness.RecuperarValorAcabamento(item);

            int contadorLadosAcabamento = 0;
            if (item.Baixo == true)
                contadorLadosAcabamento = contadorLadosAcabamento + 1;
            if (item.Cima == true)
                contadorLadosAcabamento = contadorLadosAcabamento + 1;
            if (item.Direita == true)
                contadorLadosAcabamento = contadorLadosAcabamento + 1;
            if (item.Esquerda == true)
                contadorLadosAcabamento = contadorLadosAcabamento + 1;

            double valorTotalAcabamento = valorAcabamento * contadorLadosAcabamento;

            double metros = Convert.ToDouble(item.Largura) * Convert.ToDouble(item.Comprimento);

            double valorPedra = metros * valorProduto;

            double valorTotal = (valorTotalAcabamento + valorPedra + valorTipoPedra) * item.Quantidade;

            return valorTotal;

        }

        [HttpPost, JsonExceptionFilter]
        public ActionResult GerarValorTotal(string id, string frete)
        {
            Orcamento viewModel = new Orcamento();
            int idOrcamento = 0;

            if ((!string.IsNullOrEmpty(id) && id != "0") || Session["idOrcamento"] != null)
            {
                if (id != "0")
                    idOrcamento = Convert.ToInt32(id);
                else if (Session["idOrcamento"] != null)
                    idOrcamento = (int)Session["idOrcamento"];

                double valorTotalItens = orcamentoBusiness.RecuperarValorTotalItensOrcamento(idOrcamento);

                if (!string.IsNullOrEmpty(frete))
                    viewModel.ValorTotal = valorTotalItens + Convert.ToDouble(frete);
                else
                    viewModel.ValorTotal = valorTotalItens;

                viewModel.Id_Orcamento_Capa = idOrcamento;

                return Json(viewModel);
            }
            else
            {
                throw new Exception("Não é possível calcular o valor total itens no orçamento.");
            }
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            List<ItemOrcamento> listaItemOrcamento = new List<ItemOrcamento>();
            try
            {
                if (Session["idOrcamento"] != null)
                {
                    int idOrcamento = (int)Session["idOrcamento"];
                    if (idOrcamento > 0)
                        listaItemOrcamento = orcamentoBusiness.ObterItemOrcamento(idOrcamento);
                }

                return Json(listaItemOrcamento.ToList().ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(listaItemOrcamento.ToList().ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, ItemOrcamento item)
        {
            int idOrcamento = 0;
            int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
            if (Session["idOrcamento"] != null)
            {
                idOrcamento = (int)Session["idOrcamento"];
            }
            else
            {
                Orcamento orca = new Orcamento();
                orca.Cd_Usua_Rgst = codigoUsuario;
                idOrcamento = orcamentoBusiness.InserirOrcamento(orca);
                Session["idOrcamento"] = idOrcamento;
            }

            if (idOrcamento > 0)
            {
                item.Id_Orcamento = idOrcamento;
                try
                {
                    if (codigoUsuario > 0)
                        item.Cd_Usua_Rgst = codigoUsuario;
                    else
                        item.Cd_Usua_Rgst = null;

                    double valorTotal = RecuperarValorTotal(item);
                    item.ValorTotal = valorTotal;
                    orcamentoBusiness.InserirItemOrcamento(item);

                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "Não foi possível gerar os novos dados. Favor entrar em contato com o administrador.");
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, ItemOrcamento item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                double valorTotal = RecuperarValorTotal(item);
                item.ValorTotal = valorTotal;

                orcamentoBusiness.AtualizaItemOrcamento(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, ItemOrcamento item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                orcamentoBusiness.RemoveItemOrcamento(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public FileResult Exportar([DataSourceRequest] DataSourceRequest request, string arquivo, string colunas)
        {
            var lista = clienteBusiness.ObterCliente();
            return base.Exportar(request, lista != null ? (IEnumerable)lista : null, arquivo, colunas);
        }

        public string RecuperarUsuaLogado()
        {
            var identity = (ClaimsIdentity)User.Identity;
            String usuarioLogado = identity.Name;

            return usuarioLogado;
        }

    }
}