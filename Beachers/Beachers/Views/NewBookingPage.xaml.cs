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
        Dictionary<Label, string> labelToGearId = new Dictionary<Label, string>();
        Dictionary<Label, int> deploymentMapping = new Dictionary<Label, int>();
        Dictionary<string, Position> spotLocations = new Dictionary<string, Position>()
        {
            { "\"Event Site\" - Hood River, Oregon", new Position(45.715979, -121.512101) },
            { "\"Montones\" - Isabela, Puerto Rico", new Position(18.513870, -67.064946) },
            { "\"Longmen Beach\" - Penghu, Taiwan", new Position(23.568109, 119.682358) }
        };

        class SelectionStyle
        {
            public Color TextColor;
            public Color BackgroundColor;
        }

        SelectionStyle[] selectionStyles = new SelectionStyle[]
        {
            new SelectionStyle() { TextColor = Color.White, BackgroundColor = Color.CornflowerBlue },
            new SelectionStyle() { TextColor = Color.White, BackgroundColor = Color.CadetBlue },
            new SelectionStyle() { TextColor = Color.White, BackgroundColor = Color.Salmon },
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
            deploymentMapping.Clear();
            labelToGearId.Clear();

            foreach (var item in lastUpdatedGearRecords)
            {
                var newLabel = new Label()
                {
                    Text = string.Format("{0}: {1} {2}", item.Value.type, item.Value.brand, item.Value.size),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    FontSize = 25
                };

                var gesture = new TapGestureRecognizer();
                gesture.Tapped += Gesture_Tapped;
                newLabel.GestureRecognizers.Add(gesture);

                layoutGearLoadout.Children.Add(newLabel);
                deploymentMapping.Add(newLabel, -1);
                labelToGearId.Add(newLabel, item.Key);
            }
        }

        private void Gesture_Tapped(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;

            int newStyleIndex = ++deploymentMapping[lblSender];
            if (newStyleIndex >= selectionStyles.Length)
            {
                deploymentMapping[lblSender] = -1;
                lblSender.BackgroundColor = Color.Default;
                lblSender.TextColor = Color.Default;
            }
            else
            {
                lblSender.BackgroundColor = selectionStyles[newStyleIndex].BackgroundColor;
                lblSender.TextColor = selectionStyles[newStyleIndex].TextColor;
            }
        }


        string[] GetSelectedGearIds()
        {
            List<string> selectedGear = new List<string>();
            foreach (Label gearLabel in layoutGearLoadout.Children)
            {
                int deploymentGroup = deploymentMapping[gearLabel];

                if (deploymentGroup > -1)
                {
                    string gearId = labelToGearId[gearLabel];
                    selectedGear.Add(gearId);
                }
            }

            return selectedGear.ToArray();
        }

        private int[][] CalculateDeployments(string[] selectedGearIds)
        {
            var deploymentConfig = new Dictionary<int, List<int>>(); // <group, index into gear list>

            for (int i = 0; i < selectedGearIds.Length; i++)
            {
                foreach (Label gearLabel in layoutGearLoadout.Children)
                {
                    if (labelToGearId[gearLabel] == selectedGearIds[i])
                    {
                        //Match found. Get group ID.
                        int groupId = deploymentMapping[gearLabel];

                        if(!deploymentConfig.ContainsKey(groupId))
                        {
                            deploymentConfig.Add(groupId, new List<int>(1));
                        }
                        deploymentConfig[groupId].Add(i);
                        break;
                    }
                }
            }

            var keysAlias = deploymentConfig.Keys;
            var returnFormat = new int[keysAlias.Count][];

            int j = 0;
            foreach (int key in keysAlias)
            {
                returnFormat[j++] = deploymentConfig[key].ToArray();
            }
            return returnFormat;
        }
        private BookingModel GenerateBookingModel()
        {
            var model = new BookingModel();

            model.beacherId = "";
            model.gear = GetSelectedGearIds();
            model.deployments = CalculateDeployments(model.gear);
            model.location = new float[] { (float)DeploymentMap.Pins[0].Position.Longitude, (float)DeploymentMap.Pins[0].Position.Latitude };
            model.sessionLengthMinutes = 10;

            return model;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewBookingViewModel;

            DateTime final = vm.SelectedDate + vm.SelectedTime;
            string reservationTimeStamp = final.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
            
            db.CreateNewBooking(reservationTimeStamp, GenerateBookingModel());
            await DisplayAlert("Booking Request", "Reservation has been created.", "OK");
            await Navigation.PopAsync();
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