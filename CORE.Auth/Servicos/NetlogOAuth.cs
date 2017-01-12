using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using CORE.Auth.Mapeamentos;

namespace CORE.Auth.Servicos
{
    public static class NetlogOAuth
    {
        ////
        //// Função que realiza o Login de um determinado usuário no sistema.
        ////
        ////public static void Login(LogInModel loginModel, HttpRequestBase request, List<string> sistemas)
        ////{
        ////    try
        ////    {
        ////        var searchResult = AutenticateActiveDirectory(loginModel);

        ////        List<string> listaGrupos = GetGruposActiveDirectory(searchResult);

        ////        if (listaGrupos == null || listaGrupos.Count <= 0)
        ////        {
        ////            throw new Exception("Usuario não possui acesso ao sistema");
        ////        }

        ////        //List<string> listaCodigoISA = new List<string>();

        ////        //Grupos que deverão vir do AD
        ////        //var gruposAd = new List<String>();
        ////        //gruposAd.Add("GESTR");
        ////        //gruposAd.Add("GFITA");
        ////        //listaCodigoISA = GetRolesIsa(gruposAd, sistemas);
        ////        //var listaCodigoISA = GetRolesIsa(listaGrupos, sistemas);

        ////        ////var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
        ////        var identity = new ClaimsIdentity("ApplicationCookie");                
        ////        identity.AddClaim(new Claim(ClaimTypes.Name, GetNomeCompletoUsuario(searchResult)));

        ////        //listaCodigoISA.ForEach(i => identity.AddClaim(new Claim(ClaimTypes.Role, i)));

        ////        var ctx = request.GetOwinContext();
        ////        var authManager = ctx.Authentication;
                
        ////        //indica que o o cookie expira junto com a sessão
        ////        authManager.SignIn(new AuthenticationProperties() { IsPersistent = loginModel.RememberMe }, identity);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Não foi possível realizar o login. " + ex.Message, ex);
        ////    }
        ////}       
        public static void Login(LogInModel loginModel, HttpRequestBase request, List<string> sistemas)
        {
            try
            {
                var searchResult = AutenticateActiveDirectory(loginModel);

                List<string> listaGrupos = GetGruposActiveDirectory(searchResult);

                if (listaGrupos == null || listaGrupos.Count <= 0)
                {
                    throw new Exception("Usuario não possui acesso ao sistema");
                }

                //List<string> listaCodigoISA = new List<string>();

                //Grupos que deverão vir do AD
                //var gruposAd = new List<String>();
                //gruposAd.Add("GESTR");
                //gruposAd.Add("GFITA");
                //listaCodigoISA = GetRolesIsa(gruposAd, sistemas);
                //var listaCodigoISA = GetRolesIsa(listaGrupos, sistemas);

                //var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaim(new Claim(ClaimTypes.Name, GetNomeCompletoUsuario(searchResult)));
                identity.AddClaim(new Claim(ClaimTypes.Sid, loginModel.UserName));
                foreach (string grupo in listaGrupos)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, grupo));
                }                

                //listaCodigoISA.ForEach(i => identity.AddClaim(new Claim(ClaimTypes.Role, i)));

                var ctx = request.GetOwinContext();
                var authManager = ctx.Authentication;

                //indica que o o cookie expira junto com a sessão
                authManager.SignIn(new AuthenticationProperties() { IsPersistent = loginModel.RememberMe }, identity);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar o login. " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Método responsável por realizar o logout no sistema
        /// </summary>
        /// <param name="request">Request Base</param>
        public static void LogOut(HttpRequestBase request)
        {
            var ctx = request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
        }

        /// <summary>
        /// Método responsável por realizar a autenticação no Active Directory
        /// </summary>
        /// <param name="model">Informações sobre o login</param>
        /// <returns></returns>
        private static SearchResult AutenticateActiveDirectory(LogInModel model)
        {
            try
            {
                String conString = ConfigurationManager.AppSettings["LDAPConnectionString"];
                using (var directoryEntry = new DirectoryEntry(conString, model.UserName, model.Password, AuthenticationTypes.Secure | AuthenticationTypes.ReadonlyServer))
                {
                    var directorySearch = new DirectorySearcher(directoryEntry, "cn=" + model.UserName, new string[] { "memberOf", "displayName" });
                    directorySearch.Asynchronous = true;
                    return directorySearch.FindOne();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Método responsável por capturar o nome completo do usuário logado no sistema
        /// </summary>
        /// <param name="searchResult">Resultado da pesquisa no AD</param>
        /// <returns>Nome completo do usuário</returns>
        private static string GetNomeCompletoUsuario(SearchResult searchResult)
        {
            string nomeCompleto = "";
            nomeCompleto = searchResult.Properties["displayName"][0].ToString();
            return nomeCompleto;
        }

        /// <summary>
        /// Metodo que extrai os Grupos do do Active Directory
        /// </summary>
        /// <param name="searchResult">Resultado da pesquisa no AD</param>
        /// <returns>Lista dos grupos</returns>
        private static List<String> GetGruposActiveDirectory(SearchResult searchResult)
        {
            List<string> gruposActiveDirectory = new List<string>();

            foreach (var prop in searchResult.Properties["memberOf"])
            {
                gruposActiveDirectory.Add(prop.ToString().Split('=', ',')[1]);
            }

            return gruposActiveDirectory;
        }

        public static string GetIdentificacaoCompleta(System.Security.Principal.IPrincipal user)
        {
            var _user = (ClaimsPrincipal)user;

            //var identificacaoCompleta = _user.Claims.Where(i => i.Type.Contains("claims/sid")).FirstOrDefault().Value + " - " + _user.Identity.Name;
            var identificacaoCompleta = _user.Identity.Name;

            return identificacaoCompleta;
        }

        public static string GetIdentificacaoUsuario(System.Security.Principal.IPrincipal user)
        {
            var identity = (ClaimsIdentity)user.Identity;
            string matricula = identity.FindFirst(ClaimTypes.Sid).Value;

            return matricula.ToUpper();
        }
    }
}
