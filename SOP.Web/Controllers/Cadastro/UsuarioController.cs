using Kendo.Mvc.UI;
using SOP.Domain.Logics;
using SOP.Entidades;
using SOP.Web.Controllers.Abstratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.Collections;
using System.Security.Claims;
using SOP.DAL.DAO;

namespace SOP.Web.Controllers.Cadastro
{
    public class UsuarioController : BaseController
    {
        UsuarioBusiness usuarioBusiness = new UsuarioBusiness();
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        public ActionResult Consultar()
        {
            return View("~/Views/Cadastro/CadastroUsuario.cshtml");
        }

        public ActionResult Ler([DataSourceRequest] DataSourceRequest request)
        {
            List<Usuario> listaUsuario = new List<Usuario>();

            try
            {                
                listaUsuario = usuarioBusiness.ObterUsuarios();

                return Json(listaUsuario.ToList().ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(listaUsuario.ToList().ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Inserir([DataSourceRequest] DataSourceRequest request, Usuario item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Rgst = codigoUsuario;
                else
                    item.Cd_Usua_Rgst = null;

                usuarioBusiness.InserirUsuario(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Atualizar([DataSourceRequest] DataSourceRequest request, Usuario item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;             

                usuarioBusiness.AtualizaUsuario(item);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Apagar([DataSourceRequest] DataSourceRequest request, Usuario item)
        {
            try
            {
                int codigoUsuario = autenticacaoBusiness.RecuperarCodigoUsuarioLogado(RecuperarUsuaLogado());
                if (codigoUsuario > 0)
                    item.Cd_Usua_Altr = codigoUsuario;
                else
                    item.Cd_Usua_Altr = null;

                usuarioBusiness.RemoveUsuario(item);

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
            var lista = usuarioBusiness.ObterUsuarios();            
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