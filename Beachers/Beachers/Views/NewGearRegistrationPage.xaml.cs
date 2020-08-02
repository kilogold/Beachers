﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
    }
}