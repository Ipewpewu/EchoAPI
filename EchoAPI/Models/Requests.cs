using EchoAPI.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchoAPI.Utilities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace EchoAPI.Models
{
    [JsonConverter(typeof(RequestConverter))]
    public abstract class Request
    {        
        public RequestType type { get; set; }
        public string requestId { get; set; }
        public string timestamp { get; set; }

        private class RequestConverter : JsonCreationConverter<Request>
        {
            protected override Request Create(Type objectType, JObject jObject)
            {                
                switch((RequestType)Enum.Parse(typeof(RequestType), jObject.Value<string>("type"), true))
                {
                    case RequestType.IntentRequest: return new IntentRequest();
                    case RequestType.LaunchRequest: return new LaunchRequest();
                    case RequestType.SessionEndRequest: return new SessionEndRequest();
                    default:
                        throw new NotSupportedException(string.Format("Type not implemented: '{0}'", jObject.Value<string>("type")));
                }
            }
        }

    }

    public class IntentRequest : Request
    {
        public Intent intent { get; set; }

        public class Intent
        {
            public string name { get; set; }
            public dynamic slots { get; set; }
        }
    }
    public class LaunchRequest : Request { }
    public class SessionEndRequest : Request
    {
        public string reason { get; set; }
    }
}
