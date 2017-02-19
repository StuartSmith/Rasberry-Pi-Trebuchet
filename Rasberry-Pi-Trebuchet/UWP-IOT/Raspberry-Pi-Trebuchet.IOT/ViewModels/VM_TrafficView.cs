using Raspberry_Pi_Trebuchet.Common.ViewModels.BaseViewModel;
using Raspberry_Pi_Trebuchet.IOT.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Lights.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.Controllers.api;
using Restup.Webserver.File;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace Raspberry_Pi_Trebuchet.IOT.ViewModels
{
    public class VM_TrafficView : ViewModelBase
    {
        //private IRouteHandler restRouteHandler;
        private HttpServer _httpServer;

        public VM_TrafficView()
        {  
            TrebuchetVersion = string.Format("{0}.{1}.{2}.{3}",
                                    Package.Current.Id.Version.Major,
                                    Package.Current.Id.Version.Minor,
                                    Package.Current.Id.Version.Build,
                                    Package.Current.Id.Version.Revision);
            
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
            var restRouteHandler = new RestRouteHandler();

            //Create the Routes           
            restRouteHandler.RegisterController<LightsController>();
            restRouteHandler.RegisterController<PiConfigurationController>();
            restRouteHandler.RegisterController<ServoController>();
            restRouteHandler.RegisterController<TrebuchetController>();
            restRouteHandler.RegisterController<UltraSonicController>();

            var configuration = new HttpServerConfiguration()
                .ListenOnPort(80)
                .RegisterRoute("api", restRouteHandler)
                .RegisterRoute(new StaticFileRouteHandler(@"rasberry-pi-trebuchet.staticfiles\web"))
                .EnableCors();

            var httpServer = new HttpServer(configuration);
            _httpServer = httpServer;
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
