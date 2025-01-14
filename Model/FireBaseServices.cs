using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace odev.Model
{
    public class FireBaseServices
    {
        private readonly FirebaseClient firebaseClient;

        public FireBaseServices()
        {
            firebaseClient = new FirebaseClient("https://gorsel3-ac5bc-default-rtdb.firebaseio.com/");
        }

        public async Task<User?> AddUser(User user)
             {
         try
                {
        await firebaseClient
        .Child("Users")
        .PostAsync(user);
          return user;
                }
          catch (Exception ex)
               {
             Console.WriteLine(ex.Message);
              return null;
                 }
             }

          public async Task<User?> GetUserByEmailAndPassword(string email, string password)
                 {
                   try
                        {
              var allUsers = await firebaseClient
              .Child("Users")
               .OnceAsync<User>();
                return allUsers.FirstOrDefault(u => u.Object.Email == email && u.Object.Password == password)?.Object;
                      }
               catch (Exception ex)
                  {
                Console.WriteLine(ex.Message);
                return null;
                }
              }
           }

    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
