using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.AccessControl;
using Newtonsoft.Json;

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
                string dataToSend = JsonConvert.SerializeObject(data);
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PostAsync(url, contentToSend);

                if (messageResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await messageResponse.Content.ReadAsStringAsync();
                    User responseDeserialize = JsonConvert.DeserializeObject<User>(jsonResponse);

                    userValid = responseDeserialize;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at Authentication class: " + ex);
            }

            return userValid;
        }
    }
}
