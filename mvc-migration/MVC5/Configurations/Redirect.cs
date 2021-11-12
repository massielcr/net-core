using System.Configuration;

namespace MVC5.Configurations
{
    public class Redirect : ConfigurationElement
    {
        [ConfigurationProperty("Title")]
        public string Title { get { return (string)this["Title"]; } set { this["Title"] = value; }}

        [ConfigurationProperty("Old")]
        public string Old { get { return (string)this["Old"]; } set { this["Old"] = value; } }

        [ConfigurationProperty("New")]
        public string New { get { return (string)this["New"]; } set { this["New"] = value; } }
    }
}