using Beachers.Services;
using Firebase.Auth;
using System;
using System.Threading.Tasks;

namespace Beachers.Droid
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public string UserID
        {
            get { return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid; }
        }

        public string UserFirstName
        {
            get { return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.DisplayName; }
        }

        public bool IsSignIn()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }
        public async Task<string> RegisterWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task UpateProfile(string displayName, string photoURI)
        {
            UserProfileChangeRequest.Builder b = new UserProfileChangeRequest.Builder();
            if (!string.IsNullOrEmpty(displayName)) { b.SetDisplayName(displayName); }
            if (!string.IsNullOrEmpty(photoURI)) 
            {
                //TODO
            }

            try
            {
                await FirebaseAuth.Instance.CurrentUser.UpdateProfileAsync(b.Build());
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
            }
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}