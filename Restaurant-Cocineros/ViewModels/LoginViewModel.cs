using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Restaurant_Cocineros.Models;
using Restaurant_Cocineros.Models.Validators;
using Restaurant_Cocineros.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;
using System.Windows;
using Restaurant_Cocineros.Properties;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;
using Restaurant_Cocineros.Views;

namespace Restaurant_Cocineros.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private string username = "";
        private string password = "";
        private string error = "";
        private readonly LoginService loginService;
        public LoginViewModel()
        {
            loginService = new LoginService();
        }

       
        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return password; }
            set 
            {
                password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Error
        {
            get { return error; }
            set 
            {
                error = value;
                OnPropertyChanged(nameof(Error));
            }
        }
        [RelayCommand]
        private async Task Confirmar()
        {
            Error = "";
            var dto = new LoginDTO
            {
                Username = username,
                Password = password
            };
            var result = LoginValidator.Validate(dto);
            if (!result.IsValid) Error = string.Join("\n", result.Errors.Select(x => x.ErrorMessage));
            else
            {
                var token = await loginService.Login(dto);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    token = token.Replace("\"", "");
                    Settings.Default.Token = token;
                    Settings.Default.Save();
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                    var rolClaim = jsonToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                    if (rolClaim?.Value == "Cocinero")
                    {
                        var pedidosView = new PedidosView();
                        pedidosView.Show();
                    }
                    var loginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x is LoginView);
                    loginWindow?.Close();
                }
                else
                {
                    Error = "Contraseña o Usuario incorrecto/a";
                }
            }

        }
    }
}
