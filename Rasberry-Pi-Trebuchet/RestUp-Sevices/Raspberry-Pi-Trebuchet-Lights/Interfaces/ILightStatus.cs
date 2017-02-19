
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.Interfaces
{
    public interface ILightStatus
    {
        Task<List<ILightRestViewModel>> RetrieveLightStatuses();

        Task<List<ILightRestViewModel>> RetrieveLightStatus(string LightType);

        Task<bool> SetLight(ILightRestViewModel light);
    }


    

}
