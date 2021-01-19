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
    public partial class ChangePassword : ContentPage
    {
        IAuth auth;
        public ChangePassword()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                await auth.UpdatePassword(password.Text);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                await DisplayAlert("Success", "Password was changed successfully", "Ok");
                await Navigation.PopAsync();
            }
        }
    }
}