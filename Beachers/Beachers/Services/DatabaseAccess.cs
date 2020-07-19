using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beachers.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace Beachers.Services
{
    public class DatabaseAccess
    {
        private FirebaseClient firebase;
        
        public DatabaseAccess(string firebaseToken)
        {
            firebase = new FirebaseClient("https://beachers-49bec.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(firebaseToken)
            });
        }

        public async Task<User> GetUser(string userHash)
        {
            var wat = await firebase
              .Child($"Users/{userHash}")
              .OnceSingleAsync<User>();

            return wat;
        }
    }
}
