using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages
{
    [DesignTimeVisible(true)]
    public partial class Login : ContentPage
    {
        IAuth auth;        
        public Login()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            string Token = await auth.LoginWithEmailPassword(Email.Text, Password.Text);
            if (Token != "")
            {
                MainPage.UserId = auth.GetCurrentUserId();
                await Navigation.PopAsync();
            }
            else
            {
                ShowError();
            }
        }
        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }
        async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }
    }
}