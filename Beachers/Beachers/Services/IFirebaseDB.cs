using Beachers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Services
{
    using BookingRecords = Dictionary<string, BookingModel>;
    public interface IFirebaseDB
    {
        void RegisterBookingsListener(object sender, Action<BookingRecords> recs, bool registerOnce);

        void RegisterBookingSummaryListener(object sender, Action<BookingModel> updateCallback, string bookingTimestamp);
    }
}
