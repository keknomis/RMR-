using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using footballApp.Pages.Model;
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
    public partial class MyLeagues : ContentPage
    {
        private readonly string ChildName = "FavouriteLeagues";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        string UserId = MainPage.UserId;

        public MyLeagues()
        {
            InitializeComponent();
            AddSource();
        }
        public async void AddSource()
        {
            lstMyLeagues.ItemsSource = await GetAllLeagues();
        }

        public async Task<List<League>> GetAllLeagues()
        {
            return (await firebase.Child(ChildName).OnceAsync<League>()).Where(x => x.Object.UserUid == UserId).Select(item => new League
            {
                league_id = item.Object.league_id,
                league_name = item.Object.league_name,
                league_logo = item.Object.league_logo
            }).ToList();
        }

        public async Task DeleteLeague(string leagueId)
        {
            try
            {
                var toDeletePerson = (await firebase.Child(ChildName).OnceAsync<League>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.league_id == leagueId);
                await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();

                AddSource();
            }
            catch (Exception e)
            {

                await DisplayAlert("Error", e.Message, "OK");
            }
        }

        private async void lstMyLeagues_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var test = lstMyLeagues.SelectedItem;
            await DisplayAlert("OK", test.ToString(), "OK");
        }

        private async void btnDeleteLeague_Clicked(object sender, EventArgs e)
        {
            var item = (League)((Button)sender).CommandParameter;

            var result = await DisplayAlert("Deleting..", "Do you want to remove " + item.league_name.ToUpper() + " from favourites?", "Yes", "No");

            if (result)
            {
                await DeleteLeague(item.league_id);
            }
        }
    }
}