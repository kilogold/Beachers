using Beachers.Models;
using Beachers.Services;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beachers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            loginOutcome.Text = null;

            Task.Run(Do);
        }

        /// <summary>
        /// This logic goes in ModelView
        /// </summary>
        private async void Do()
        {
            //////////////////////AUTH//////////////////////////
            const string firebaseAPIkey = "AIzaSyA2JHpohpCz_412DdeaWqUaNKxaziRvV1g";
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIkey));

            FirebaseAuthLink auth = null;
            try
            {
                auth = await authProvider.SignInWithEmailAndPasswordAsync(email.Text, password.Text);

            }catch(FirebaseAuthException ex)
            {
                Console.WriteLine(ex.Reason);
                return;
            }


            DatabaseAccess db = new DatabaseAccess(auth.FirebaseToken);



            //////////////////////DBACCESS//////////////////////////

        }

        private async void KelvinsAuth()
        {
            DatabaseAccess db = new DatabaseAccess("");

            var hasher = MD5.Create();
            var md5Input = $"{email.Text}{password.Text}";
            var credentials = Encoding.ASCII.GetBytes(md5Input);
            var userHashBytes = hasher.ComputeHash(credentials);
            var userHashString = Encoding.ASCII.GetString(userHashBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < userHashBytes.Length; i++)
            {
                sb.Append(userHashBytes[i].ToString("X2"));
            }

            Models.User allPersons = await db.GetUser(sb.ToString().ToLower());

            MainThread.BeginInvokeOnMainThread(() =>
            {
                bool loggedIn = (allPersons != null);

                if (loggedIn)
                {
                    Navigation.PushAsync(new Booking());

                }
                else
                {
                    loginOutcome.Text = "Try again...";
                }
            });

        }
    }
}