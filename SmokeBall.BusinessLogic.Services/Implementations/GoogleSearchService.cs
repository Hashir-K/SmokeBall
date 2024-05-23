using SmokeBall.BusinessLogic.Services.Interfaces;
using SmokeBall.Common.DTOs;
using SmokeBall.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SmokeBall.BusinessLogic.Services.Implementations
{   
    public class GoogleSearchService : IKeywordSearchService
    { 
        private readonly string _searchUrl;
        
        public GoogleSearchService(string searchUrl) 
        { 
            _searchUrl = searchUrl;
        }

        //Responsible for:
        //validating parameters
        //encoding keyword parameter
        //searching the keyword in Google
        //search uri in the results and return the count
        public async Task<int> GetSearchCount(string uri, string keywords, int resultCount)
        {
            ValidateRequest(uri, keywords, resultCount);

            var apiClient = new ApiClient();

            //"https://www.google.com.au/search?num=100&q=conveyancing+software";
            var searchUri = _searchUrl;

            if (resultCount > 0) searchUri += $"num={resultCount}&";

            if (!String.IsNullOrEmpty(keywords)) searchUri += $"q={HttpUtility.UrlEncode(keywords)}";

            var html = await apiClient.Get(searchUri);

            var mc = Regex.Matches(html, Regex.Escape(uri));

            return mc.Count;
        }

        private void ValidateRequest(string uri, string keywords, int resultCount)
        {
            if (resultCount <= 0)
            {
                throw new Exception("Number or results cannot be 0 or less.");
            }

            if (String.IsNullOrEmpty(keywords))
            {
                throw new Exception("Please provide search keywords.");
            }

            if (String.IsNullOrEmpty(uri))
            {
                throw new Exception("Please provide URL to search for.");
            }
        }
    }
}
