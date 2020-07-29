using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Beachers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingSummaryPage : ContentPage, INotifyPropertyChanged
    {
        private void OnBookingModelUpdated(Dictionary<string, BookingModel> updatedModel)
        {
            lblMins.Text = updatedModel.Values.First().sessionLengthMinutes.ToString(); // just to test.
        }

        public ObservableCollection<Deployment> GearList { get; set; } = Deployment.All;

        public BookingSummaryPage()
        {
            InitializeComponent();
            var mapPosition = new Position(45.715979, -121.512101);
            Pin pin = new Pin
            {
                Label = "Deployment Location",
                Address = string.Empty,
                Type = PinType.Place,
                Position = mapPosition
            };
            map.Pins.Add(pin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(mapPosition, Distance.FromMiles(1)));
            BindingContext = this;

            DependencyService.Get<IFirebaseDB>().RegisterBookingsListener(this, OnBookingModelUpdated);
            lblMins.Text = "5";
        }
    }


    public class GearModel
    {
        public GearModel(string type, string detail)
        {
            this.type = type;
            this.detail = detail;
        }

        public string type { get; set; }
        public string detail { get; set; }
    }


    public class Deployment : List<GearModel>
    {
        public string Name { get; set; }
        
        private Deployment(string title)
        {
            Name = title;
        }

        public static ObservableCollection<Deployment> All { private set; get; }

        static Deployment()
        {
            var Groups = new ObservableCollection<Deployment>
            {
                new Deployment ("Deployment1")
                {
                    new GearModel("Board", "Wainman Hawaii Twintip 138cm")
                },
                new Deployment ("Deployment2")
                {
                    new GearModel("Kite", "Core XR6 (12m)"),
                    new GearModel("Board", "F-One Traxx 268cm")
                }
            };
            All = Groups; //set the publicly accessible list
        }
    }
}