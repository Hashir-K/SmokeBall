using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeBall.BusinessLogic.Services.Interfaces
{
    public interface IKeywordSearchService
    {
        public Task<int> GetSearchCount(string uri, string keyword, int resultCount);
    }
}
