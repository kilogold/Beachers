using Beachers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Services
{
    using BookingRecords = Dictionary<string, BookingModel>;
    using GearRecords = Dictionary<string, GearModel>;
    public interface IFirebaseDB
    {
        void RegisterBookingsListener(object sender, Action<BookingRecords> recs, bool registerOnce);

        void RegisterBookingSummaryListener(object sender, Action<BookingModel> updateCallback, string bookingTimestamp);

        void CreateNewBooking(string timestamp, BookingModel model);

        void RegisterUserInventoryListener(object sender, Action<GearRecords> updateCallback);

        void RegisterNewGear(GearModel model);

        void RemoveGearFromInventory(string gearId);

    }
}
