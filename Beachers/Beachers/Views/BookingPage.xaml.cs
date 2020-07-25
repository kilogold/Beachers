using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Xamarin.Forms.Maps;
namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : ContentPage
    {
        public BookingPage()
        {
            InitializeComponent();

            string[] reservationBtns = new string[100];
            for (int i = 0; i < 100; i++)
            {
                reservationBtns[i] = $"Btn{i}";
            }

            lstReservations.ItemsSource = reservationBtns;
        }

        //protected override void OnAppearing()
        //{
        //    var auth = DependencyService.Get<IFirebaseAuthentication>();
        //    base.OnAppearing();
        //    string userAlias = auth.UserFirstName;
        //    lblWelcome.Text = $"Welcome, {userAlias}!";
        //}
    }
}