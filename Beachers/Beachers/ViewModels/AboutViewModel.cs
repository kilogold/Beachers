using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Beachers.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string UserFirstName { get; set; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            ToInventory = new Command(async () => await (Application.Current.MainPage as Views.MainPage).NavigateFromMenu(Models.MenuItemType.Inventory));
            ToBooking = new Command(async () => await (Application.Current.MainPage as Views.MainPage).NavigateFromMenu(Models.MenuItemType.Booking));
            UserFirstName = DependencyService.Get<Services.IFirebaseAuthentication>().UserFirstName;

        }

        public ICommand OpenWebCommand { get; }
        public ICommand ToInventory { get; }
        public ICommand ToBooking { get; }
    }
}