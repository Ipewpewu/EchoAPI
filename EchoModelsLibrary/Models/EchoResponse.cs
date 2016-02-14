using EchoModelsLibrary.Models.Enums;

namespace EchoModelsLibrary.Models
{
    public partial class EchoResponse
    {
        public string version { get; set; }
        public dynamic sessionAttributes { get; set; }
        public Response response { get; set; }
        public class Response
        {
            public bool shouldEndSession { get; set; }
            public OutputSpeech outputSpeech { get; set; }
            public Card card { get; set; }
            public Reprompt reprompt { get; set; }

            public class OutputSpeech
            {
                public OutputSpeechType type { get; set; }
                public string text { get; set; }
            }
            public class Card
            {
                public CardType type { get; set; }
                public string title { get; set; }
                public string content { get; set; }
            }
            public class Reprompt
            {
                public OutputSpeech outputSpeech { get; set; }
            }
        }                        
    }

    public partial class EchoResponse
    {
        public EchoResponse()
        {
            response = new Response();
            //response.outputSpeech = new Response.OutputSpeech();
            //response.card = new Response.Card();
            //response.reprompt = new Response.Reprompt();
            //response.reprompt.outputSpeech = new Response.OutputSpeech();
            
        }
    }
   

}
