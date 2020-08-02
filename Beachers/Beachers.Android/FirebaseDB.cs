using Android.Util;
using Beachers.Models;
using Beachers.Services;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using Java.Util;

namespace Beachers.Droid
{
    using BookingRecords = Dictionary<string, BookingModel>;
    using GearRecords = Dictionary<string, GearModel>;

    static class BookingModelParser
    {
        static internal BookingModel ParseBooking(DataSnapshot bookingElements)
        {
            BookingModel model = new BookingModel();
            foreach (DataSnapshot item in bookingElements.Children.ToEnumerable())
            {
                if (item.Key == "beacher")
                {
                    model.beacherId = (string)item.Value;
                    continue;
                }

                if (item.Key == "deployments")
                {
                    model.deployments = ParseDeployments(item);
                    continue;
                }

                if (item.Key == "gear")
                {
                    model.gear = ParseGear(item);
                    continue;
                }

                if (item.Key == "lengthMinutes")
                {
                    model.sessionLengthMinutes = (int)(item.GetValue(Java.Lang.Class.FromType(typeof(Java.Lang.Long))));
                    continue;
                }

                if (item.Key == "location")
                {
                    string locationString = (string)item.Value;
                    string[] longitudeLatitude = locationString.Split(',');
                    model.location = new float[] {
                        float.Parse(longitudeLatitude[0]),
                        float.Parse(longitudeLatitude[1])
                    };
                    continue;
                }
            }
            return model;
        }
        static internal string[] ParseGear(DataSnapshot gear)
        {
            string[] gearListing = new string[gear.ChildrenCount];
            int curIdx = 0;
            foreach (DataSnapshot gearUID in gear.Children.ToEnumerable())
            {
                gearListing[curIdx++] = (string)gearUID.Value;
            }

            return gearListing;
        }
        static internal int[][] ParseDeployments(DataSnapshot deployments)
        {
            int[][] deploymentIndices = new int[deployments.ChildrenCount][];
            int curGroup = 0;
            foreach (DataSnapshot deploymentGroup in deployments.Children.ToEnumerable())
            {
                deploymentIndices[curGroup] = new int[deploymentGroup.ChildrenCount];
                foreach (DataSnapshot deploymentGearIndex in deploymentGroup.Children.ToEnumerable())
                {
                    deploymentIndices[curGroup][int.Parse(deploymentGearIndex.Key)] = (int)deploymentGearIndex.Value;
                }

                ++curGroup;
            }
            return deploymentIndices;
        }

    }
    public class BookingRecordsEventListener : Java.Lang.Object, IValueEventListener
    {
        private Action<BookingRecords> updatedModelCallback;
        public BookingRecordsEventListener(Action<BookingRecords> updatedCallback)
        {
            updatedModelCallback = updatedCallback;
        }

        public void OnCancelled(DatabaseError error)
        {
            Log.Warn("Beachers", "Failed to read value:", error.ToException());
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            BookingRecords data = new BookingRecords();

            foreach (DataSnapshot timestamp in snapshot.Children.ToEnumerable())
            {
                data.Add(timestamp.Key, BookingModelParser.ParseBooking(timestamp));
            }

            updatedModelCallback.Invoke(data);
        }
    }
    public class BookingModelEventListener : Java.Lang.Object, IValueEventListener
    {
        private Action<BookingModel> updatedModelCallback;
        public BookingModelEventListener(Action<BookingModel> updatedCallback)
        {
            updatedModelCallback = updatedCallback;
        }

        public void OnCancelled(DatabaseError error)
        {
            Log.Warn("Beachers", "Failed to read value:", error.ToException());
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            BookingModel updatedModel = BookingModelParser.ParseBooking(snapshot);
            updatedModelCallback.Invoke(updatedModel);
        }
    }

    static class GearModelParser
    {
        static internal GearModel ParseGear(DataSnapshot gearInventoryRoot)
        {
            GearModel outData = new GearModel();
            foreach(DataSnapshot child in gearInventoryRoot.Children.ToEnumerable())
            {
                if (child.Key == "memo")
                {
                    outData.Memo = (string)child.Value;
                    continue;
                }

                if (child.Key == "brand")
                {
                    outData.Brand = (string)child.Value;
                    continue;
                }

                if (child.Key == "size")
                {
                    outData.Size = (string)child.Value;
                    continue;
                }

                if (child.Key == "model")
                {
                    outData.Model = (string)child.Value;
                    continue;
                }

                if (child.Key == "type")
                {
                    string typeVal = (string)child.Value;
                    outData.Type = typeVal == "Kite" ? GearType.Kite : GearType.Board;
                    continue;
                }
            }
            return outData;
        }
    }

    public class GearRecordsEventListener : Java.Lang.Object, IValueEventListener
    {
        private Action<GearRecords> updatedModelCallback;

        public GearRecordsEventListener(Action<GearRecords> updatedModelCallback)
        {
            this.updatedModelCallback = updatedModelCallback;
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot gearInventoryRoot)
        {
            GearRecords outData = new GearRecords((int)gearInventoryRoot.ChildrenCount);

            foreach (DataSnapshot gearDescription in gearInventoryRoot.Children.ToEnumerable())
            {
                outData.Add(gearDescription.Key, GearModelParser.ParseGear(gearDescription));
            }

            updatedModelCallback.Invoke(outData);
        }
    }


    // TODO: Implement 'Unregister' logic.
    public class FirebaseDB : IFirebaseDB
    {
        private const string urlFirebaseDB = "https://beachers-49bec.firebaseio.com";

        //Only supports booking atm...
        public void RegisterBookingsListener(object sender, Action<BookingRecords> updateCallback, bool registerOnce)
        {
            BookingRecordsEventListener bookingsListener = new BookingRecordsEventListener(updateCallback);

            if(registerOnce)
                GetCurrentUserBookings().AddListenerForSingleValueEvent(bookingsListener);
            else
                GetCurrentUserBookings().AddValueEventListener(bookingsListener);
        }

        public void RegisterBookingSummaryListener(object sender, Action<BookingModel> updateCallback, string bookingTimestamp)
        {
            BookingModelEventListener bookingsListener = new BookingModelEventListener(updateCallback);
            GetCurrentUserBookings().Child(bookingTimestamp).AddValueEventListener(bookingsListener);
        }

        public void RegisterUserInventoryListener(object sender, Action<GearRecords> updateCallback)
        {
            var listener = new GearRecordsEventListener(updateCallback);
            GetCurrentUserInventory().AddValueEventListener(listener);
        }

        public void CreateNewBooking(string timestamp, BookingModel model)
        {
            var bookings = GetCurrentUserBookings();
            var bookingDbEntry = bookings.Child(timestamp);

            bookingDbEntry.Child("beacher").SetValue(model.beacherId);
            for (int i = 0; i < model.deployments.Length; i++)
            {
                ArrayList lst = new ArrayList(model.deployments[i]);
                bookingDbEntry.Child("deployments").Child(i.ToString()).SetValue(lst);
            }
            bookingDbEntry.Child("gear").SetValue(new ArrayList(model.gear));
            bookingDbEntry.Child("lengthMinutes").SetValue(model.sessionLengthMinutes);
            bookingDbEntry.Child("location").SetValue($"{model.location[0]},{model.location[1]}");
        }

        public void RegisterNewGear(GearModel model)
        {
            var data = new Dictionary<string, Java.Lang.Object>();
            
            data.Add("brand", model.Brand);
            data.Add("memo", model.Memo);
            data.Add("model", model.Model);
            data.Add("size", model.Size);
            data.Add("type", model.Type.ToString());
            GetCurrentUserInventory().Push().UpdateChildren(data);
        }

        private DatabaseReference GetCurrentUserInventory()
        {
            return FirebaseDatabase.Instance.GetReferenceFromUrl($"{urlFirebaseDB}/Inventory/{FirebaseAuth.Instance.CurrentUser.Uid}/");
        }

        private DatabaseReference GetCurrentUserBookings()
        {
            return FirebaseDatabase.Instance.GetReferenceFromUrl($"{urlFirebaseDB}/Booking/{FirebaseAuth.Instance.CurrentUser.Uid}/");
        }
    }
}