using System.Threading.Tasks;
using Notes.Data;
using Xamarin.Android;
using Firebase.Auth;
using Xamarin.Forms;
using Notes.Droid;

[assembly: Dependency(typeof(FirebaseAuthenticator))]
namespace Notes.Droid
{
    public class FirebaseAuthenticator : ThisFirebase
    {
        public async Task<string> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

            var getTokenResult = await authResult.User.GetIdTokenAsync(false);
            return getTokenResult.Token;
        }
        public async Task<string> CreateUserWithEmailAndPasswordAsync(string email, string password)
        {
            var authResult = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

            var getTokenResult = await authResult.User.GetIdTokenAsync(false);
            return getTokenResult.Token;
        }

    }
}