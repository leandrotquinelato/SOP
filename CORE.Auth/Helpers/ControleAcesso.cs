using MRS.Automacao.Uteis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.WebPages;

namespace CORE.Auth.Helpers
{   
    public class ControleAcesso
    {
        public enum PerfilAcesso { ADMINISTRADOR, INSPETOR_HORTO, INSPETOR_LAFAIETE, ASSISTENTE_HORTO, ASSISTENTE_LAFAIETE, INDICADORES, VISITANTE }

        //public static PerfilAcesso PerfilUsuario(IPrincipal user)
        //{
        //    try
        //    {
        //        if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAdministradores"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.ADMINISTRADOR.ToString());
        //            return PerfilAcesso.ADMINISTRADOR;
        //        }
        //        else if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoInspetorHorto"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.INSPETOR_HORTO.ToString());
        //            return PerfilAcesso.INSPETOR_HORTO;
        //        }
        //        else if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoInspetorConsLafaiete"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.INSPETOR_LAFAIETE.ToString());
        //            return PerfilAcesso.INSPETOR_LAFAIETE;
        //        }
        //        else if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAssistenteHorto"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.ASSISTENTE_HORTO.ToString());
        //            return PerfilAcesso.ASSISTENTE_HORTO;
        //        }
        //        else if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAssistenteConsLafaiete"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.ASSISTENTE_LAFAIETE.ToString());
        //            return PerfilAcesso.ASSISTENTE_LAFAIETE;
        //        }
        //        else if ((user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoIndicadores"])))
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.INDICADORES.ToString());
        //            return PerfilAcesso.INDICADORES;
        //        }
        //        else
        //        {
        //            TratamentoLog.GravarLog(PerfilAcesso.VISITANTE.ToString());
        //            return PerfilAcesso.VISITANTE;
        //        }
        //        //return PerfilAcesso.ADMINISTRADOR;
        //    }
        //    catch (Exception ex)
        //    {
        //        TratamentoLog.GravarLog(ex.Message);
        //        return PerfilAcesso.VISITANTE;
        //    }
        //}

        public static bool UsuarioNoGrupo(IPrincipal user,PerfilAcesso perfil)
        {
            try
            {

                if (PerfilAcesso.ADMINISTRADOR == perfil)
                {
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAdministradores"]));
                }
                else if (PerfilAcesso.INSPETOR_HORTO == perfil)
                {                    
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoInspetorHorto"]));
                }
                else if (PerfilAcesso.INSPETOR_LAFAIETE == perfil)
                {                    
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoInspetorConsLafaiete"]));
                }
                else if (PerfilAcesso.ASSISTENTE_HORTO == perfil)
                {                    
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAssistenteHorto"]));
                }
                else if (PerfilAcesso.ASSISTENTE_LAFAIETE == perfil)
                {                    
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoAssistenteConsLafaiete"]));
                }
                else if (PerfilAcesso.INDICADORES == perfil)
                {                    
                    return (user.IsInRole(ConfigurationManager.AppSettings["GrupoAcessoIndicadores"]));
                }
                else
                {
                    TratamentoLog.GravarLog(PerfilAcesso.VISITANTE.ToString());
                    return false;
                }
                //return PerfilAcesso.ADMINISTRADOR;
            }
            catch (Exception ex)
            {
                TratamentoLog.GravarLog(ex.Message);
                return false;
            }
        }
    }
}
