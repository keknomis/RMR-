using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace footballApp
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<bool> SignUpWithEmailPassword(string email, string password);
        string GetCurrentUser();
        void Logout();
        void DeleteAccount();
        Task<string> UpdateEmail(string email);
        Task<string> UpdatePassword(string password);
        string GetCurrentUserId();
    }
}
