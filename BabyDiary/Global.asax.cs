using System.Data.Entity.Infrastructure.Interception;
using System.Web.Mvc;
using System.Web.Routing;
using BabyDiary.DAL;
using BabyDiary.Models;
using Resources;

namespace BabyDiary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DbInterception.Add(new BabyDiaryLoggingInterceptor());

            ModelMetadataProviders.Current = new DataAnnotationsResourcesModelMetadataProvider(typeof(ValidationMessages));
        }
    }
}

//using Ninject;
//using Ninject.Web.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace BabyDiary
//{
//    public class MvcApplication : NinjectHttpApplication
//    {
//        protected override void OnApplicationStarted()
//        {
//            base.OnApplicationStarted();
//            AreaRegistration.RegisterAllAreas();
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//        }

//        protected override IKernel CreateKernel()
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
//            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
//            var kernel = new StandardKernel();
//            kernel.Load(Assembly.GetExecutingAssembly());
//            return kernel;
//        }
//    }
//}
