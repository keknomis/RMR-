using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages.Profil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyClubs : ContentPage
    {
        private readonly string ChildName = "FavouriteClubs";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        string UserId = MainPage.UserId;
        public MyClubs()
        {
            InitializeComponent();
            AddSource();
        }

        public async void AddSource()
        {
            lstMyClubs.ItemsSource = await GetAllClubs();
        }

        public async Task<List<Club>> GetAllClubs()
        {
            return (await firebase.Child(ChildName).OnceAsync<Club>()).Where(x => x.Object.UserUid == UserId).Select(item => new Club
            {
                team_key = item.Object.team_key,
                team_name = item.Object.team_name,
                team_badge = item.Object.team_badge
            }).ToList();
        }

        private async void btnDeleteClub_Clicked(object sender, EventArgs e)
        {
            var item = (Club)((Button)sender).CommandParameter;

            var result = await DisplayAlert("Deleting..", "Do you want to remove " + item.team_name.ToUpper() + " from favourites?", "Yes", "No");

            if (result)
            {
                await DeleteClub(item.team_key);
            }
        }

        public async Task DeleteClub(string teamKey)
        {
            try
            {
                var toDeleteClub = (await firebase.Child(ChildName).OnceAsync<Club>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.team_key == teamKey);
                await firebase.Child(ChildName).Child(toDeleteClub.Key).DeleteAsync();

                AddSource();
            }
            catch (Exception e)
            {

                await DisplayAlert("Error", e.Message, "OK");
            }
        }
    }
}