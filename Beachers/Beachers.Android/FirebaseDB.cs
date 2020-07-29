﻿using System;
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
using Beachers.Models;
using Beachers.Services;
using Firebase.Auth;
using Firebase.Database;

namespace Beachers.Droid
{
    using BookingRecords = Dictionary<string, BookingModel>;
    
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

    public class FirebaseDB : IFirebaseDB
    {
        private const string urlFirebaseDB = "https://beachers-49bec.firebaseio.com";

        //Only supports booking atm...
        public void RegisterBookingsListener(object sender, Action<BookingRecords> updateCallback)
        {
            BookingRecordsEventListener bookingsListener = new BookingRecordsEventListener(updateCallback);
            GetCurrentUserBookings().AddValueEventListener(bookingsListener);
        }

        public void RegisterBookingSummaryListener(object sender, Action<BookingModel> updateCallback, string bookingTimestamp)
        {
            BookingModelEventListener bookingsListener = new BookingModelEventListener(updateCallback);
            GetCurrentUserBookings().Child(bookingTimestamp).AddValueEventListener(bookingsListener);
        }

        private DatabaseReference GetCurrentUserBookings()
        {
            return FirebaseDatabase.Instance.GetReferenceFromUrl($"{urlFirebaseDB}/Booking/{FirebaseAuth.Instance.CurrentUser.Uid}/");
        }
    }
}