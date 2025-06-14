using Restaurant_Cocineros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

namespace Restaurant_Cocineros.Services
{
    public class LoginService
    {
        private readonly HttpClient httpClient;
        public LoginService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://restaurant-api.websitos256.com/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string> Login(LoginDTO loginDTO)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/login", jsonContent);
            try
            {
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return result;
                else return string.Empty;
            }
            catch (Exception e)
            {

                MessageBox.Show($"{e.Message}", "Errors");
                return string.Empty;
            }
        }
    }
}
