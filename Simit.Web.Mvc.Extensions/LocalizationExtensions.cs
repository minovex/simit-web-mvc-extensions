//-----------------------------------------------------------------------
// <copyright file="LocalizationExtensions.cs" company="Minovex">
//     Copyright 2013 Minovex. All rights reserved.
// </copyright>
// <author>Mustafa Ozan Vural</author>
//-----------------------------------------------------------------------
namespace System.Web.Mvc.Html
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    #endregion Using Directives

    /// <summary>
    /// Extension Class
    /// </summary>
    public static class LocalizationExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Local resource Method.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// string key
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key string</exception>
        public static string LocalResource(this HtmlHelper helper, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            string pageVirtualPath = ((RazorView)helper.ViewContext.View).ViewPath;
            return helper.ViewContext.HttpContext.GetLocalResourceObject(pageVirtualPath, key) as string;
        }

        /// <summary>
        /// Local resource Method.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="key">The key.</param>
        /// <param name="replacements">The replacements.</param>
        /// <returns>
        /// resource variable
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key
        /// or
        /// replacements
        /// or</exception>
        public static string LocalResource(this HtmlHelper helper, string key, object replacements)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }

            string pageVirtualPath = ((RazorView)helper.ViewContext.View).ViewPath;
            string resource = helper.ViewContext.HttpContext.GetLocalResourceObject(pageVirtualPath, key) as string;

            if (resource == null)
            {
                throw new ArgumentNullException(key + " resource not found");
            }

            IDictionary<string, object> replacementList = TypeDescriptor.GetProperties(replacements)
                                                            .OfType<PropertyDescriptor>()
                                                            .ToDictionary(
                                                                prop => prop.Name,
                                                                prop => prop.GetValue(replacements)
                                                            );

            foreach (string replaceKey in replacementList.Keys)
            {
                resource = resource.Replace(string.Format("%{0}%", replaceKey), replacementList[replaceKey].ToString());
            }

            return resource;
        }

        #endregion Public Static Methods
    }
}