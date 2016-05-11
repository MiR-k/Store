using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TAiMStore.Configs;
using TAiMStore.Model.ViewModels;
using TAiMStore.WebUI.Infrastructure.Binders;

namespace TAiMStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            DependencyResolver.SetResolver(AutofacConfiguration.GetAutofacDependencyResolver());
        }
    }
}
