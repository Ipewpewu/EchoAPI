using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EchoModelsLibrary.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestType
    {        
        IntentRequest,
        LaunchRequest,
        SessionEndRequest 
    }

}
