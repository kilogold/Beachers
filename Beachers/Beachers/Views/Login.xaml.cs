using Beachers.Models;
using Beachers.Services;
//using Firebase.Auth;
//using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        IFirebaseAuthentication auth;

        public Login()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            loginOutcome.Text = null;

            string token = await auth.LoginWithEmailAndPassword(email.Text, password.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
            }
        }
    }
}