using BabyDiary.Business;
using BabyDiary.Business.Interfaces;
using BabyDiary.DAL;
using BabyDiary.DAL.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace BabyDiary.Ninject
{
    public class ProjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<BabyDiaryContext>().ToSelf().InRequestScope();

            Bind<IUserProvider>().To<UserProvider>().InRequestScope();

            Bind<IUserRepository>().To<UserRepository>().InRequestScope();
        }
    }
}

