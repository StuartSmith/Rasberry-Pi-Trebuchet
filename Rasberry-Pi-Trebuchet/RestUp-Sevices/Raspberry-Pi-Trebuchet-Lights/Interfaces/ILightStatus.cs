
using Raspberry_Pi_Trebuchet.RestUp.Lights.RestViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.Interfaces
{
    public interface ILightStatus
    {
        Task<List<LightRestViewModel>> RetrieveLightStatuses();

        Task<List<LightRestViewModel>> RetrieveLightStatus(string LightType);

        Task<bool> SetLight(ILightRestViewModel light);
    }


    

}
