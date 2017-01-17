using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SOP.Domain.Logics;
using Kendo.Mvc.UI;
using SOP.Entidades;
using SOP.DAL.DAO;
using System.Collections;
using SOP.Web.Controllers.Abstratos;
using System.Security.Claims;

namespace SOP.Web.Controllers.Cadastro
{
    public class ProdutoController : BaseController
    {
        ProdutoBusiness produtoBusiness = new ProdutoBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        public ActionResult Consultar()
        {
            return View("~/Views/Cadastro/CadastroProduto.cshtml");
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            List<Produto> listaProduto = new List<Produto>();

            try
            {
                listaProduto = produtoBusiness.ObterProduto();

                return Json(listaProduto.ToList().ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(listaProduto.ToList().ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, Produto item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Rgst = codigoUsuario;
                else
                    item.Cd_Usua_Rgst = null;

                produtoBusiness.InserirProduto(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, Produto item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                produtoBusiness.AtualizaProduto(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, Produto item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                produtoBusiness.RemoveProduto(item);

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
            var lista = produtoBusiness.ObterProduto();
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