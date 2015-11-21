using BabyDiary.Business;
using BabyDiary.Business.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyDiary.Ninject
{
    public class BusinessBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserProvider>().To<UserProvider>();
        }
    }
}

