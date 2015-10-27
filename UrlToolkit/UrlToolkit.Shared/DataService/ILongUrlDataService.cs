using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlToolkit.DataService.Entities;

namespace UrlToolkit.DataService
{
    public interface ILongUrlDataService
    {
        Task<LongUrl> ExpandUrl(LongUrlFilter filter, Action onLoadingStarts, Action onLoadingEnds);

        Task<IList<Service>> GetSupportedServicesList(BaseFilter filter);
    }
}
