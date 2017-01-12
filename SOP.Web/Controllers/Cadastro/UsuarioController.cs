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

namespace SOP.Web.Controllers.Cadastro
{
    public class UsuarioController : BaseController
    {
        UsuarioBusiness usuarioBusiness = new UsuarioBusiness();

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
	}
}