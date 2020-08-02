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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            btnRegister.IsEnabled = false;
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            string token = await auth.RegisterWithEmailAndPassword(email.Text, password.Text);

            const string ErrorPrefix = "Error:";
            if (token.StartsWith(ErrorPrefix))
            {
                await DisplayAlert("Registration Error", token.Substring(ErrorPrefix.Length), "OK");
                btnRegister.IsEnabled = true;
                return;
            }
            await auth.UpateProfile(fName.Text, null);
            btnRegister.IsEnabled = true;
            await DisplayAlert("Registration Successful!", "Please log in with your exiting account.", "OK");
            await Navigation.PopAsync();
        }
    }
}