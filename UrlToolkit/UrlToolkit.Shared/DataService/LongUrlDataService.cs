using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UrlToolkit.DataService.Entities;

namespace UrlToolkit.DataService
{
    public class LongUrlDataService : ILongUrlDataService
    {
        private static async Task<string> GetResponse(String requestUri, String userAgent)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (String.IsNullOrWhiteSpace(userAgent))
                        throw new Exception("User-Agent is required");
                    
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);  


                    HttpResponseMessage response = await client.GetAsync(new Uri(requestUri));
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception(HttpStatusCode.InternalServerError.ToString());
                        }
                        else
                        {
                            // Throw default exception for other errors
                            response.EnsureSuccessStatusCode();
                        }
                    }

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    // An unhandled exception has occurred; break into the debugger
                    System.Diagnostics.Debugger.Break();
                }

                throw;
            }
        }

        public async Task<IList<Service>> GetSupportedServicesList(ServicesFilter filter, String userAgent)
        {
            string servicesUri = LongUrlConstants.API_ENDPOINT + "/services";

            switch (filter.Format)
            {
                case ResponseFormat.XML:
                    servicesUri = servicesUri + "?format=xml";
                    break;
                case ResponseFormat.JSON:
                    servicesUri = servicesUri + "?format=json";
                    break;
                case ResponseFormat.PHP:
                    servicesUri = servicesUri + "?format=php";
                    break;
                default:
                    servicesUri = servicesUri + "?format=json";
                    break;
            }

            string responseBody = await GetResponse(servicesUri, userAgent);

            using (StreamReader reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(responseBody))))
            {
                String jsonString = reader.ReadToEnd();
                JObject parsedString = JObject.Parse(jsonString);

                ObservableCollection<Service> services = new ObservableCollection<Service>();
                foreach (JObject service in parsedString[0])
                {
                    Service mappedService = Service.mapItemToService(service);
                    services.Add(mappedService);
                }

                return services;
            }
        }

        public async Task<LongUrl> ExpandUrl(ExpandUrlFilter filter, String userAgent, Action onLoadingStarts, Action onLoadingEnds)
        {
            onLoadingStarts();

            filter.Url = filter.Url.ToLowerInvariant();

            if (!filter.Url.StartsWith("http://") && !filter.Url.StartsWith("https://"))
                throw new Exception("URL must start with http:// or https://.");

            String expanderUri = LongUrlConstants.API_ENDPOINT + "/expand";
            
            switch (filter.Format)
            {
                case ResponseFormat.XML:
                    expanderUri = expanderUri + "?format=xml";
                    break;
                case ResponseFormat.JSON:
                    expanderUri = expanderUri + "?format=json";
                    break;
                case ResponseFormat.PHP:
                    expanderUri = expanderUri + "?format=php";
                    break;
                default:
                    expanderUri = expanderUri + "?format=json";
                    break;
            }

            if (filter.AllRedirects == Argument.INCLUDE)
                expanderUri = expanderUri + "&all-redirects=1";

            if (filter.CanonicalUrl == Argument.INCLUDE)
                expanderUri = expanderUri + "&rel-canonical=1";

            if (filter.ContentType == Argument.INCLUDE)
                expanderUri = expanderUri + "&content-type=1";

            if (filter.HtmlTitle == Argument.INCLUDE)
                expanderUri = expanderUri + "&title=1";

            if (filter.MetaDescription == Argument.INCLUDE)
                expanderUri = expanderUri + "&meta-description=1";

            if (filter.MetaKeywords == Argument.INCLUDE)
                expanderUri = expanderUri + "&meta-keywords=1";

            if (filter.ResponseCode == Argument.INCLUDE)
                expanderUri = expanderUri + "&response-code=1";

            expanderUri = expanderUri + "&url=" + Uri.EscapeUriString(filter.Url);

            String responseBody = await GetResponse(expanderUri, userAgent);

            using (StreamReader reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(responseBody))))
            {
                String jsonString = reader.ReadToEnd();

                onLoadingEnds();
                return LongUrl.mapItemToLongUrl(JObject.Parse(jsonString));
            }
        }
    }
}
