using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beachers.Services
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> RegisterWithEmailAndPassword(string email, string password);
        Task UpateProfile(string displayName, string photoURI);
        void ResetPassword(string email);

        bool SignOut();
        bool IsSignIn();

        string UserID { get; }
        string UserFirstName { get; }
    }
}
