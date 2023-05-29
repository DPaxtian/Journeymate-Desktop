using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text.Json;
using Newtonsoft;

namespace Logic
{
    public class Autentication
    {
        private static string apiVersion = "v1";

        public static async Task<User> Login(string emailToLogin, string password)
        {
            User userValid = null;
            try
            {
                HttpClient server = new HttpClient();
                string passwordHashed = Encription.GetHashedPassword(password);
                string url = "http://localhost:9000/api/" + apiVersion + "/login";
                var data = new
                {
                    email = emailToLogin,
                    password = passwordHashed,
                };
                string dataToSend = JsonSerializer.Serialize(data);
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PostAsync(url, contentToSend);


                if (messageResponse.IsSuccessStatusCode)
                {
                    string jsonResponde = await messageResponse.Content.ReadAsStringAsync();
                    ApiResponseUser responseDescerialize = JsonSerializer.Deserialize<ApiResponseUser>(jsonResponde);
                    User userLogin = responseDescerialize.msg;

                    userValid = userLogin;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at Autentication class:" +
                    ex);
            }

            return userValid;
        }

    }
}
