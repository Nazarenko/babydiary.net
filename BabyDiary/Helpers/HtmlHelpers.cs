using System;
using System.Web.Mvc;
using System.Web.WebPages;

namespace BabyDiary.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsActive(this HtmlHelper html,
                                 string controller,
                                 string activeClass)
        {
            var routeData = html.ViewContext.RouteData;

            var routeController = (string)routeData.Values["controller"];

            // both must match
            var returnActive = (controller == routeController);

            return returnActive ? activeClass : "";
        }

        public static HelperResult RenderSection(this WebPageBase webPage,
                                string name, Func<dynamic, HelperResult> defaultContents)
        {
            return webPage.IsSectionDefined(name) ? webPage.RenderSection(name) : defaultContents(null);
        }
    }
}