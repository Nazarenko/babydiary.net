using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyDiary.Business.Interfaces;
using Ninject;

namespace BabyDiary.Helpers
{
    public abstract class InjectedWebViewPage : WebViewPage
    {
        [Inject]
        public ICurrentUser CurrentUser { get; set; }
    }

    public abstract class InjectedWebViewPage<TModel> : WebViewPage<TModel>
    {
        [Inject]
        public ICurrentUser CurrentUser { get; set; }
    }
}