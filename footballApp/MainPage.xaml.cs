using Firebase.Database;
using footballApp.Model;
using footballApp.Pages;
using footballApp.Pages.News;
using footballApp.Pages.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace footballApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IAuth auth;

        static HttpClient client = new HttpClient();

        static List<Livescore> livescore = new List<Livescore>();
        public static object livescoreObject;

        public static string UserId = "";
        private readonly string ChildName = "FavouriteClubs";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");

        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            bv0.IsVisible = false;
            bv1.IsVisible = false;
            bv2.IsVisible = false;
            lblEngLogo.IsVisible = false;
            lblFraLogo.IsVisible = false;
            lblFavLogo.IsVisible = false;
            imgEng.IsVisible = false;
            imgFra.IsVisible = false;
            imgFav.IsVisible = false;
            UserId = auth.GetCurrentUserId();
            datePicker_DateSelected(null, new DateChangedEventArgs(DateTime.Now, DateTime.Now));
        }


        public async Task<List<Livescore>> GetLivescore()
        {
            try
            {
                string fromDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                string toDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                HttpResponseMessage response = await client.GetAsync($"https://apiv2.apifootball.com/?action=get_events&from={fromDate}&to={toDate}&APIkey=003ed92f1d95dc06e2a9c59e1b5ee5eced768b476349986e18331e74e7370c66");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    livescore = JsonConvert.DeserializeObject<List<Livescore>>(data);

                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        private async void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            lblSelectedDate.Text = datePicker.Date.ToString("dd-MM-yyyy");
            lblEngLogo.IsVisible = false;
            lblFraLogo.IsVisible = false;
            lblFavLogo.IsVisible = false;
            imgEng.IsVisible = false;
            imgFra.IsVisible = false;
            imgFav.IsVisible = false;
            listLivescoreFra.ItemsSource = null;
            listLivescoreEng.ItemsSource = null;
            listFavMatch.ItemsSource = null;
            bv0.IsVisible = false;
            bv1.IsVisible = false;
            bv2.IsVisible = false;
            bv3.IsVisible = false;
            bv4.IsVisible = false;
            bv5.IsVisible = false;
            await GetLivescore();
            List<Livescore> pridobiFra = livescore.Where(a => a.match_date == datePicker.Date.ToString("yyyy-MM-dd") && a.country_name == "France").ToList();
            List<Livescore> pridobiEng = livescore.Where(a => a.match_date == datePicker.Date.ToString("yyyy-MM-dd") && a.country_name == "England").ToList();
            List<Livescore> pridobiFav = new List<Livescore>();

            if (UserId != "")
            {
                List<Club> myClubs = (await firebase.Child(ChildName).OnceAsync<Club>()).Where(x => x.Object.UserUid == UserId).Select(item => new Club
                {
                    team_key = item.Object.team_key,
                    team_name = item.Object.team_name,
                    team_badge = item.Object.team_badge
                }).ToList();

                foreach (var item in myClubs)
                {
                    var trenutni = livescore.FirstOrDefault(a => a.match_date == datePicker.Date.ToString("yyyy-MM-dd") && (a.match_hometeam_name == item.team_name || a.match_awayteam_name == item.team_name));

                    if (pridobiFav.Contains(trenutni) == false && trenutni != null)
                        pridobiFav.Add(trenutni);
                }
            }

            if (pridobiFra.Count == 0 && pridobiEng.Count == 0 && pridobiFav.Count == 0)
            {
                lblIzpis.IsVisible = true;
                lblIzpis.Text = "There are no matches for the selected date";
            }
            else
            {
                if (pridobiEng.Count > 0)
                {
                    bv2.IsVisible = true;
                    bv3.IsVisible = true;
                    lblIzpis.IsVisible = false;
                    listLivescoreEng.ItemsSource = pridobiEng;
                    imgEng.IsVisible = true;
                    lblEngLogo.IsVisible = true;
                }
                if (pridobiFra.Count > 0)
                {
                    bv4.IsVisible = true;
                    bv5.IsVisible = true;
                    lblIzpis.IsVisible = false;
                    listLivescoreFra.ItemsSource = pridobiFra;
                    imgFra.IsVisible = true;
                    lblFraLogo.IsVisible = true;
                }
                if (pridobiFav.Count > 0)
                {
                    if (UserId != "")
                    {
                        listFavMatch.IsVisible = true;
                        bv0.IsVisible = true;
                        bv1.IsVisible = true;
                        lblIzpis.IsVisible = false;
                        listFavMatch.ItemsSource = pridobiFav;
                        imgFav.IsVisible = true;
                        lblFavLogo.IsVisible = true;                        
                    }
                }
            }
        }

        private void listLivescoreEng_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (listLivescoreEng.SelectedItem != null)
            {
                livescoreObject = listLivescoreEng.SelectedItem;
                Navigation.PushAsync(new MatchDetails());
            }
            else
            {
                DisplayAlert("Error", "Match details not available", "OK");
            }
        }

        private void listLivescoreFra_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (listLivescoreFra.SelectedItem != null)
            {
                livescoreObject = listLivescoreFra.SelectedItem;
                Navigation.PushAsync(new MatchDetails());
            }
            else
            {
                DisplayAlert("Error", "Match details not available", "OK");
            }
        }

        private async void btnMenu_Clicked(object sender, EventArgs e)
        {
            var selection = "";

            var token = UserId;

            if (token == "")
            {
                selection = await DisplayActionSheet("Menu", "Cancel", "", "Search", "News", "About", "Login", "SignUp");
            }
            else if (token == "iRfY4xW3YeMYQXF1btrNYzv5XfQ2" || token == "SAdQsOIxT0V37FjlQCM6mbiUr0G3" || token == "zPsgHWBEYLbElXnPQZvPTodmjH52")
            {
                selection = await DisplayActionSheet("Menu", "Cancel", "", "Search", "News", "Profile", "Logout", "About", "Add news");
            }
            else if (token != "")
            {
                selection = await DisplayActionSheet("Menu", "Cancel", "", "Search", "News", "Profile", "About", "Logout");
            }

            if (selection == "News")
            {
                await Navigation.PushAsync(new News());
            }
            else if (selection == "Login")
            {
                await Navigation.PushAsync(new Login());
            }
            else if (selection == "SignUp")
            {
                await Navigation.PushAsync(new SignUp());
            }
            else if (selection == "Profile")
            {
                await Navigation.PushAsync(new UserProfile());
            }
            else if (selection == "About")
            {
                await Navigation.PushAsync(new About());
            }
            else if (selection == "Logout")
            {
                auth.Logout();
                UserId = "";
            }
            else if (selection == "Add news")
            {
                await Navigation.PushAsync(new NewsAdd());
            }
            else if (selection == "Search")
            {
                await Navigation.PushAsync(new Search());
            }
            datePicker_DateSelected(null, new DateChangedEventArgs(DateTime.Now, DateTime.Now));
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            datePicker.Focus();
        }

        private void listFavMatch_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (listFavMatch.SelectedItem != null)
            {
                livescoreObject = listFavMatch.SelectedItem;
                Navigation.PushAsync(new MatchDetails());
            }
            else
            {
                DisplayAlert("Error", "Match details not available", "OK");
            }
        }
    }
}