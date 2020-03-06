using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using PediburRatingApp;
using PediburRatingApp.Adapter;
using PediburRatingApp.DataModels;
using PediburRatingApp.Fragments;
using FragmentManager = Android.Support.V4.App.FragmentManager;
namespace PediburRatingApp.Activity
{
    
    public class Rating : Fragment
    {
        Button submit_button;
        EditText searchText;
       // public String inputText { get; set; }


        View view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);


            view = inflater.Inflate(Resource.Layout.rating, container, false);
            submit_button = (Button)view.FindViewById(Resource.Id.submit_rating);
            searchText = (EditText)view.FindViewById(Resource.Id.searchText);


            submit_button.Click += SubmitButton_Click;

            return view;
        }
        public Rating NewInstance(String SearchText)
        {
            Bundle args = new Bundle();
           
            args.PutString(searchText.Text, SearchText);
            DriversListFrag frag = new DriversListFrag();
            frag.Arguments = Arguments;
            return new Rating { Arguments = args };
            
        }
       
       
        
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(searchText.Text))
            {
                Snackbar snackBar = Snackbar.Make(view, "Please Enter A Value ", Snackbar.LengthShort);
                snackBar.Show();
            }
            else
            {
               // Rating rate = new Rating();
               // rate.inputText = searchText.Text;
                
                FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
                
                Fragment fragment = new DriversListFrag();
                Bundle args = new Bundle();
                args.PutString("search_text", searchText.Text);
                
                fragment.Arguments = args;
                transaction.Replace(Resource.Id.FramePage, fragment);
                transaction.Commit();
                
            }
            

        }
        



    }
}