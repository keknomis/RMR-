using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class NewsEdit : ContentPage
    {
        public object news { get; set; } = News.newsObject;
        private readonly string ChildName = "News";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");

        public NewsEdit()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void btnPosodobi_Clicked(object sender, EventArgs e)
        {
            var getId = (Newss)news;
            string naslov1 = naslov.Text;
            string vsebina1 = vsebina.Text;
            string url1 = url.Text;

            if (naslov1 != "" && vsebina1 != "" && url1 != "")
            {
                await UpdateNews(getId.NewsId, naslov1, vsebina1, url1);
            }
            else
            {
                await DisplayAlert("Manjkajoči podatki", "Prosim izpolnite vse vnosna polja!", "cancel");
            }
        }

        public async Task UpdateNews(Guid NewsId, string naslov1, string vsebina1, string url1)
        {
            try
            {
                var toUpdateNews = (await firebase.Child(ChildName).OnceAsync<Newss>()).FirstOrDefault(a => a.Object.NewsId == NewsId);

                await firebase.Child(ChildName).Child(toUpdateNews.Key).PutAsync(new Newss() { NewsId = NewsId, Naslov = naslov1, Vsebina = vsebina1, Url = url1 });
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message.ToString(), "cancel");
            }
            finally
            {
                await DisplayAlert("Success", "The news was edited successfully", "OK");

                await Navigation.PopAsync();
            }

        }

    }
}