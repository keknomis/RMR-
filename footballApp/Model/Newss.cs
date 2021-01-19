using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace footballApp.Pages.Model
{
    public class Newss
    {
        public Newss()
        {
        }

        public Guid NewsId { get; set; }
        public string Naslov { get; set; }
        public string Vsebina { get; set; }
        public string Url { get; set; }
    }
}
