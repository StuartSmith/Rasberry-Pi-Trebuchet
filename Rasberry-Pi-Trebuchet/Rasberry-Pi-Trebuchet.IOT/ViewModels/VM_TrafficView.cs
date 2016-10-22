using Devkoes.Restup.WebServer.Http;
using Rasberry_Pi_Trebuchet.Common.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Models.Contracts;
using Devkoes.Restup.WebServer.Rest;
using Rasberry_Pi_Trebuchet.IOT.Controllers;
using Devkoes.Restup.WebServer.File;
using Rasberry_Pi_Trebuchet.IOT.Controllers.api;

namespace Rasberry_Pi_Trebuchet.IOT.ViewModels
{
    public class VM_TrafficView : ViewModelBase
    {
        //private IRouteHandler restRouteHandler;
        private HttpServer _httpServer;

        public  async Task InitializeWebServer()
        {
            // var httpServer = new HttpServer(8800);
            var httpServer = new HttpServer(80);
            _httpServer = httpServer;
            //Register the Server
            var restRouteHandler = new RestRouteHandler();

            //Create the Routes           
            restRouteHandler.RegisterController<LightsController>();
            restRouteHandler.RegisterController<ServoController>();
            restRouteHandler.RegisterController<UltraSonicController>();
            restRouteHandler.RegisterController<TrebuchetController>();

            //Register the Route Controller
            httpServer.RegisterRoute("api", restRouteHandler);            

            httpServer.RegisterRoute(new StaticFileRouteHandler(@"Rasberry-Pi-Trebuchet.StaticFiles\Web"));
            
            await httpServer.StartServerAsync();
            
        }
    }
}
