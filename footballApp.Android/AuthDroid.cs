using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Xamarin.Forms;
using footballApp.Droid;

[assembly:Dependency(typeof(AuthDroid))]
namespace footballApp.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthException)
            {
                return "";
            }
        }
        public async Task<bool> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                var signUpTask = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return signUpTask != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Logout()
        {
            FirebaseAuth.Instance.SignOut();
        }

        public string GetCurrentUser()
        {
            try
            {   
                string user = FirebaseAuth.Instance.CurrentUser.Email;
                return user;
            }
            catch(Exception e)
            {
                return "User not found" + e.Message;
            }
        }
        public string GetCurrentUserId()
        {
            try
            {
                string user = FirebaseAuth.Instance.CurrentUser.Uid;
                return user;
            }
            catch (Exception e)
            {
                return "User not found" + e.Message;
            }
        }

        public async void DeleteAccount()
        {
            await FirebaseAuth.Instance.CurrentUser.DeleteAsync();
        }

        public async Task<string> UpdateEmail(string email)
        {           
            try
            {
                await FirebaseAuth.Instance.CurrentUser.UpdateEmailAsync(email);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public async Task<string> UpdatePassword(string password)
        {
            try
            {
                await FirebaseAuth.Instance.CurrentUser.UpdatePasswordAsync(password);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
    }
}
