using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        IAuth auth;
        public SignUp()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            bool created = await auth.SignUpWithEmailPassword(Email.Text, Password.Text);
            if (created)
            {
                await DisplayAlert("Success", "Registration successful!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Registration failed", "Error!", "OK");
            }
        }

    }
}