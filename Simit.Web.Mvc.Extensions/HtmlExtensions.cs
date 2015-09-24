namespace System.Web.Mvc
{
    #region Using Directives

    using System.Text;
    using System.Web.Routing;

    #endregion Using Directives

    /// <summary>
    ///
    /// </summary>
    public static class HtmlExtensions
    {
        #region Public Static Properties

        public static string CDNURL
        {
            get
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["simit:cdn:url"];
                return url == null ? string.Empty : url;
            }
        }

        #endregion Public Static Properties

        #region Public Static Methods

        public static MvcHtmlString CSS(this HtmlHelper helper, params string[] cssPath)
        {
            StringBuilder tags = new StringBuilder();

            foreach (string cssPathItem in cssPath)
            {
                TagBuilder tagBuilder = new TagBuilder("link");
                tagBuilder.Attributes["href"] = Normalize(cssPathItem);
                tagBuilder.Attributes["rel"] = "stylesheet";

                tags.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));
            }

            return new MvcHtmlString(tags.ToString());
        }

        public static MvcHtmlString JS(this HtmlHelper helper, params string[] jsPath)
        {
            StringBuilder tags = new StringBuilder();

            foreach (string jsPathItem in jsPath)
            {
                TagBuilder tagBuilder = new TagBuilder("script");
                tagBuilder.Attributes["src"] = Normalize(jsPathItem);

                tags.Append(tagBuilder.ToString());
            }

            return new MvcHtmlString(tags.ToString());
        }

        public static MvcHtmlString Path(this HtmlHelper helper, string path)
        {
            string newPath = Normalize(path);
            return new MvcHtmlString(newPath);
        }

        public static MvcHtmlString IMG(this HtmlHelper helper, string path, string alt = null, object attiributes = null)
        {
            TagBuilder tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes["src"] = Normalize(path);
            if (attiributes != null)
            {
                tagBuilder.MergeAttributes(new RouteValueDictionary(attiributes));
            }

            if (alt != null)
            {
                tagBuilder.Attributes["alt"] = alt;
            }

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        #endregion Public Static Methods

        #region Private Static Methods

        private static string Normalize(string path)
        {
            if (!path.StartsWith("/"))
                return path;
            else
                return CDNURL + path;
        }

        #endregion Private Static Methods
    }
}