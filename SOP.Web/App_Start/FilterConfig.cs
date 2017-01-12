using CORE.Auth.Attributes;
using System.Web;
using System.Web.Mvc;

namespace SOP.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //coloca como padrão para todos os controllers a autorização.
            filters.Add(new AjaxAuthorizeAttribute());
        }
    }
}
