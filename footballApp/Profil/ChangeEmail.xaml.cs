using Firebase.Database;
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
    public partial class ChangeEmail : ContentPage
    {
        IAuth auth;

        public ChangeEmail()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                await auth.UpdateEmail(email.Text);
            } 
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                await DisplayAlert("Success", "Email was changed successfully", "Ok");
                await Navigation.PopAsync();
            }
        }
    }
}