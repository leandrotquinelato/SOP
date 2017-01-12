using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(SOP.App_Start.Startup))]
namespace SOP.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //caminho para onde será direcionado caso a aplicação retorne unauthorized (401)
                LoginPath = new PathString("/Autenticacao/logIn"),
                //informar que se o tempo da sessão do usuario passar de mais da metade do Exp
                //a sessão será encerrada se o usuario fizer um novo request
                SlidingExpiration = false,
                //tempo da sessão de 2 horas                
                ExpireTimeSpan = TimeSpan.FromHours(2)
            });


            var config = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            config.Workarounds.ColumnTypeCasingConventionCompatibility = false;
        }
    }
}
