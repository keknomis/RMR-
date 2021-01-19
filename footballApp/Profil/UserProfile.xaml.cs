using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using footballApp.Pages.Model;
using footballApp.Pages.Profil;
using footballApp.Profil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        static HttpClient client = new HttpClient();
        private readonly string ChildName = "ProfilePicture";
        private readonly string ChildNameClubs = "FavouriteClubs";
        private readonly string ChildNameLeagues = "FavouriteLeagues";
        FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        IAuth auth;
        string email = "";
        string UserId = MainPage.UserId;

        public UserProfile()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            email = auth.GetCurrentUser();
            PopulateEmailField();
            ProfilePicture();
        }

        public async void ProfilePicture()
        {
            var ProfilePicture = await GetProfilePicture();

            if (ProfilePicture.Count != 0)
            {
                foreach (var item in ProfilePicture)
                {
                    profilePicture.Source = item.ProfilePictureUrl;
                }                
            }
            else
            {
                await firebase.Child(ChildName).PostAsync(new UserPicture() { Id = Guid.NewGuid(), UserUid = UserId, ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Football_%28soccer_ball%29.svg/1200px-Football_%28soccer_ball%29.svg.png" });
                profilePicture.Source = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Football_%28soccer_ball%29.svg/1200px-Football_%28soccer_ball%29.svg.png";
            }
        }

        public async Task<List<UserPicture>> GetProfilePicture()
        {
            return (await firebase.Child(ChildName).OnceAsync<UserPicture>()).Where(x => x.Object.UserUid == UserId).Select(item => new UserPicture
            {
                UserUid = item.Object.UserUid,
                ProfilePictureUrl = item.Object.ProfilePictureUrl
            }).ToList();
        }

        public void PopulateEmailField()
        {
            email = auth.GetCurrentUser();
            userEmail.Text = email;
        }

        private async void btnMyClubs_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyClubs());
        }

        private async void btnDeleteAccount_Clicked(object sender, EventArgs e)
        {

            var result = await DisplayAlert("Deleting..", "Do you want to delete your account?", "Yes", "No");

            if (result)
            {
                await DeleteUserClubs();
                await DeleteUserLeagues();
                await DeleteUserPicture();
                auth.DeleteAccount();
            }
        }

        private async Task DeleteUserClubs()
        {
            var toDeleteClub = (await firebase.Child(ChildNameClubs).OnceAsync<Club>()).Where(a => a.Object.UserUid == UserId);

            foreach (var item in toDeleteClub)
            {
                await firebase.Child(ChildNameClubs).Child(item.Key).DeleteAsync();
            }
        }

        private async Task DeleteUserLeagues()
        {
            var toDeleteLeague = (await firebase.Child(ChildNameLeagues).OnceAsync<League>()).Where(a => a.Object.UserUid == UserId);

            foreach (var item in toDeleteLeague)
            {
                await firebase.Child(ChildNameLeagues).Child(item.Key).DeleteAsync();
            }
        }

        private async Task DeleteUserPicture()
        {
            var toDeleteUserPicture = (await firebase.Child(ChildName).OnceAsync<League>()).Where(a => a.Object.UserUid == UserId);

            foreach (var item in toDeleteUserPicture)
            {
                await firebase.Child(ChildName).Child(item.Key).DeleteAsync();
            }
        }

        private async void btnAddClubs_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clubs());
        }

        private async void btnAddLeague_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Leagues());
        }

        private async void btnMyLeagues_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyLeagues());
        }

        private async void btnEditProfile_Clicked(object sender, EventArgs e)
        {
            var selection = await DisplayActionSheet("Select option", "Cancel", "", "Change profile picture", "Change email", "Change password");

            if (selection == "Change profile picture")
            {
                await Navigation.PushAsync(new ChangePicture());
            }
            else if (selection == "Change email")
            {
                await Navigation.PushAsync(new ChangeEmail());
            }
            else if (selection == "Change password")
            {
                await Navigation.PushAsync(new ChangePassword());
            }

            PopulateEmailField();
        }
    }
}
