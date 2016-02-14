using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EchoModelsLibrary.Models;
using EchoModelsLibrary.Models.Enums;
using System.Configuration;
using EchoAPI.Utilities;

namespace EchoAPI.Controllers
{
    public class EchoController : ApiController
    {
        
        [HttpPost]
        public EchoResponse Post(EchoRequest echoRequest)
        {
            try
            {               
                var router = new RequestRouter();
                var response = router.RouteRequest(echoRequest);

                if (response is EchoResponse)
                    return response;
                else
                {
                    var defaultResponse = new EchoResponse();
                    defaultResponse.version = "1.0.1";
                    defaultResponse.response = new EchoResponse.Response();
                    defaultResponse.response.shouldEndSession = true;
                    defaultResponse.response.outputSpeech = new EchoResponse.Response.OutputSpeech();
                    defaultResponse.response.outputSpeech.type = OutputSpeechType.PlainText;
                    defaultResponse.response.outputSpeech.text = response;

                    return defaultResponse;
                }

            }
            catch (Exception ex)
            {
                var defaultResponse = new EchoResponse();
                defaultResponse.version = "1.0.1";
                defaultResponse.response = new EchoResponse.Response();
                defaultResponse.response.shouldEndSession = true;
                defaultResponse.response.outputSpeech = new EchoResponse.Response.OutputSpeech();
                defaultResponse.response.outputSpeech.type = OutputSpeechType.PlainText;
                defaultResponse.response.outputSpeech.text = string.Format("ECHO Exception! {0} - {1}", ex.Message, ex.StackTrace);

                return defaultResponse;
            }            
        }
    }
}
