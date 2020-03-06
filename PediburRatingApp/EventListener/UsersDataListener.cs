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
    class UsersDataListener : Java.Lang.Object, IValueEventListener
    {
        List<UsersData> UsersList = new List<UsersData>();

        public event EventHandler<UsersDataEventArgs> UsersRetrieved;
        public class UsersDataEventArgs : EventArgs
        {
            public List<UsersData> Users { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                UsersList.Clear();
                foreach (DataSnapshot UsersData in child)
                {
                    UsersData users = new UsersData();
                    users.key  = UsersData.Key;
                    users.fullname = UsersData.Child("fullname").Value.ToString();
                    users.phone = UsersData.Child("phone").Value.ToString();
                    users.email = UsersData.Child("email").Value.ToString();
                    UsersList.Add(users);
                }
                UsersRetrieved.Invoke(this, new UsersDataEventArgs { Users = UsersList });
            }
        }

        public void Create()
        {
            DatabaseReference usersRef = AppDataHelper.GetDatabase().GetReference("users");
            usersRef.AddValueEventListener(this);
        }
    }
}