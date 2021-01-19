using Firebase.Database;
using Firebase.Database.Query;
using footballApp.Model;
using footballApp.Pages.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        static HttpClient client = new HttpClient();

        private readonly string FavClubs = "FavouriteClubs";
        private readonly string FavLeagues = "FavouriteLeagues";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");

        string UserId = MainPage.UserId;

        public Search()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async Task<List<Club>> GetClub()
        {
            var query = searchBtn.Text;
            var listClubs = await GetClubs();
            List<List<Club>> clubs = new List<List<Club>>();

            foreach (var trenutniKlub in listClubs)
            {
                var trenutni = trenutniKlub.Where(p => p.team_name.ToLower().Contains(query.ToLower())).ToList();
                clubs.Add(trenutni);

            }

            List<Club> searchedClub = new List<Club>();
            foreach (var item in clubs)
            {
                foreach (var klub in item)
                {
                    searchedClub.Add(klub);
                }
            }
            return searchedClub;
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

        public async Task<List<League>> GetLeague()
        {
            var query = searchBtn.Text;
            var listLeagues = await GetLeagues();

            List<League> searchedLeague = listLeagues.Where(p => p.league_name.ToLower().Contains(query.ToLower())).ToList();

            return searchedLeague;
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
                await DisplayAlert("Error", e.Message, "OK");
            }

            return null;
        }

        public async Task<List<PlayerStats>> GetPlayers()
        {
            try
            {
                var playerName = searchBtn.Text;
                HttpResponseMessage response = await client.GetAsync($"https://apiv2.apifootball.com/?action=get_players&player_name={playerName}&APIkey=003ed92f1d95dc06e2a9c59e1b5ee5eced768b476349986e18331e74e7370c66");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var players = JsonConvert.DeserializeObject<List<PlayerStats>>(data);

                    return players;
                }

            }
            catch
            {
                await DisplayAlert("Error", "Player couldn't be found", "OK");
            }

            return null;
        }

        private async void searchBtn_SearchButtonPressed(object sender, EventArgs e)
        {
            if (picker.SelectedItem != null)
            {
                if (picker.SelectedItem.ToString() == "Clubs")
                {
                    lstClubs.IsVisible = true;
                    listPlayers.IsVisible = false;
                    listLeagues.IsVisible = false;

                    var club = await GetClub();
                    if (club.Count != 0)
                    {
                        lblNotFound.IsVisible = false;
                        lstClubs.ItemsSource = club;
                    }
                    else
                    {
                        lblNotFound.Text = "Club couldn't be found";
                    }
                }
                if (picker.SelectedItem.ToString() == "Players")
                {
                    listPlayers.IsVisible = true;
                    lstClubs.IsVisible = false;
                    listLeagues.IsVisible = false;
                    listPlayers.ItemsSource = await GetPlayers();
                }
                if (picker.SelectedItem.ToString() == "Leagues")
                {
                    listLeagues.IsVisible = true;
                    lstClubs.IsVisible = false;
                    listPlayers.IsVisible = false;

                    var league = await GetLeague();
                    if (league.Count != 0)
                    {
                        lblNotFound.IsVisible = false;
                        listLeagues.ItemsSource = league;
                    }
                    else
                    {
                        lblNotFound.Text = "League couldn't be found";
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Choose what do you want to search", "OK");
            }
        }

        private async void btnAddClub_Clicked(object sender, EventArgs e)
        {
            var UserId = MainPage.UserId;

            if (UserId != "")
            {
                var club = (Club)((Button)sender).CommandParameter;

                var exists = (await firebase.Child(FavClubs).OnceAsync<Club>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.team_key == club.team_key);

                if (exists == null)
                {
                    try
                    {
                        await firebase.Child(FavClubs).PostAsync(new Club()
                        {
                            team_key = club.team_key,
                            team_name = club.team_name,
                            team_badge = club.team_badge,
                            UserUid = UserId
                        });
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
            else
            {
                await DisplayAlert("Error", "You must log in to continue!", "OK");
            }
        }

        private async void btnAddLeague_Clicked(object sender, EventArgs e)
        {
            var UserId = MainPage.UserId;

            if (UserId != "")
            {
                var league = (League)((Button)sender).CommandParameter;

                var exsists = (await firebase.Child(FavLeagues).OnceAsync<League>()).FirstOrDefault(a => a.Object.UserUid == UserId && a.Object.league_id == league.league_id);
                if (exsists == null)
                {
                    try
                    {
                        await firebase.Child(FavLeagues).PostAsync(new League()
                        {
                            LeagaueId = Guid.NewGuid(),
                            league_id = league.league_id,
                            league_name = league.league_name,
                            league_logo = league.league_logo,
                            UserUid = UserId
                        });
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
            else
            {
                await DisplayAlert("Error", "You must log in to continue!", "OK");
            }
        }

        private void lstClubs_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var club = lstClubs.SelectedItem as Club;

            listClubPlayers.IsVisible = true;
            listClubPlayers.ItemsSource = club.players;
        }

        private async void listLeagues_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            List<Club> clubs = new List<Club>();
            var league = listLeagues.SelectedItem as League;

            try
            {
                var liga = league.league_id;
                HttpResponseMessage response = await client.GetAsync($"https://apiv2.apifootball.com/?action=get_teams&league_id={liga}&APIkey=003ed92f1d95dc06e2a9c59e1b5ee5eced768b476349986e18331e74e7370c66");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    clubs = JsonConvert.DeserializeObject<List<Club>>(data);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            lstClubs.IsVisible = true;
            lstClubs.ItemsSource = clubs;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstClubs.IsVisible = false;
            listClubPlayers.IsVisible = false;
            listLeagues.IsVisible = false;
            listPlayers.IsVisible = false;

            searchBtn.Text = "";

        }
    }
}