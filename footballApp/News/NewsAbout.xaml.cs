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
    public partial class NewsAbout : ContentPage
    {
        public object news { get; set; } = News.newsObject;
        private readonly string ChildName = "News";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");
        public NewsAbout()
        {
            InitializeComponent();
            BindingContext = this;

            btnEdit.IsVisible = false;
            btnDelete.IsVisible = false;
            DisplayButtons();
        }

        public void GetDetails()
        {
            news = News.newsObject;
        }

        public void DisplayButtons()
        {
            var token = MainPage.UserId;

            if (token == "iRfY4xW3YeMYQXF1btrNYzv5XfQ2" || token == "SAdQsOIxT0V37FjlQCM6mbiUr0G3" || token == "zPsgHWBEYLbElXnPQZvPTodmjH52")
            {
                btnEdit.IsVisible = true;
                btnDelete.IsVisible = true;
            }
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewsEdit());
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var getId = (Newss)news;

            var result = await DisplayAlert("Deleting..", "Do you want to remove the news?", "Yes", "No");

            if (result)
            {
                await DeleteNews(getId.NewsId);
                await Navigation.PopAsync();
            }

        }

        public async Task DeleteNews(Guid NewsId)
        {
            try
            {
                var deleteNews = (await firebase.Child(ChildName).OnceAsync<Newss>()).FirstOrDefault(a => a.Object.NewsId == NewsId);

                await firebase.Child(ChildName).Child(deleteNews.Key).DeleteAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message.ToString(), "OK");
            }
            finally
            {
                await DisplayAlert("Success", "The news was deleted successfully", "OK");

                await Navigation.PopAsync();
            }
        }
    }
}