using EchoAPI.Models;
using System.Net.Http;
using EchoAPI.Models.Enums;

namespace EchoAPI.Utilities
{
    public class RequestRouter
    {
        public dynamic RouteRequest(EchoRequest echoRequest)
        {
            var intent = IntentsSettings.IntentsCollection[echoRequest.session.application.applicationId];
            var service = ServicesSettings.ServicesCollection[intent.Service];

            var url = string.Format(service.Url, service.Port, intent.Name);

            var client = new HttpClient();

            HttpResponseMessage response = new HttpResponseMessage();
            switch (echoRequest.request.type)
            {
                case RequestType.IntentRequest:
                    response = client.PostAsync(url, ((IntentRequest)echoRequest.request).intent.slots).Result;
                    break;
                case RequestType.LaunchRequest:
                    response = client.GetAsync(url).Result;
                    break;
                case RequestType.SessionEndRequest:
                    response = client.GetAsync(string.Format("{0}/{1}", url, ((SessionEndRequest)echoRequest.request).reason)).Result;
                    break;
            }

            //TODO: success/fail handling and create a standard response object to map to EchoResponseObject
            return response.Content;
        }

    }
}
