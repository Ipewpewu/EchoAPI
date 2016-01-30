﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EchoAPI.Models;
using EchoAPI.Models.Enums;

namespace EchoAPI.Controllers
{
    public class EchoController : ApiController
    {
        
        [HttpPost]
        public dynamic Post(EchoRequest echoRequest)
        {
            

            //test data for round trip
            var response = new EchoResponse();
            response.version = "1.0.1";
            response.response = new EchoResponse.Response();
            response.response.shouldEndSession = true;
            response.response.outputSpeech = new EchoResponse.Response.OutputSpeech();
            response.response.outputSpeech.type = OutputSpeechType.PlainText;
            response.response.outputSpeech.text = "Hey! What's Going On.";
            response.sessionAttributes = echoRequest;

            response.response.card = new EchoResponse.Response.Card();
            response.response.card.type = CardType.Simple;
            response.response.card.title = "TestCard";
            response.response.card.content = "Testing 1 2 3";
            //response.response.reprompt = new EchoResponse.Response.Reprompt();
            //response.response.reprompt.outputSpeech = new EchoResponse.Response.OutputSpeech();
            //response.response.reprompt.outputSpeech.type = "PlainText";
            //response.response.reprompt.outputSpeech.text = "Say what!";

            return JObject.FromObject(response);
        }
    }
}