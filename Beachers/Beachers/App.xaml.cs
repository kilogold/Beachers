using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Beachers.Services;
using Beachers.Views;

namespace Beachers
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage(true));
            //MainPage = new NavigationPage(new NewBookingPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
