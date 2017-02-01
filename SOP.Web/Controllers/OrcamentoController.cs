using Kendo.Mvc.UI;
using SOP.Web.Controllers.Abstratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SOP.Entidades;
using SOP.Domain.Logics;
using SOP.DAL.DAO;
using System.Collections;
using System.Security.Claims;

namespace SOP.Web.Controllers.Cadastro
{
    public class OrcamentoController : BaseController
    {
        ClienteBusiness clienteBusiness = new ClienteBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        public ActionResult Consultar()
        {
            return View("~/Views/Orcamento/Index.cshtml");
        }

        public ActionResult ObterCliente([DataSourceRequest] DataSourceRequest request)
        {
            var listaClientes = clienteBusiness.ObterCliente().Select(i => new { Id = i.Id_Cliente, Descricao = i.Nm_Cliente });

            return Json(listaClientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            return null;
            //List<Cliente> listaCliente = new List<Cliente>();

            //try
            //{
            //    listaCliente = clienteBusiness.ObterCliente();

            //    return Json(listaCliente.ToList().ToDataSourceResult(request, ModelState));
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message);

            //    return Json(listaCliente.ToList().ToDataSourceResult(request, ModelState));
            //}
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, Cliente item)
        {
            return null;
            //try
            //{
            //    int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
            //    if (codigoUsuario > 0)
            //        item.Cd_Usua_Rgst = codigoUsuario;
            //    else
            //        item.Cd_Usua_Rgst = null;

            //    clienteBusiness.InserirCliente(item);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, Cliente item)
        {
            return null;
            //try
            //{
            //    int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
            //    if (codigoUsuario > 0)
            //        item.Cd_Usua_Altr = codigoUsuario;
            //    else
            //        item.Cd_Usua_Altr = null;

            //    clienteBusiness.AtualizaCliente(item);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, Cliente item)
        {
            return null;
            //try
            //{
            //    int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
            //    if (codigoUsuario > 0)
            //        item.Cd_Usua_Altr = codigoUsuario;
            //    else
            //        item.Cd_Usua_Altr = null;

            //    clienteBusiness.RemoveCliente(item);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message);

            //    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            //}
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