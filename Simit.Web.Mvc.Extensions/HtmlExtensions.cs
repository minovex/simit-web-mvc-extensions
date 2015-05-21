namespace System.Web.Mvc
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    #endregion Using Directives

    /// <summary>
    /// 
    /// </summary>
    public static class HtmlExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Includes the style sheet.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static MvcHtmlString IncludeStyleSheet(this HtmlHelper html, string path)
        {
            return new MvcHtmlString(string.Format("<link type=\"text/css\" rel=\"Stylesheet\" href=\"{0}?{1}\" />", path, DateTime.Now.Ticks.ToString()));
        }

        /// <summary>
        /// Includes the javascript.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static MvcHtmlString IncludeJavascript(this HtmlHelper html, string path)
        {
            return new MvcHtmlString(string.Format("<script type=\"text/javascript\" src=\"{0}?{1}\"></script>", path, DateTime.Now.Ticks.ToString()));
        }

        /// <summary>
        /// Displays the date.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static MvcHtmlString DisplayDate(this HtmlHelper html, DateTime date)
        {
            return new MvcHtmlString(date.ToString("dd.MM.yyyy"));
        }

        /// <summary>
        /// Displays the date.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static MvcHtmlString DisplayDate(this HtmlHelper html, DateTime? date)
        {
            return new MvcHtmlString(date.HasValue ? date.Value.ToString("dd.MM.yyyy") : string.Empty);
        }

        /// <summary>
        /// Displays the short date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string DisplayShortDate(DateTime dateTime)
        {
            return String.Format("{0:dd.MM.yyyy}", dateTime);
        }

        /// <summary>
        /// Displays the short date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string DisplayShortDate(DateTime? dateTime)
        {
            return String.Format("{0:dd.MM.yyyy}", dateTime);
        }

        /// <summary>
        /// Displays the date time.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static MvcHtmlString DisplayDateTime(this HtmlHelper html, DateTime date)
        {
            return new MvcHtmlString(date.ToString("dd.MM.yyyy HH:MM"));
        }

        /// <summary>
        /// Displays the date time.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static MvcHtmlString DisplayDateTime(this HtmlHelper html, DateTime? date)
        {
            return new MvcHtmlString(date.HasValue ? date.Value.ToString("dd.MM.yyyy HH:MM") : string.Empty);
        }

        /// <summary>
        /// Displays the boolean.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static MvcHtmlString DisplayBoolean(this HtmlHelper html, bool value)
        {
            return new MvcHtmlString(value ? "True" : "False");
        }

        /// <summary>
        /// Renders the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static MvcHtmlString RenderHTML(this HtmlHelper html, string content)
        {
            if (String.IsNullOrWhiteSpace(content))
                return new MvcHtmlString(string.Empty);

            string output = content.Replace("\r\n", string.Empty);
            output = output.Replace("\n", string.Empty);
            return new MvcHtmlString(output);
        }

        /// <summary>
        /// CheckBoxes the list.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            return CheckBoxList(helper, name, items, null);
        }

        /// <summary>
        /// CheckBoxes the list.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="items">The items.</param>
        /// <param name="checkboxHtmlAttributes">The checkbox HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> checkboxHtmlAttributes)
        {
            StringBuilder output = new StringBuilder();

            foreach (var item in items)
            {
                output.Append("<div class=\"fields\"><label>");
                var checkboxList = new TagBuilder("input");
                checkboxList.MergeAttribute("type", "checkbox");
                checkboxList.MergeAttribute("name", name);
                checkboxList.MergeAttribute("value", item.Value);

                if (item.Selected)
                    checkboxList.MergeAttribute("checked", "checked");

                if (checkboxHtmlAttributes != null)
                    checkboxList.MergeAttributes(checkboxHtmlAttributes);

                checkboxList.SetInnerText(item.Text);
                output.Append(checkboxList.ToString(TagRenderMode.SelfClosing));
                output.Append("&nbsp;" + item.Text + "</label></div>");
            }

            return MvcHtmlString.Create(output.ToString());
        }

        /// <summary>
        /// RadioButtons for select list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="listOfValues">The list of values.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> listOfValues, object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                foreach (SelectListItem item in listOfValues)
                {
                    var id = string.Format(
                       "{0}_{1}",
                        metaData.PropertyName,
                        item.Value
                    );
                    RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
                    if (!routeValues.ContainsKey("id"))
                        routeValues.Add("id", id);
                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, routeValues).ToHtmlString();
                    sb.AppendFormat(
                       "{2}<label for=\"{0}\">{1}</label> <br />",
                        id,
                        HttpUtility.HtmlEncode(item.Text),
                        radio
                    );
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Enums the dropdown list.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="enumerateType">Type of the enumerate.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Invalid Enumerator Type</exception>
        public static MvcHtmlString EnumDropdownList(this HtmlHelper htmlHelper, string name, Type enumerateType, object selectedValue)
        {
            if (!enumerateType.IsEnum)
                throw new InvalidOperationException("Invalid Enumerator Type");

            var enumList = new List<SelectListItem>();

            for (int i = 0; i < enumerateType.GetEnumNames().Count(); i++)
            {
                var option = new SelectListItem
                {
                    Value = ((int)enumerateType.GetEnumValues().GetValue(i)).ToString(),
                    Text = enumerateType.GetEnumNames()[i],
                    Selected = (enumerateType.GetEnumValues().GetValue(i).Equals(selectedValue))
                };

                enumList.Add(option);
            }

            return htmlHelper.DropDownList(name, enumList, selectedValue.ToString());
        }

        /// <summary>
        /// To the select list.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="selectedKeys">The selected keys.</param>
        /// <returns></returns>
        public static SelectList ToSelectList<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, List<TKey> selectedKeys = null)
        {
            return selectedKeys == null ? new SelectList(dictionary, "Key", "Value") : new SelectList(dictionary, "Key", "Value", selectedKeys);
        }

        #endregion Public Static Methods
    }
}