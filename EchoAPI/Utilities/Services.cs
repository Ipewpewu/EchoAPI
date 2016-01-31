using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoAPI.Utilities
{
    public class ServiceSettings : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
        }
        [ConfigurationProperty("port", IsRequired = true)]
        public string Port
        {
            get { return (string)this["port"]; }
        }
    }
    public class ServicesSettings  : ConfigurationSection
    {
        [ConfigurationProperty("services", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ServicesCollection), AddItemName = "service")]
        private ServicesCollection Services
        {
            get
            {
                return (ServicesCollection)base["services"];
            }
        }

        public static ServicesCollection ServicesCollection
        {
            get
            {
                return ((ServicesSettings)ConfigurationManager.GetSection("servicesSection")).Services;
            }
        }

    }
    public class ServicesCollection : ConfigurationElementCollection
    {
        private readonly List<ServiceSettings> elements;

        public ServicesCollection()
        {
            this.elements = new List<ServiceSettings>();
        }
        public ServiceSettings this[int index]
        {
            get { return (ServiceSettings)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        public new ServiceSettings this[string serviceName]
        {
            get
            {
                var element = elements.FirstOrDefault(x => x.Name == serviceName);
                if (element != null)
                    return (ServiceSettings)BaseGet(elements.IndexOf(element));
                return null;
            }
            set
            {
                var element = elements.FirstOrDefault(x => x.Name == serviceName);
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
            var element = new ServiceSettings();
            this.elements.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceSettings)element).Name;
        }

    }
}
