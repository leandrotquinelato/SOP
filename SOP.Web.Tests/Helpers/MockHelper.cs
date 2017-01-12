using Moq;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAcessoDados.Tests.Helpers
{
    /// <summary>
    /// Helper de "mock" para realização dos testes
    /// </summary>
    public static class MockHelper
    {
        /// <summary>
        /// Método responsável por realizar o mock de um controller
        /// </summary>
        /// <typeparam name="T">Controller a ser mockado</typeparam>
        /// <param name="requestParameters">Parâmetros enviados no request</param>
        /// <param name="constructorParameters">Objetos necessários para inicialização do controller</param>
        /// <returns>Instância do controller</returns>
        public static Controller MockController<T>(NameValueCollection requestParameters,
                                                     params object[] constructorParameters) where T : Controller
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();                      
                          
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection 
                                                        {
                                                            {"X-Requested-With", "XMLHttpRequest"}
                                                        });
            if (requestParameters.Count > 0)
                request.SetupGet(x => x.Params).Returns(requestParameters);
                       
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            Controller controllerInstance = (Controller)Activator.CreateInstance(typeof(T), constructorParameters);

            controllerInstance.ControllerContext = new ControllerContext(context.Object, new RouteData(), controllerInstance);

            return controllerInstance;                
        }
    }
}
