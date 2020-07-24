//TODO:
// Loading tutorial:
// https://www.lindseybroos.be/2020/03/xamarin-forms-and-firebase-authentication/
using Beachers.Services;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using System;

namespace Beachers.iOS
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public string UserID
        {
            get { return Auth.DefaultInstance.CurrentUser.Uid; }
        }

        public string UserFirstName
        {
            get { return Auth.DefaultInstance.CurrentUser.DisplayName; }
        }

        public bool IsSignIn()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return await user.User.GetIdTokenAsync();
        }

        public Task<string> RegisterWithEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public bool SignOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task UpateProfile(string displayName, string photoURI)
        {
            throw new NotImplementedException();
        }
    }
}