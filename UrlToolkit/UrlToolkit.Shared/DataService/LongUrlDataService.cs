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
        private static async Task<string> GetResponse(string requestUri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
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

        public async Task<IList<Service>> GetSupportedServicesList(BaseFilter filter)
        {
            string servicesUri = LongUrlConstants.API_ENDPOINT + "/services?format=json";

            string responseBody = await GetResponse(servicesUri);

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

        public async Task<LongUrl> ExpandUrl(LongUrlFilter filter, Action onLoadingStarts, Action onLoadingEnds)
        {
            onLoadingStarts();

            String expanderUri = LongUrlConstants.API_ENDPOINT + "/expand?format=json&url=" + Uri.EscapeUriString(filter.shortenedUrl);
            String responseBody = await GetResponse(expanderUri);

            using (StreamReader reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(responseBody))))
            {
                String jsonString = reader.ReadToEnd();

                onLoadingEnds();
                return LongUrl.mapItemToLongUrl(JObject.Parse(jsonString));
            }
        }
    }
}
