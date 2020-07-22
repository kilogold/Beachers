﻿using Beachers.Models;
using Beachers.Services;
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

        private async void Button_Login(object sender, EventArgs e)
        {
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

        private void Button_Register(object sender, EventArgs e)
        {
            var navigationPage = new NavigationPage(new RegisterPage());
            Navigation.PushAsync(navigationPage);
        }

        private void Button_ForgotPassword(object sender, EventArgs e)
        {
            var navigationPage = new NavigationPage(new ForgotPasswordPage());
            Navigation.PushAsync(navigationPage);
        }
    }
}