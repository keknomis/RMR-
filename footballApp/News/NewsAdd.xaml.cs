using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using footballApp.Pages.Model;
using Firebase.Database.Query;

namespace footballApp.Pages.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsAdd : ContentPage
    {
        private readonly string ChildName = "News";
        readonly FirebaseClient firebase = new FirebaseClient("https://footballapp-b8a50.firebaseio.com/");

        public NewsAdd()
        {
            InitializeComponent();
        }
        private async void btnDodaj_Clicked(object sender, EventArgs e)
        {
            string naslov1 = naslov.Text;
            string vsebina1 = vsebina.Text;
            string url1 = url.Text;
            if (naslov1 != "" && vsebina1 != "" && url1 != "")
            {
                try
                {
                    await firebase.Child(ChildName).PostAsync(new Newss() { NewsId = Guid.NewGuid(), Naslov = naslov1, Vsebina = vsebina1, Url = url1 });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message.ToString(), "OK");
                }
                finally
                {
                    await DisplayAlert("Success", "The news was added successfully", "OK");

                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Manjkajoči podatki", "Prosim izpolnite vse vnosna polja!", "cancel");
            }
        }
    }
}