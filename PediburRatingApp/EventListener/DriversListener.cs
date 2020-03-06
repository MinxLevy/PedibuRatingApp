using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;            
using PediburRatingApp.DataModels;
using PediburRatingApp.Helpers;

namespace PediburRatingApp.EventListener
{
    class DriversListener : Java.Lang.Object, IValueEventListener
    {
        List<Drivers> DriversList = new List<Drivers>();

        public event EventHandler<DriversDataEventArgs> DriversRetrieved;
        public class DriversDataEventArgs : EventArgs
        {
            public List<Drivers> Drivers { get; set; } 
        }
        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
           if(snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                DriversList.Clear();
                foreach (DataSnapshot DriversData in child)
                {
                    Drivers drivers = new Drivers();
                    drivers.ID = DriversData.Key;
                    drivers.BodyNum = DriversData.Child("BodyNum").Value.ToString();
                    drivers.Name = DriversData.Child("Name").Value.ToString();
                    drivers.Location = DriversData.Child("Location").Value.ToString();
                    DriversList.Add(drivers);
                }
                DriversRetrieved.Invoke(this, new DriversDataEventArgs { Drivers = DriversList});
            }
        }

        public void Create()
        {
            DatabaseReference driversRef = AppDataHelper.GetDatabase().GetReference("Drivers");
            driversRef.AddValueEventListener(this);
        }
    }
}