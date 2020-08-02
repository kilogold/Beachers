using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnGearPage : ContentPage
    {
        public ObservableCollection<GearModel> UserInventory { get; private set; } = new ObservableCollection<GearModel>();

        private IFirebaseDB db;
        private Dictionary<string, GearModel> cachedGearRecords;

        public ReturnGearPage()
        {
            InitializeComponent();
            BindingContext = this;
            db = DependencyService.Get<IFirebaseDB>();
            db.RegisterUserInventoryListener(this, OnUserGearInventoryUpdated);
        }

        private void OnUserGearInventoryUpdated(Dictionary<string, GearModel> gearInventory)
        {
            cachedGearRecords = gearInventory;
            UserInventory.Clear();

            foreach (var item in gearInventory.Values)
            {
                UserInventory.Add(item);
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GearModel selectedGear = UserInventory[e.SelectedItemIndex];
            string gearId = null;
            foreach (var item in cachedGearRecords)
            {
                if(item.Value == selectedGear)
                {
                    gearId = item.Key;
                    break;
                }
            }

            bool confirmDelete = await DisplayAlert(string.Empty, $"Do you wish to process a return for {selectedGear.Model} {selectedGear.Brand}?", "YES", "NO");

            if (confirmDelete)
            {
                db.RemoveGearFromInventory(gearId);
            }
        }
    }
}