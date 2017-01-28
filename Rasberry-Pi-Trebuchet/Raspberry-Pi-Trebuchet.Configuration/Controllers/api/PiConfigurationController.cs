﻿
using Raspberry_Pi_Trebuchet.Common.Models;
using Raspberry_Pi_Trebuchet.Configuration.Interfaces;
using Raspberry_Pi_Trebuchet.Configuration.RestViewModels;
using Raspberry_Pi_Trebuchet.Configuration.Services;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class PiConfigurationController
    {
        [UriFormat("/piconfiguration?={time}")]
        public GetResponse GetAllConfigurationValuePairs(string time)
        {
            var Results = (from nameValuePair in new AzurePiConfiguraton().GetAllValues()
                           select new ViewModelRestNameValuePair() { name = nameValuePair.name, value = nameValuePair.value }
                        ).ToList<ViewModelRestNameValuePair>();

            return new GetResponse(GetResponse.ResponseStatus.OK,
                                Results);
        }


        [UriFormat("/piconfiguration/{name}?={time}")]
        public GetResponse GetSingleConfigurationValuePairs(string name, string time)
        {
            var piNameValuePairDBSettings = new PiNameValuePairDBSettings();
            var PiNameValueFromDb = new PiNameValuePairDBSettings().GetPiNameValuePair(name);
            if (PiNameValueFromDb == null)
                return new GetResponse(GetResponse.ResponseStatus.NotFound,
                                null);

            return new GetResponse(GetResponse.ResponseStatus.OK,
                              PiNameValueFromDb.value);
        }


        [UriFormat("/piconfiguration")]
        public IPostResponse UpdateMultipleConfigurationValuePairs([FromContent] List<ViewModelRestNameValuePair> values)
        {
            new AzurePiConfiguraton().UpdateValues(values.ToList<IPiNameValuePair>());
            return new PostResponse(PostResponse.ResponseStatus.Created, $"/api/piconfiguration", values);
        }


        // PUT api/values/5
        [UriFormat("/piconfiguration/{name}")]
        public IPutResponse UpdateSingleConfigurationValuePair(string name, [FromContent]string value)
        {
            new PiNameValuePairDBSettings().SetNameValuePair(name, value);
            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

    }

}