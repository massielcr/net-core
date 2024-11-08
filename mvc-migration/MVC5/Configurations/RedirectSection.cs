using System.Configuration;

namespace MVC5.Configurations
{
    public class RedirectSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(RedirectCollection))]
        public RedirectCollection Redirects { get { return (RedirectCollection)base[""]; } }
    }
}