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
        public ObservableCollection<string> Entries { get; private set; } = new ObservableCollection<string>();

        public BookingRecordsPage()
        {
            InitializeComponent();
            var db = DependencyService.Get<IFirebaseDB>();
            db.RegisterBookingsListener(this, OnBookingRecordsUpdated, false);
            lstBookings.ItemSelected += OnItemSelected;
            BindingContext = this;
        }

        private void OnBookingRecordsUpdated(Dictionary<string, BookingModel> bookingRecords)
        {
            Entries.Clear();
            foreach (string timestamp in bookingRecords.Keys)
            {
                var dateTime = DateTime.Parse(timestamp).ToLocalTime();
                Entries.Add(dateTime.ToString("f"));
            }
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