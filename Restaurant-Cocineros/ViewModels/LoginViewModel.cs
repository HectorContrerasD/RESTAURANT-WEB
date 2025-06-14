using GalaSoft.MvvmLight;
using Restaurant_Cocineros.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Cocineros.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        
        private string password = "";
        private string error = "";
        private readonly LoginService loginService;
        public LoginViewModel()
        {
            loginService = new LoginService();
        }

        private string username = "";
        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                OnPropertyChanged(nameof(Username));    
            }
        }

    }
}
