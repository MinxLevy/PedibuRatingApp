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
using Android.Support.V7.App;
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using PediburRatingApp;
using AlertDialog = Android.App.AlertDialog;
using PediburRatingApp.Helpers;
using PediburRatingApp.Activity;

namespace PediburRatingApp.Activity
{
    [Activity(Label = "ForgotPassword", Theme = "@style/AppTheme.NoActionBar")]
    public class ForgotPassword : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private TextInputLayout input_email;
        private Button btnResetPass;
        private TextView btnBack;
        


        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ForgotPassword);

            //Init Firebase
            auth = FirebaseAuth.GetInstance(AppDataHelper.GetDatabase().App);

            //View
            input_email = (TextInputLayout)FindViewById(Resource.Id.forgot_email);
            btnResetPass = FindViewById<Button>(Resource.Id.forgot_btn_reset);
            btnBack = FindViewById<TextView>(Resource.Id.forgot_btn_back);
            

            btnResetPass.SetOnClickListener(this);
            btnBack.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.forgot_btn_back)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.forgot_btn_reset)
            {
                ResetPassword(input_email.EditText.Text);
            }
        }

        private void ResetPassword(string email)
        {
            auth.SendPasswordResetEmail(email)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == false)
            {

                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Failed");
                alert.SetMessage("Request failed, something went wrong");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.SetButton2("CANCEL", (c, ev) => { });
                alert.Show();

            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Success");
                alert.SetMessage("Request success, please check your email");
                alert.SetButton("OK", (c, ev) =>
                {
                    // Ok button click task  
                });
                alert.Show();
            }
        }
    }
}