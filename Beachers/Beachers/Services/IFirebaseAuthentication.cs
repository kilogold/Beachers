using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beachers.Services
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();

        string UserID { get; }
    }
}
