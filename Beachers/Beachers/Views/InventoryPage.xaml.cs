using Beachers.ViewModels;
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
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
            BindingContext = new InventoryViewModel();
        }

        private async void RegisterNewGear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewGearRegistrationPage());
        }

        private async void ReturnGear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReturnGearPage());
        }
    }
}