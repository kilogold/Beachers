using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Booking : ContentPage
    {
        IFirebaseAuthentication auth;

        public Booking()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            string userAlias = auth.UserID;
            lblWelcome.Text = $"Welcome, {userAlias}!";
        }
    }
}