
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using PediburRatingApp.EventListener;
using PediburRatingApp.Helpers;


namespace PediburRatingApp.Activity
{
    [Activity(Label = "Pedibur Rating App", Theme="@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity  
    {
        TextInputLayout emailText;
        TextInputLayout passwordText;
        Button loginButton;
        TextView signinButton;
        TextView forgotButton;
        CoordinatorLayout rootView;
        FirebaseAuth mAuth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login);
            emailText = (TextInputLayout)FindViewById(Resource.Id.emailText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordText);
            rootView = (CoordinatorLayout)FindViewById(Resource.Id.rootView);
            loginButton = (Button)FindViewById(Resource.Id.loginButton);
            signinButton = (TextView)FindViewById(Resource.Id.signinButton);
            forgotButton = (TextView)FindViewById(Resource.Id.forgotButton);
            mAuth = FirebaseAuth.GetInstance(AppDataHelper.GetAuth().App);
            loginButton.Click += LoginButton_Click;
            signinButton.Click += SigninButton_Click;
            forgotButton.Click += ForgotButton_Click;
        }

        private void ForgotButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ForgotPassword));
            
        }

        private void SigninButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
           
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email, password;

            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;

            if (!email.Contains("@"))
            {
                Snackbar.Make(rootView, "Please provide a valid email", Snackbar.LengthShort).Show();
                return;
            }
            if (password.Length < 6)
            {
                Snackbar.Make(rootView, "Please enter a password up to 6 characters", Snackbar.LengthShort).Show();
                return;
            }
            TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
            taskCompletionListener.Success += TaskCompletionListener_Success;
            taskCompletionListener.Failure += TaskCompletionListener_Failure;
            mAuth.SignInWithEmailAndPassword(email, password)
                .AddOnSuccessListener(taskCompletionListener)
                .AddOnFailureListener(taskCompletionListener);
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Snackbar.Make(rootView, "Login Failed", Snackbar.LengthShort).Show();
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(this, typeof(MainDashboard)));
            Finish();
        }


        }
}