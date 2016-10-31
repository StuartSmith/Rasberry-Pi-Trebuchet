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
using Windows.ApplicationModel;
using System.Reflection;
using Windows.Networking;
using Windows.Networking.Connectivity;
using System.Net;

namespace Rasberry_Pi_Trebuchet.IOT.ViewModels
{
    public class VM_TrafficView : ViewModelBase
    {
        //private IRouteHandler restRouteHandler;
        private HttpServer _httpServer;

        public VM_TrafficView()
        {

            ///Retieve the Machine Name
            TrebuchetVersion = string.Format("{0}.{1}.{2}.{3}",
                                    Package.Current.Id.Version.Major,
                                    Package.Current.Id.Version.Minor,
                                    Package.Current.Id.Version.Build,
                                    Package.Current.Id.Version.Revision);

            ///Retieve the Host Name
            foreach (HostName localHostName in NetworkInformation.GetHostNames())
            {
                if (localHostName.IPInformation != null)
                {
                    if (localHostName.Type == HostNameType.Ipv4)
                    {
                        PiIP = localHostName.ToString();
                        break;
                    }
                }
            }
        }

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


        private string _PiMachineName;
        /// <summary>
        ///  The Name of the Machine
        /// </summary>
        public string PiMachineName
        {
            get
            {
                return _PiMachineName;
            }
            set
            {
                // Set value
                if (_PiMachineName != value)
                {
                    _PiMachineName = value;
                    OnPropertyChanged();
                }
            }
        }

        
        private string _PiIP;
        /// <summary>
        ///  The IP Address for the Raspberry Bi
        /// </summary>
        public string PiIP
        {
            get
            {
                return _PiIP;
            }
            set
            {
                // Set value
                if (_PiIP != value)
                {
                    _PiIP = value;
                    OnPropertyChanged();
                }

            }
        }


        private string _TrebuchetVersion;
        /// <summary>
        ///  The IP Address for the Raspberry Bi
        /// </summary>
        public string TrebuchetVersion
        {
            get
            {
                return _TrebuchetVersion;
            }
            set
            {
                // Set value
                if (_TrebuchetVersion != value)
                {
                    _TrebuchetVersion = value;
                    OnPropertyChanged();
                }

            }
        }




    }
}
