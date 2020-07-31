using Beachers.Services;
using Beachers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBookingPage : ContentPage
    {
        Dictionary<string, Position> spotLocations = new Dictionary<string, Position>()
        {
            { "\"Event Site\" - Hood River, Oregon", new Position(45.715979, -121.512101) },
            { "\"Montones\" - Isabela, Puerto Rico", new Position(18.513870, -67.064946) },
            { "\"Longmen Beach\" - Penghu, Taiwan", new Position(23.568109, 119.682358) }
        };

        public NewBookingPage()
        {
            InitializeComponent();

            DeploymentMap.Pins.Add(new Pin
            {
                Label = "Deployment Location",
                Address = string.Empty,
                Type = PinType.Place,
                Position = new Position()
            });

            foreach (var item in spotLocations)
            {
                var btn = new RadioButton { Text = item.Key, IsChecked = false };
                btn.CheckedChanged += RadioButton_Clicked;
                layoutRadioButtons.Children.Add(btn);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewBookingViewModel;

            DateTime final = vm.SelectedDate + vm.SelectedTime;
            string foo = final.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
            DependencyService.Get<IFirebaseDB>().CreateNewBooking(foo);
            await DisplayAlert("Booking Request", foo, "Ignore");
        }

        private void RadioButton_Clicked(object sender, CheckedChangedEventArgs e)
        {
            if(e.Value)
            {
                string locNam = (sender as RadioButton).Text;
                Position selectedLocation = spotLocations[locNam];

                DeploymentMap.Pins[0].Position = selectedLocation;
                DeploymentMap.MoveToRegion(MapSpan.FromCenterAndRadius(selectedLocation, Distance.FromMiles(0.5)));
            }
        }
    }
}