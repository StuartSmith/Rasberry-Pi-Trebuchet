using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers
{
         public class PiLights
        {
            public PiLights()
            {
                 Lights = new List<Light>();
                 Lights.Add(new Light() { IsLightOn = true,
                                          LightPosition = LightType.LeftLight,
                                          Description =LightType.LeftLight.ToString() });

                 Lights.Add(new Light() { IsLightOn = true,
                                          LightPosition = LightType.RightLight,
                                          Description = LightType.RightLight.ToString() });
        }

             public List<Light> Lights { get; set; }
       }

[RestController(InstanceCreationType.Singleton)]
    class ParameterController
    {
        public class DataReceived
        {
            public int ID { get; set; }
            public string PropName { get; set; }
        }

       

        [UriFormat("/simpleparameter/{id}/property/{propName}")]
        public GetResponse GetWithSimpleParameters(int id, string propName)
        {
            //return new GetResponse(
            //    GetResponse.ResponseStatus.OK,
            //    new DataReceived() { ID = id, PropName = propName });

            return new GetResponse(
               GetResponse.ResponseStatus.OK,
               new PiLights());
        }

    }
}
