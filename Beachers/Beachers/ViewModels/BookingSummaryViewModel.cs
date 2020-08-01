﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Beachers.Models;
using Beachers.Services;
using Beachers.Utils;
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
        public DateTime BookingTimestamp { get; set; }

        private Dictionary<string, GearModel> cachedUserGearInventory;
        private BookingModel cachedBookingModel;

        public BookingSummaryViewModel(string bookingTimestamp)
        {
            this.BookingTimestamp = DateTime.Parse(bookingTimestamp);
            var db = DependencyService.Get<IFirebaseDB>();
            string dbKey = StringOperations.ToUTCFormat(BookingTimestamp.ToUniversalTime());
            db.RegisterBookingSummaryListener(this, OnBookingModelUpdated, dbKey);
            db.RegisterUserInventoryListener(this, OnUserGearInventoryUpdated);

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

        private void OnUserGearInventoryUpdated(Dictionary<string, GearModel> obj)
        {
            cachedUserGearInventory = obj;
        }

        private void OnBookingModelUpdated(BookingModel updatedModel)
        {
            cachedBookingModel = updatedModel;

            //TODO: Support multi-day session time
            const string fmt = "h:mm tt";
            DateTime start = BookingTimestamp;
            DateTime end = BookingTimestamp.AddMinutes(updatedModel.sessionLengthMinutes);
            TimeSpan diff = end - start;

            SessionLength = string.Format("{4}hr {3}min ({0}-{1} {2})",
                start.ToString(fmt), 
                end.ToString(fmt), 
                "PST",
                diff.Minutes,
                diff.Hours);


            double longitude = updatedModel.location[0];
            double latitude = updatedModel.location[1];
            var mapPosition = new Position(latitude, longitude);
            DeploymentMap.Pins[0].Position = mapPosition;
            DeploymentMap.MoveToRegion(MapSpan.FromCenterAndRadius(mapPosition, Distance.FromMiles(1)));
            NotifyPropertyChanged("DeploymentMap");
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
