using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlToolkit.DataService.Entities;

namespace UrlToolkit.DataService
{
    public interface ILongUrlDataService
    {
        Task<LongUrl> ExpandUrl(ExpandUrlFilter filter, String userAgent, Action onLoadingStarts, Action onLoadingEnds);

        Task<IList<Service>> GetSupportedServicesList(ServicesFilter filter, String userAgent);
    }
}
