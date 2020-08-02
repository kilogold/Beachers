using Beachers.Models;
using Beachers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Beachers.ViewModels
{
    class InventoryViewModel : BaseViewModel
    {
        public ObservableCollection<GearModel> UserInventory { get; private set; } = new ObservableCollection<GearModel>();

        public InventoryViewModel()
        {
            var db = DependencyService.Get<IFirebaseDB>();
            db.RegisterUserInventoryListener(this, OnUserGearInventoryUpdated);
        }

        private void OnUserGearInventoryUpdated(Dictionary<string, GearModel> gearInventory)
        {
            UserInventory.Clear();

            foreach (var item in gearInventory.Values)
            {
                UserInventory.Add(item);
            }
        }
    }
}
