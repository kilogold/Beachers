using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;

namespace Beachers.Droid
{
    public class BookingModel
    {
        public string beacherId;
        public string[] gear;
        public int[][] deployments;
        public int sessionLengthMinutes;
        public float[] location;
    }

    public class BookingModelEventListener : Java.Lang.Object, IValueEventListener
    {
        Dictionary<string, BookingModel> data = new Dictionary<string, BookingModel>();
        public void OnCancelled(DatabaseError error)
        {
            Log.Warn("Beachers", "Failed to read value:", error.ToException());
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            foreach (DataSnapshot timestamp in snapshot.Children.ToEnumerable())
            {
                data.Add(timestamp.Key, ParseBooking(timestamp));
            }
        }

        private BookingModel ParseBooking(DataSnapshot bookingElements)
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
                    model.sessionLengthMinutes = (int)item.Value;
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

        private string[] ParseGear(DataSnapshot gear)
        {
            string[] gearListing = new string[gear.ChildrenCount];
            int curIdx = 0;
            foreach (DataSnapshot gearUID in gear.Children.ToEnumerable())
            {
                gearListing[curIdx++] = (string)gearUID.Value;
            }

            return gearListing;
        }

        private int[][] ParseDeployments(DataSnapshot deployments)
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

    class FirebaseDB
    {
        static DatabaseReference booking;
        static BookingModelEventListener userListener;
        public static void Test()
        {
            
            var root = Firebase.Database.FirebaseDatabase.Instance.GetReferenceFromUrl("https://beachers-49bec.firebaseio.com/");
            string userID = Xamarin.Forms.DependencyService.Get<Services.IFirebaseAuthentication>().UserID;
            booking = root.Child("Booking").Child(userID);

            userListener = new BookingModelEventListener();

            booking.AddListenerForSingleValueEvent(userListener);
        }
    }
}