using footballApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace footballApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchDetails : ContentPage
    {
        public object livescore { get; set; } = MainPage.livescoreObject;

        public MatchDetails()
        {
            InitializeComponent();
            BindingContext = this;
            GetDetails();
        }

        public void GetDetails()
        {
            livescore = MainPage.livescoreObject;
            var temp = (Livescore)livescore;

            listHomeScorers.ItemsSource = temp.HomeGoalScorers;
            listAwayScorers.ItemsSource = temp.AwayGoalScorers;

            listHomeCards.ItemsSource = temp.HomeCard;
            listAwayCards.ItemsSource = temp.AwayCard;
        }
    }
}