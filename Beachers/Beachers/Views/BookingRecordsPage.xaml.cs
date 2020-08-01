using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingRecordsPage : ContentPage
    {
        public BookingRecordsPage()
        {
            InitializeComponent();
            var db = DependencyService.Get<IFirebaseDB>();
            db.RegisterBookingsListener(this, OnBookingRecordsUpdated, false);
            lstBookings.ItemSelected += OnItemSelected;
        }

        private void OnBookingRecordsUpdated(Dictionary<string, BookingModel> bookingRecords)
        {
            string[] reformattedDates = new string[bookingRecords.Keys.Count];
            bookingRecords.Keys.CopyTo(reformattedDates, 0);

            for (int i = 0; i < reformattedDates.Length; i++)
            {
                reformattedDates[i] = DateTime.Parse(reformattedDates[i]).Date.ToString("f");
            }
            lstBookings.ItemsSource = reformattedDates;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lstBookings.SelectedItem == null)
                return;

            string timestamp = lstBookings.SelectedItem as string;
            Navigation.PushAsync(new NavigationPage(new BookingSummaryPage(timestamp)));

            lstBookings.SelectedItem = null;
        }

        private async void Reservation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewBookingPage());
        }
    }
}