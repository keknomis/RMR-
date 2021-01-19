using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using footballApp.Pages;
using footballApp.Pages.Model;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Clubs : ContentPage
    {
        static HttpClient client = new HttpClient();
        private readonly string ChildName = "FavouriteClubs";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        public Clubs()
        {
            InitializeComponent();
            AddClubs();
        }

        public async void AddClubs()
        {
            var listClubs = await GetClubs();
            List<Club> temp = new List<Club>();

            foreach (var item in listClubs)
            {
                foreach (var item2 in item)
                temp.Add(item2);
            }
            lstClubs.ItemsSource = temp;
        }

        public async Task<List<List<Club>>> GetClubs()
        {
            List<List<Club>> clubs = new List<List<Club>>();
            var vseLige = await GetLeagues();

            foreach (var item in vseLige)
            {
                try
                {
                    var liga = item.league_id;
                    HttpResponseMessage response = await client.GetAsync($"https://apiv2.apifootball.com/?action=get_teams&league_id={liga}&APIkey=003ed92f1d95dc06e2a9c59e1b5ee5eced768b476349986e18331e74e7370c66");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        var trenutni = JsonConvert.DeserializeObject<List<Club>>(data);
                        clubs.Add(trenutni);
                    }

                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }            

            return clubs;
        }

        public async Task<List<League>> GetLeagues()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://apiv2.apifootball.com/?action=get_leagues&APIkey=003ed92f1d95dc06e2a9c59e1b5ee5eced768b476349986e18331e74e7370c66");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var leagues = JsonConvert.DeserializeObject<List<League>>(data);

                    return leagues;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        private async void lstClubs_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var club = e.Item as Club;
            var UserId = MainPage.UserId;

            var exsists = (await firebase.Child(ChildName).OnceAsync<Club>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.team_key == club.team_key);

            if (exsists == null)
            {
                try
                {
                    await firebase.Child(ChildName).PostAsync(new Club() { team_key = club.team_key, team_name = club.team_name, team_badge = club.team_badge, UserUid = UserId });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message.ToString(), "OK");
                }
                finally
                {
                    await DisplayAlert("Success", "The club was added successfully", "OK");
                }
            }
        }
    }
}