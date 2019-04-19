using System;
using System.Collections.Generic;
using HelpSGF.Services;
using HelpSGF.Models;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using HelpSGF.ViewModels;
using System.Threading.Tasks;
using HelpSGF.Models.Search;

namespace HelpSGF.Views
{
    public partial class LocationsPage : ContentPage
    {
        public string Tester { get; set; }
        public DataService dataService = new DataService();
        ObservableCollection<LocationSearchResultItem> LocationSearchResultItems = new ObservableCollection<LocationSearchResultItem>();

        ResultsViewModel viewModel;


        public LocationsPage(ResultsViewModel viewModel)
        {
            //BindingContext = new ResultsViewModel();
            Tester = "Does this work?";
            InitializeComponent();
            Title = "Results";

            BindingContext = this.viewModel = viewModel;

            //Locations = dataService.GetLocations();
            //LocationsListView.ItemsSource = Locations;
        }

        void OnLocationSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is LocationSearchResultItem locationSelection))
                return;

            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Navigation.PushAsync(new LocationDetailPage(locationSelection.NiceURL));
        }

        public async Task GoToLocationPageAsync(string NiceURL)
        {
            var location = await dataService.GetLocationAsync(NiceURL);


            //await Navigation.PushAsync(new LocationDetailPage(location));

            // Manually deselect item.
            LocationsListView.SelectedItem = null;
        }

        public void OnFilterTap(object sender, EventArgs args)
        {
            var categoryFilterPage = new NavigationPage(new CategoryFilterPage())
            {
                BarBackgroundColor = Color.FromHex("#B4E2D9"),
                BarTextColor = Color.Black
            };

            Navigation.PushModalAsync(categoryFilterPage);
        }

        public void OnFilterItemTap(object sender, EventArgs args)
        {
            var view = sender as View;
            var filter = view?.BindingContext as string;
            DisplayAlert("Feature Coming Soon", "You have selected " + filter, "Ok");
        }
    }
}
