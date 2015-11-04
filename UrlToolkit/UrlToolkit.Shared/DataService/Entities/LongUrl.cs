using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace UrlToolkit.DataService.Entities
{
    /// <example>
    /// {
    ///     "long-url": "http://www.google.com/",
    ///     "all-redirects": [
    ///         "http://www.google.com/"
    ///     ],
    ///     "content-type": "text/html; charset=ISO-8859-1",
    ///     "title": "Google",
    ///     "meta-description": "Search the world's information, including webpages, images, videos and more. Google has many special features to help you find exactly what you're looking for."
    /// }  
    /// </example>
    public class LongUrl
    {
        /// <summary> Long Url </summary>
        /// <remarks> Json Reponse Name: long-url </remarks>
        public String Url { get; set; }

        /// <summary> All redirect </summary>
        /// <remarks> Json Reponse Name: all-redirect </remarks>
        public List<String> AllRedirect { get; set; }

        /// <summary> Content type </summary>
        /// <remarks> Json Reponse Name: content-type </remarks>
        public String ContentType { get; set; }

        /// <summary> Title </summary>
        /// <remarks> Json Reponse Name: title </remarks>
        public String HtmlTitle { get; set; }

        /// <summary> Meta description </summary>
        /// <remarks> Json Reponse Name: meta-description </remarks>
        public String MetaDescription { get; set; }

        /// <summary> Response Code </summary>
        /// <remarks> Json Reponse Name: response-code </remarks>
        public String ResponseCode { get; set; }

        /// <summary> Canonical Url </summary>
        /// <remarks> Json Reponse Name: rel-canonical </remarks>
        public String CanonicalUrl { get; set; }

        /// <summary> Meta Keywords </summary>
        /// <remarks> Json Reponse Name: meta-keywords </remarks>
        public String MetaKeywords { get; set; }

        /// <summary> Parses an url json string </summary>
        public static LongUrl mapItemToLongUrl(JObject longUrlItem)
        {
            LongUrl parsedLongUrl = new LongUrl();

            parsedLongUrl.Url = longUrlItem["long-url"] == null ? String.Empty : (String)longUrlItem["long-url"];
            parsedLongUrl.ContentType = longUrlItem["content-type"] == null ? String.Empty : (String)longUrlItem["content-type"];
            parsedLongUrl.HtmlTitle = longUrlItem["title"] == null ? String.Empty : (String)longUrlItem["title"];
            parsedLongUrl.MetaDescription = longUrlItem["meta-description"] == null ? String.Empty : (String)longUrlItem["meta-description"];
            parsedLongUrl.ResponseCode = longUrlItem["response-code"] == null ? String.Empty : (String)longUrlItem["response-code"];
            parsedLongUrl.CanonicalUrl = longUrlItem["rel-canonical"] == null ? String.Empty : (String)longUrlItem["rel-canonical"];
            parsedLongUrl.MetaKeywords = longUrlItem["meta-keywords"] == null ? String.Empty : (String)longUrlItem["meta-keywords"];

            List<String> allRedirectsList = new List<String>();
            if (longUrlItem["all-redirect"] != null)
            {
                foreach (JObject item in (JArray)longUrlItem["all-redirect"])
                {
                    allRedirectsList.Add((String)item);
                }
            }

            return parsedLongUrl;
        }
    }
}
