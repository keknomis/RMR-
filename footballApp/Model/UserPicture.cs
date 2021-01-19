using System;
using System.Collections.Generic;
using System.Text;

namespace footballApp.Pages.Model
{
    public class UserPicture
    {
        public Guid Id { get; set; }
        public string UserUid { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
