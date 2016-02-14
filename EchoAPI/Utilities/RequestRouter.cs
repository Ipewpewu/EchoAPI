using EchoModelsLibrary.Models;
using System.Net.Http;
using EchoModelsLibrary.Models.Enums;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace EchoAPI.Utilities
{
    public class RequestRouter
    {
        public dynamic RouteRequest(EchoRequest echoRequest)
        {
            string url;
            using (var dbo = new EchoEntities())
            {
                var intent = dbo.Intents.Include("Service").First(x => x.ApplicationId == echoRequest.session.application.applicationId && x.Active);
                url = string.Format(intent.Service.BaseUrl, intent.Service.Port, intent.Name);
            }                

            var client = new HttpClient();

            HttpResponseMessage response = new HttpResponseMessage();
            switch (echoRequest.request.type)
            {
                case RequestType.IntentRequest:
                    var jobject = JsonConvert.SerializeObject(((IntentRequest)echoRequest.request).intent.slots);
                    var content = new StringContent(jobject, Encoding.UTF8, "application/json");
                    response = client.PostAsync(url, content).Result;
                    break;
                case RequestType.LaunchRequest:
                    response = client.GetAsync(url).Result;
                    break;
                case RequestType.SessionEndRequest:
                    response = client.GetAsync(string.Format("{0}/{1}", url, ((SessionEndRequest)echoRequest.request).reason)).Result;
                    break;
            }

            //TODO: success/fail handling and create a standard response object to map to EchoResponseObject

            var echoReponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result) as EchoResponse;

            if (echoReponse != null)
                return echoReponse;

            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
