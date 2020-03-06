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
using Firebase;
using Firebase.Auth;
using Firebase.Database;

namespace PediburRatingApp.Helpers
{
    public static class AppDataHelper
    {
        static ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        public static FirebaseDatabase GetDatabase()
        {

            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;
            FirebaseAuth auth;

            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("pediburratingapp")
                    .SetApiKey("AIzaSyC6ZBvONyo6vt7ZH1pzwG8i1oxsEnXLRLA")
                    .SetDatabaseUrl("https://pediburratingapp.firebaseio.com")
                    .SetStorageBucket("pediburratingapp.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
                auth = FirebaseAuth.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
                auth = FirebaseAuth.GetInstance(app);
            }

            return database;
        }

        public static FirebaseAuth GetAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth auth;

            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("pediburratingapp")
                    .SetApiKey("AIzaSyC6ZBvONyo6vt7ZH1pzwG8i1oxsEnXLRLA")
                    .SetDatabaseUrl("https://pediburratingapp.firebaseio.com")
                    .SetStorageBucket("pediburratingapp.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);

                auth = FirebaseAuth.GetInstance(app);
            }
            else
            {
                auth = FirebaseAuth.GetInstance(app);
            }

            return auth;
        }

        public static FirebaseUser GetCurrentUser()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);

            FirebaseAuth mAuth;
            FirebaseUser mUser;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()

                    .SetApplicationId("pediburratingapp")
                    .SetApiKey("AIzaSyC6ZBvONyo6vt7ZH1pzwG8i1oxsEnXLRLA")
                    .SetDatabaseUrl("https://pediburratingapp.firebaseio.com")
                    .SetStorageBucket("pediburratingapp.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                mAuth = FirebaseAuth.Instance;
                mUser = mAuth.CurrentUser;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
                mUser = mAuth.CurrentUser;
            }

            return mUser;
        }
        public static string GetFullName()
        {
            string fullname = preferences.GetString("fullname", "");
            return fullname;
        }

        public static string GetEmail()
        {
            string email = preferences.GetString("email", "");
            return email;
        }

        public static string GetPhone()
        {
            string phone = preferences.GetString("email", "");
            return phone;
        }
    }
}