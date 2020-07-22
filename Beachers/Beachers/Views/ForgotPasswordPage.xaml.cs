using Beachers.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void Button_Reset(object sender, EventArgs e)
        {
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            auth.ResetPassword(email.Text);

            await DisplayAlert("Reset Request Sent", $"Password reset link has been sent to {email.Text}.\nPlease check your inbox for further instructions." , "OK");
        }
    }
}