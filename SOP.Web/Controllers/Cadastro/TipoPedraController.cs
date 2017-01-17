using Kendo.Mvc.UI;
using SOP.Domain.Logics;
using SOP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SOP.DAL.DAO;
using System.Collections;
using SOP.Web.Controllers.Abstratos;
using System.Security.Claims;

namespace SOP.Web.Controllers.Cadastro
{
    public class TipoPedraController : BaseController
    {
        TipoPedraBusiness tipoPedraBusiness = new TipoPedraBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        public ActionResult Consultar()
        {
            return View("~/Views/Cadastro/CadastroTipoPedra.cshtml");
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            List<TipoPedra> listaTipoPedra = new List<TipoPedra>();

            try
            {
                listaTipoPedra = tipoPedraBusiness.ObterTipoPedra();

                return Json(listaTipoPedra.ToList().ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(listaTipoPedra.ToList().ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, TipoPedra item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Rgst = codigoUsuario;
                else
                    item.Cd_Usua_Rgst = null;

                tipoPedraBusiness.InserirTipoPedra(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, TipoPedra item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                tipoPedraBusiness.AtualizaTipoPedra(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, TipoPedra item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                tipoPedraBusiness.RemoveTipoPedra(item);

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
            var lista = tipoPedraBusiness.ObterTipoPedra();
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