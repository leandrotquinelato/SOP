using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using CORE.Auth.Servicos;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using SOP.DAL.DAO;
using SOP.Domain.Logics;

namespace SOP.Web.Controllers
{
    /// <summary>
    /// Controller responsável pelas actions de autenticação
    /// </summary>
    [AllowAnonymous]
    public class AutenticacaoController : Controller
    {
        AutenticacaoBusiness autenticacaoBusiness = new AutenticacaoBusiness();

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return RedirectToAction("UnAuthorize", "Autenticacao");
            }
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string usuario = autenticacaoBusiness.ValidaUsuarioLogin(model.UserName.ToUpper(), model.Password);

                    if (usuario == null)
                    {
                        ModelState.AddModelError("", "Não foi possível realizar o login. Nome de usuário ou senha incorretos. ");
                    }
                    else
                    {
                        Login(model, Request, usuario);

                        return Redirect(GetRedirectUrl(model.ReturnUrl));
                    }                   
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);   
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        /// <summary>
        /// Método responsável por realizar o logout no sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            NetlogOAuth.LogOut(Request);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Método responsável por exibir a mensagem de acesso não autorizado no sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult UnAuthorize()
        {
            return View();
        }       

        /// <summary>
        /// Método responsável por fazer o tratamento de redirecionamentos da página,
        /// esse método tem como objetivo validar a URL de redirecionamento
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        protected string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                //return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }


        public static void Login(LogInModel loginModel, HttpRequestBase request, string usuarioRetorno)
        {
            try
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuarioRetorno.ToUpper()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuarioRetorno.ToUpper()));


                var ctx = request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(new AuthenticationProperties() { IsPersistent = loginModel.RememberMe }, identity);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar o login. " + ex.Message, ex);
            }
        }
    }
}