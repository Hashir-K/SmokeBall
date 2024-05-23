using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmokeBall.Common.DTOs;
using SmokeBall.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmokeBall.Presentation.Windows.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {
        public MainWindowVM()
        {
            SearchRequest = new SearchRequest
            {
                Keywords = "conveyancing software",
                URI = "www.smokeball.com.au",
                NumberOfResults = 100
            };

            SearchingVisibility = Visibility.Hidden;
        }

        [ObservableProperty] public SearchRequest searchRequest;

        [ObservableProperty] public string information;

        [ObservableProperty] public Visibility searchingVisibility;

        public Window VMWindow { get; set; }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                //reset previous info
                Information = String.Empty;

                //validate search params
                ValidateSearchRequest();

                //show information
                SearchingVisibility = Visibility.Visible;

                //performs search
                var apiClient = new ApiClient();                
                var response = await apiClient.Post<SearchResponse>($"{SearchEndpoint}search", SearchRequest);

                //display results
                Information = $"{SearchRequest.URI} was found {response.Count} times in the search!";

            }
            catch (Exception ex)
            {
                //show user friendly message instead of technical one
                MessageBox.Show("Oops! There was an error processing the request. Please try again."); 
            }
            finally
            {
                //hide searching message
                SearchingVisibility = Visibility.Hidden;
            }
        }

        private string SearchEndpoint
        {
            get
            {
                return ConfigurationManager.AppSettings["SearchEndpoint"].ToString();
            }
        }

        private void ValidateSearchRequest()
        {
            if (SearchRequest.NumberOfResults <= 0)
            {
                throw new Exception("Number or results cannot be 0 or less.");
            }

            if (String.IsNullOrEmpty(SearchRequest.Keywords))
            {
                throw new Exception("Please provide search keywords.");
            }

            if (String.IsNullOrEmpty(SearchRequest.URI))
            {
                throw new Exception("Please provide URL to search for.");
            }
        }

    }
}
