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

namespace PediburRatingApp.DataModels
{
    public class UsersData
    {
        public string email { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string key { get; set; }

    }
}