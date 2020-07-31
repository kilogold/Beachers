using Beachers.Models;
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
    using GearRecords = Dictionary<string, GearModel>;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBookingPage : ContentPage
    {
        IFirebaseDB db;
        GearRecords lastUpdatedGearRecords = null;
        Dictionary<string, Position> spotLocations = new Dictionary<string, Position>()
        {
            { "\"Event Site\" - Hood River, Oregon", new Position(45.715979, -121.512101) },
            { "\"Montones\" - Isabela, Puerto Rico", new Position(18.513870, -67.064946) },
            { "\"Longmen Beach\" - Penghu, Taiwan", new Position(23.568109, 119.682358) }
        };

        public NewBookingPage()
        {
            InitializeComponent();

            db = DependencyService.Get<IFirebaseDB>();

            db.RegisterUserInventoryListener(this, OnGearUpdated);

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

        private void OnGearUpdated(GearRecords gear)
        {
            lastUpdatedGearRecords = gear;

            layoutGearLoadout.Children.Clear();

            foreach (GearModel item in lastUpdatedGearRecords.Values)
            {
                var newLabel = new Label()
                {
                    Text = string.Format("{0}: {1} {2}", item.type, item.brand, item.size),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    FontSize = 25
                };


                var gesture = new TapGestureRecognizer();
                gesture.Tapped += Gesture_Tapped;
                newLabel.GestureRecognizers.Add(gesture);


                layoutGearLoadout.Children.Add(newLabel);
            }
        }

        private void Gesture_Tapped(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;

            if(lblSender.BackgroundColor == Color.CornflowerBlue)
            {
                lblSender.BackgroundColor = Color.White;
                lblSender.TextColor = Color.Default;
            }
            else
            {
                lblSender.BackgroundColor = Color.CornflowerBlue;
                lblSender.TextColor = Color.White;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewBookingViewModel;

            DateTime final = vm.SelectedDate + vm.SelectedTime;
            string foo = final.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
            db.CreateNewBooking(foo);
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