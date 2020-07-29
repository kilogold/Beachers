using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Beachers.Models;
using Beachers.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Beachers.ViewModels
{
    public class BookingSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string sessionLength;
        public string SessionLength 
        {
            get {
                return sessionLength;
            }
            set
            {
                sessionLength = value;
                NotifyPropertyChanged();
            }
        }

        public Map DeploymentMap { get; set;}
        private DateTime bookingTimestamp;

        public BookingSummaryViewModel(string bookingTimestamp)
        {
            this.bookingTimestamp = DateTime.Parse(bookingTimestamp);
            DependencyService.Get<IFirebaseDB>().RegisterBookingSummaryListener(this, OnBookingModelUpdated, bookingTimestamp);

            Pin pin = new Pin
            {
                Label = "Deployment Location",
                Address = string.Empty,
                Type = PinType.Place,
                Position = new Position()
            };
            DeploymentMap = new Map();
            DeploymentMap.HeightRequest = 340;
            DeploymentMap.Pins.Add(pin);
        }

        private void OnBookingModelUpdated(BookingModel updatedModel)
        {
            //TODO: Support multi-day session time
            const string fmt = "h:mm tt";
            DateTime start = bookingTimestamp;
            DateTime end = bookingTimestamp.AddMinutes(updatedModel.sessionLengthMinutes);
            TimeSpan diff = end - start;

            SessionLength = string.Format("{4}hr {3}min ({0}-{1} {2})",
                start.ToString(fmt), 
                end.ToString(fmt), 
                "PST",
                diff.Minutes,
                diff.Hours);

            var mapPosition = new Position(updatedModel.location[0], updatedModel.location[1]);
            DeploymentMap.Pins[0].Position = mapPosition;
            DeploymentMap.MoveToRegion(MapSpan.FromCenterAndRadius(mapPosition, Distance.FromMiles(1)));
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
