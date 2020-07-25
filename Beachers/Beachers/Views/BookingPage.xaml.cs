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
    public partial class BookingPage : ContentPage
    {
        public BookingPage()
        {
            InitializeComponent();
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new BookingItemSummary()));
        }
    }
}