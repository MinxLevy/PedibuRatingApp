﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Java.Util;
using PediburRatingApp.EventListener;
using PediburRatingApp.Helpers;

namespace PediburRatingApp.Activity
{
    [Activity(Label = "Register Activity", Theme ="@style/AppTheme.NoActionBar", MainLauncher = false)]

    public class RegisterActivity : AppCompatActivity
    {
        TextInputLayout fullnameText;
        TextInputLayout phoneText;
        TextInputLayout emailText;
        TextInputLayout passwordText;
        TextView returnButton;
        Button registerButton;
        CoordinatorLayout rootView;

        FirebaseAuth mAuth;
        FirebaseDatabase database;
        TaskCompletionListener TaskCompletionListener = new TaskCompletionListener();
        string fullname, phone, email, password;
        ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        ISharedPreferencesEditor editor;
        protected override void OnCreate(Bundle savedInstanceState)
        {   
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Registration);
            mAuth = FirebaseAuth.GetInstance(AppDataHelper.GetAuth().App);
            FirebaseUser user = mAuth.CurrentUser;
            UserProfileChangeRequest profUpdate = new UserProfileChangeRequest.Builder().SetDisplayName(fullname).Build();
            ConnectControl();
        }
        void ConnectControl()
        {

            fullnameText = (TextInputLayout)FindViewById(Resource.Id.fullNameText);
            phoneText = (TextInputLayout)FindViewById(Resource.Id.phoneText);
            emailText = (TextInputLayout)FindViewById(Resource.Id.emailText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordText);
            rootView = (CoordinatorLayout)FindViewById(Resource.Id.rootView);
            registerButton = (Button)FindViewById(Resource.Id.registerButton);
            returnButton = (TextView)FindViewById(Resource.Id.returnButton);

            registerButton.Click += RegisterButton_Click;
            returnButton.Click += ReturnButton_Click;
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(LoginActivity)));
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            
            fullname = fullnameText.EditText.Text;
            phone = phoneText.EditText.Text;
            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;

            if(fullname.Length < 3)
            {
                Snackbar.Make(rootView, "Please enter a valid name", Snackbar.LengthShort).Show();
                return;
            }
            else if(phone.Length < 9)
            {
                Snackbar.Make(rootView, "Please enter a valid phone number", Snackbar.LengthShort).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Snackbar.Make(rootView, "Please enter a valid email", Snackbar.LengthShort).Show();
                return;
            }
            else if(password.Length < 6)
            {
                Snackbar.Make(rootView, "Please enter a password up to 6 characters", Snackbar.LengthShort).Show();
                return;
            }
            RegisterUser(fullname, phone, email, password);
        }
        void RegisterUser(string name, string phone, string email, string password)
        {

            TaskCompletionListener.Success += TaskCompletionListener_Success;
            TaskCompletionListener.Failure += TaskCompletionListener_Failure;
            mAuth.CreateUserWithEmailAndPassword(email, password)
                .AddOnSuccessListener(this, TaskCompletionListener)
                .AddOnFailureListener(this, TaskCompletionListener);
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Snackbar.Make(rootView, "User Registration failed", Snackbar.LengthShort).Show();
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {

            

            Snackbar.Make(rootView, "User Registration was Successful", Snackbar.LengthShort).Show();
            HashMap userMap = new HashMap();
            userMap.Put("email", email);
            userMap.Put("phone", phone);
            userMap.Put("fullname", fullname);
            

            DatabaseReference userReference = AppDataHelper.GetDatabase().GetReference("users/" + mAuth.CurrentUser.Uid);
            userReference.SetValue(userMap);
            
        }
        void SaveToSharedPreference()
        {
            editor = preferences.Edit();

            editor.PutString("email", email);
            editor.PutString("fullname", fullname);
            editor.PutString("phone", phone);

            editor.Apply();

        }
    }
}