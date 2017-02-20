using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Interfaces;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.JsonParams
{
    public class SonicJsonConverter<T> : JsonConverter
    {      
            public override bool CanConvert(Type objectType)
            {
                //assume we can convert to anything for now
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                 var IUltraSonicSensorRunMeasurements  = new List<IUltraSonicSensorRunMeasurement>();
                 var ValuesToDesialize = serializer.Deserialize<List<ViewModelUltraSonicSensorRunMeasurement>>(reader);
                 ValuesToDesialize.ForEach(x => IUltraSonicSensorRunMeasurements.Add(x));

                return IUltraSonicSensorRunMeasurements;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                //use the default serialization - it works fine
                serializer.Serialize(writer, value);
            }
        
    }

 
}
