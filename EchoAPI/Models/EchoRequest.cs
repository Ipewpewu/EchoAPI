using EchoAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoAPI.Models
{
    public class EchoRequest
    {
        public Session session { get; set; }
        public Request request { get; set; }

        public class Session
        {
            public string sessionId { get; set; }
            public Application application { get; set; }
            public User user { get; set; }
            public bool New { get; set; }

            public class Application
            {
                public string applicationId { get; set; }
            }
            public class User
            {
                public string userId { get; set; }
            }
        }
        
    }
}
