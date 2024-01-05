using System.Web.Mvc;
using System.Text;
using System;

namespace YTP.Main.Areas.SportsStore.Models.HtmlHelpers {
    public static class PagingHelpers {

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl) {

            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++) {
                TagBuilder tag = new TagBuilder("a");

                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage) {
                    tag.AddCssClass("selected");

                    tag.AddCssClass("btn-light");
                } else
                    tag.AddCssClass("btn-light");
                

                tag.AddCssClass("btn btn-lg");

                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}