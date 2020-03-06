using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using FR.Ganfra.Materialspinner;
using Java.Util;
using PediburRatingApp.Helpers;
using PediburRatingApp;
using SupportV7 = Android.Support.V7.App;

namespace PediburRatingApp.Fragments
{
    public class addDriverFragment : Fragment
    {
        TextInputLayout name;
        TextInputLayout location;
        TextInputLayout bodyNum;
        
        Button submitButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = inflater.Inflate(Resource.Layout.addDriver, container, false);

            name = (TextInputLayout)view.FindViewById(Resource.Id.addName);
            location = (TextInputLayout)view.FindViewById(Resource.Id.addLocation);
            bodyNum = (TextInputLayout)view.FindViewById(Resource.Id.addBodyNum);
           
            submitButton = (Button)view.FindViewById(Resource.Id.submitButton);

            submitButton.Click += SubmitButton_Click;
            

            return view;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string Name = name.EditText.Text;
            string Location = location.EditText.Text;
            string BodyNum = bodyNum.EditText.Text;

            HashMap driversInfo = new HashMap();
            driversInfo.Put("Name", Name);
            driversInfo.Put("Location", Location);
            driversInfo.Put("BodyNum", BodyNum);

            DatabaseReference newDriversRef = AppDataHelper.GetDatabase().GetReference("Drivers").Push();
            newDriversRef.SetValue(driversInfo);
                
            

        }


    }
}