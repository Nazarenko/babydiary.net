using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
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

        public static MvcHtmlString KoTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null)
        {
            return htmlHelper.TextBoxFor(expression, AttributesWithBind(htmlAttributes, "textInput", expression));
        }

        public static MvcHtmlString KoRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object value,
            object htmlAttributes)
        {
            return htmlHelper.RadioButtonFor(expression, value,
                AttributesWithBind(htmlAttributes, "checked", expression));
        }

        public static HelperResult RenderSection(this WebPageBase webPage,
                                string name, Func<dynamic, HelperResult> defaultContents)
        {
            return webPage.IsSectionDefined(name) ? webPage.RenderSection(name) : defaultContents(null);
        }

        private static RouteValueDictionary AttributesWithBind<TModel, TProperty>(object htmlAttributes, string bindOption, Expression<Func<TModel, TProperty>> expression)
        {
            RouteValueDictionary attributes = null;
            if (htmlAttributes != null)
            {
                attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            }
            else
            {
                attributes = new RouteValueDictionary();
            }
            if (!attributes.ContainsKey("data-bind"))
            {
                attributes.Add("data-bind", bindOption + ": " + ExpressionHelper.GetExpressionText(expression));
            }
            return attributes;

        }
    }
}