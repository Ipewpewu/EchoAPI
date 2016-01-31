using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EchoAPI.Utilities
{
    public class IntentSettings : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name { get { return (string)this["name"]; } }
        [ConfigurationProperty("service", IsRequired = true)]
        public string Service { get { return (string)this["service"]; } }
        [ConfigurationProperty("applicationId", IsRequired = true)]
        public string ApplicationId { get { return (string)this["applicationId"]; } }
    }
    public class IntentCollection : ConfigurationElementCollection
    {
        private readonly List<IntentSettings> elements;

        public IntentCollection()
        {
            this.elements = new List<IntentSettings>();
        }
        public IntentSettings this[int index]
        {
            get { return (IntentSettings)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        public new IntentSettings this[string applicationId]
        {
            get
            {
                var element = elements.FirstOrDefault(x => x.ApplicationId == applicationId);
                if (element != null)
                    return (IntentSettings)BaseGet(elements.IndexOf(element));
                return null;
            }
            set
            {
                var element = elements.FirstOrDefault(x => x.ApplicationId == applicationId);
                if (element != null)
                {
                    var index = elements.IndexOf(element);
                    BaseRemoveAt(index);
                    BaseAdd(index, value);
                }
                else
                    BaseAdd(value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            var element = new IntentSettings();
            this.elements.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IntentSettings)element).Name;
        }

    }
    public class IntentsSettings : ConfigurationSection
    {
        [ConfigurationProperty("intents", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(IntentCollection), AddItemName = "intent")]
        private IntentCollection Intents
        {
            get
            {
                return (IntentCollection)base["intents"];
            }
        }

        public static IntentCollection IntentsCollection
        {
            get
            {
                return ((IntentsSettings)ConfigurationManager.GetSection("intentsSection")).Intents;
            }
        }

    }
}
