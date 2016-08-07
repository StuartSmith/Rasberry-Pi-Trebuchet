using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.ServiceContracts
{
    public interface ILightStatus
    {
        Task<List<Light>> RetrieveLightStatuses();

        Task<List<Light>> RetrieveLightStatus(string LightType);

        Task<bool> SetLight(Light light);
    }
}
