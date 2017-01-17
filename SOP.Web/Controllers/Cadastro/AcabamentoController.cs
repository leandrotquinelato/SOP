using SOP.Web.Controllers.Abstratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using SOP.Domain.Logics;
using SOP.Entidades;
using Kendo.Mvc.UI;
using SOP.DAL.DAO;
using System.Collections;
using System.Security.Claims;

namespace SOP.Web.Controllers.Cadastro
{
    public class AcabamentoController : BaseController
    {
        AcabamentoBusiness acabamentoBusiness = new AcabamentoBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        public ActionResult Consultar()
        {
            return View("~/Views/Cadastro/CadastroAcabamento.cshtml");
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            List<Acabamento> listaAcabamento = new List<Acabamento>();

            try
            {
                listaAcabamento = acabamentoBusiness.ObterAcabamento();

                return Json(listaAcabamento.ToList().ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(listaAcabamento.ToList().ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, Acabamento item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Rgst = codigoUsuario;
                else
                    item.Cd_Usua_Rgst = null;

                acabamentoBusiness.InserirAcabamento(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, Acabamento item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                acabamentoBusiness.AtualizaAcabamento(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, Acabamento item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                acabamentoBusiness.RemoveAcabamento(item);

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
            var lista = acabamentoBusiness.ObterAcabamento();
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