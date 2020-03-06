using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using PediburRatingApp;
using PediburRatingApp.Activity;
using PediburRatingApp.Adapter;
using PediburRatingApp.DataModels;
using PediburRatingApp.EventListener;
using PediburRatingApp.Fragments;
using System.Drawing;
using Java.Util;
using PediburRatingApp.Helpers;
using Firebase.Database;
using Firebase.Auth;
using Android.Content;
using Newtonsoft.Json;

namespace PediburRatingApp.Fragments
{

    public class DriversListFrag : Fragment
    {

        View view;
        RecyclerView myRecyclerView;
        List<Drivers> DriversList;
        DriversAdapter adapter;
        DriversListener driversListener;
        String inputText;
        RatingBar rating;
        TextInputLayout feedBack;
        FirebaseAuth mAuth;
        Button subButton;
        LinearLayout rootsview;
        public string driverID;
        public override void OnCreate(Bundle savedInstanceState)
        {

        
            base.OnCreate(savedInstanceState);


        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            view = inflater.Inflate(Resource.Layout.DriversList, container, false);
            inputText = Arguments.GetString("search_text");
            rootsview = (LinearLayout)view.FindViewById(Resource.Id.rootsview);
            feedBack = (TextInputLayout)view.FindViewById(Resource.Id.Feedback);
            rating = (RatingBar)view.FindViewById(Resource.Id.ratingbar);
            subButton = (Button)view.FindViewById(Resource.Id.subButton);
            subButton.Click += SubButton_Click;
            myRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.myRecyclerView);
            mAuth = AppDataHelper.GetAuth();
            RetrievedData();

            //CreateData();
            //SetUpRecyClerView();
            return view;
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            String feedbackRecover = feedBack.EditText.Text;
            String ratingScore = rating.Rating.ToString();


            HashMap ratinginfo = new HashMap();
            
            //KUHA DATA GIKAN LIST
            var uid = DriversList.Where(driver => driver.BodyNum.Contains(inputText)).FirstOrD­efault().ID;

            ratinginfo.Put("UserEmail", mAuth.CurrentUser.Email);
            ratinginfo.Put("Rating", ratingScore);
            ratinginfo.Put("FeedBack", feedbackRecover);
            
            DatabaseReference newDriversRef = AppDataHelper.GetDatabase().GetReference("Drivers/"+ uid +"/"+"rating/"+mAuth.CurrentUser.Uid);
            
            newDriversRef.SetValue(ratinginfo);
            Snackbar.Make(rootsview, "Successfully Added", Snackbar.LengthShort).Show();
            return;

        }

        private void SetUpRecyClerView()
        {

            List<Drivers> SearchResult =
            (from drivers in DriversList
             where drivers.BodyNum.Contains(inputText)
             select drivers).ToList();


            myRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(myRecyclerView.Context));
            
            
            adapter = new DriversAdapter(SearchResult);
            
            myRecyclerView.SetAdapter(adapter);
        }



        /* private void Adapter_SubmitRateClick(object sender, DriversAdapterClickEventArgs e)
         {
             String feedbackRecover = feedBack.EditText.Text;
             String ratingScore = rating.Rating.ToString();

             HashMap ratinginfo = new HashMap();

             //ratinginfo.Put("Name", Name);
             ratinginfo.Put("Rating", ratingScore);
             ratinginfo.Put("FeedBack", feedbackRecover);

             DatabaseReference newDriversRef = AppDataHelper.GetDatabase().GetReference("rating/" + mAuth.CurrentUser.Uid);
             newDriversRef.SetValue(ratinginfo);
         }*/
        public void CreateData()
        {
            DriversList = new List<Drivers>();
            DriversList.Add(new Drivers { Name = "Stupid", Location = "Tagum City", BodyNum = "1056" });
            DriversList.Add(new Drivers { Name = "Madafaka", Location = "Tagum City", BodyNum = "1056", });

        }
        public void RetrievedData()
        {
            driversListener = new DriversListener();
            driversListener.Create();
            driversListener.DriversRetrieved += DriversListener_DriversRetrieved;
        }

        private void DriversListener_DriversRetrieved(object sender, DriversListener.DriversDataEventArgs e)
        {


            DriversList = e.Drivers;

            SetUpRecyClerView();
        }
    }
}