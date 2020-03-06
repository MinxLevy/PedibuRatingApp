using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using PediburRatingApp.Fragments;



namespace PediburRatingApp.Activity
{
    public class TopDriver : Fragment
    {
        ImageView addButton;
       // addDriverFragment addDriverFragment;

        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

           View view = inflater.Inflate(Resource.Layout.TopDriver, container, false);
            addButton = (ImageView)view.FindViewById(Resource.Id.addNewButton);
            return view;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {

        }
    }
}