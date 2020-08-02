using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewGearRegistrationPage : ContentPage
    {
        public List<string> GearTypes { get; private set; } = new List<string>() { "Kite", "Board" };
        public NewGearRegistrationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if( string.IsNullOrWhiteSpace(txtBrand.Text) || 
                string.IsNullOrWhiteSpace(txtMemo.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtSize.Text) ||
                cmbGearType.SelectedItem == null)
            {
                await DisplayAlert("Input Error", "Please check the fields for valid entries and try again.", "OK");
                return;
            }

            var model = new GearModel();
            model.Brand = txtBrand.Text;
            model.Memo = txtMemo.Text;
            model.Model = txtModel.Text;
            model.Size = txtSize.Text;
            model.Type = ((string)cmbGearType.SelectedItem) == GearTypes[0] ? GearType.Kite : GearType.Board;
            DependencyService.Get<IFirebaseDB>().RegisterNewGear(model);

            await DisplayAlert("New Gear Registered", "Your gear is now registered. You may select it in your booking.\nGear-shipping feature coming soon.", "OK");
            await Navigation.PopAsync();
        }
    }
}