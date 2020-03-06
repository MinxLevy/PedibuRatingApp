using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using PediburRatingApp.DataModels;
using PediburRatingApp.EventListener;
using PediburRatingApp.Fragments;
using PediburRatingApp.Helpers;

namespace PediburRatingApp.Activity
{
    [Activity(Label = "", Theme = "@style/AppTheme.NoActionBar")]
    public class MainDashboard : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        FirebaseAuth auth;
        TextView nameNav;
        TextView emailNav;
        ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        ISharedPreferencesEditor editor;
        List<UsersData> UsersList;
        UsersDataListener userDataListener = new UsersDataListener();



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.MainDashboard);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            //Init Firebase
            auth = FirebaseAuth.GetInstance(AppDataHelper.GetAuth().App);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();


            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            View headerView = navigationView.GetHeaderView(0);
            nameNav = (TextView)headerView.FindViewById(Resource.Id.NameNav);
            emailNav = (TextView)headerView.FindViewById(Resource.Id.emailNav);
            RetrievedUser();


            navigationView.SetNavigationItemSelectedListener(this);


        }
        public void RetrievedUser()
        {
            userDataListener = new UsersDataListener();
            userDataListener.Create();
            userDataListener.UsersRetrieved += UserDataListener_UsersRetrieved;

        }


           


        private void UserDataListener_UsersRetrieved(object sender, UsersDataListener.UsersDataEventArgs e)
        {
            UsersList = e.Users;
            var username = UsersList.Where(user => user.email.Contains(auth.CurrentUser.Email)).FirstOrD­efault().fullname;
            if (auth.CurrentUser != null)
            {
                /*                    List<UsersData> SearchResult =
                                                (from users in UsersList
                                                 where users.key.Contains(auth.CurrentUser.Uid)
                                                 select users).ToList();*/
                string fullname = username;
                nameNav.Text = fullname;
                emailNav.Text = auth.CurrentUser.Email;
            }
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        Fragment fragment;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();


            if (id == Resource.Id.navRating)
            {
                fragment = new Rating();
                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
                transaction.Replace(Resource.Id.FramePage, fragment);
                transaction.Commit();
            }
            else if (id == Resource.Id.navTopDriver)
            {
                fragment = new addDriverFragment();
                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
                transaction.Replace(Resource.Id.FramePage, fragment);
                transaction.Commit();
            }
            else if (id == Resource.Id.navBalance)
            {
                fragment = new Balance();
                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                drawer.CloseDrawer(GravityCompat.Start);
                transaction.Replace(Resource.Id.FramePage, fragment);
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_send)
            {
                auth.SignOut();
                editor = preferences.Edit();
                editor.Clear().Commit();

                if (auth.CurrentUser == null)
                {


                    StartActivity(new Intent(this, typeof(LoginActivity)));
                    Finish();
                }
            }


            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

