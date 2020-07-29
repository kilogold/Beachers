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

        const int TotalBooking = 40;
        List<BookingModelProto> bookings = new List<BookingModelProto>(TotalBooking);


        public BookingRecordsPage()
        {
            InitializeComponent();

            for (int i = 0; i < TotalBooking; i++)
            {
                bookings.Add(new BookingModelProto("08/24/1989"));
            }

            lstBookings.ItemsSource = bookings;
            lstBookings.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lstBookings.SelectedItem == null)
                return;
            
            Navigation.PushAsync(new NavigationPage(new BookingSummaryPage()));

            lstBookings.SelectedItem = null;
        }
    }

    public class BookingModelProto
    {
        public BookingModelProto(string date)
        {
            Date = date;
        }
        public string Date { get; }
    }
}