using Firebase.Database;
using footballApp.Pages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News : ContentPage
    {
        static List<Newss> news = new List<Newss>();
        public static object newsObject;

        private readonly string ChildName = "News";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");

        public News()
        {
            InitializeComponent();
            AddSource();
        }

        public async void AddSource()
        {
            lstNews.ItemsSource = await GetAllNews();
        }

        private void lstNews_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (lstNews.SelectedItem != null)
            {
                newsObject = lstNews.SelectedItem;
                Navigation.PushAsync(new NewsAbout());
            }
            else
            {
                DisplayAlert("Error", "News is not found", "OK");
            }
        }

        public async Task<List<Newss>> GetAllNews()
        {
            return (await firebase.Child(ChildName).OnceAsync<Newss>()).Select(item => new Newss
            {
                NewsId = item.Object.NewsId,
                Naslov = item.Object.Naslov,
                Vsebina = item.Object.Vsebina,
                Url = item.Object.Url
            }).ToList();
        }
    }
}