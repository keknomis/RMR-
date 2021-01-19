using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using footballApp.Pages.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Profil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Leagues : ContentPage
    {
        static HttpClient client = new HttpClient();
        private readonly string ChildName = "FavouriteLeagues";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        string UserId = MainPage.UserId;

        public Leagues()
        {
            InitializeComponent();
            AddLeagues();
        }

        private async void lstLeagues_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var league = e.Item as League;

            var exsists = (await firebase.Child(ChildName).OnceAsync<League>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.league_id == league.league_id);

            if (exsists == null)
            {
                try
                {
                    await firebase.Child(ChildName).PostAsync(new League() { LeagaueId = Guid.NewGuid(), league_id = league.league_id, league_name = league.league_name, league_logo = league.league_logo, UserUid = UserId });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message.ToString(), "OK");
                }
                finally
                {
                    await DisplayAlert("Success", "The league was added successfully", "OK");
                }
            }
        }

        public async void AddLeagues()
        {
            var listLeagues = await GetLeagues();
            lstLeagues.ItemsSource = listLeagues;
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
    }
}