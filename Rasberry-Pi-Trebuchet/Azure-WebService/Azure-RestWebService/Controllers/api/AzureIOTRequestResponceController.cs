using Microsoft.AspNetCore.Mvc;
using Raspberry_Pi_Trebuchet.RestUp.LoggingService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Raspberry_Pi_Trebuchet.Azure_WebService.Controllers.api
{
    [Route("api/[controller]")]
    public class AzureIOTRequestResponceController : Controller
    {
        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            Guid msGuid = new Guid(guid);

            var azureIOTRequestResponce = AzureIOTRequestResponceService.Instance;
            var requestResponce = azureIOTRequestResponce.Get(msGuid);

            if (requestResponce == null)
                return NotFound();


            return Ok(requestResponce);
        }
    }
}
