using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            db.RegisterBookingsListener(this, something, true);
            lstBookings.ItemSelected += OnItemSelected;
        }

        private void something(Dictionary<string, BookingModel> obj)
        {
            lstBookings.ItemsSource = obj.Keys;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lstBookings.SelectedItem == null)
                return;

            Navigation.PushAsync(new NavigationPage(new BookingSummaryPage()));

            lstBookings.SelectedItem = null;
        }

        private void Reservation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new NewBookingPage()));
        }
    }
}