using MVC5.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MVC5.Extensions
{
    public class RedirectModule : IHttpModule
    {
        private HttpApplication application;
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            application = context;
            application.MapRequestHandler += RedirectUrls;
        }

        private void RedirectUrls(object source, EventArgs args)
        {
            RedirectSection section = (RedirectSection)WebConfigurationManager.GetWebApplicationSection("redirects");
            foreach(Redirect redirect in section.Redirects)
            {
                if(redirect.Old == application.Request.RequestContext.HttpContext.Request.RawUrl)
                {
                    application.Response.Redirect(redirect.New);
                    break;
                }
            }
        }
    }
}