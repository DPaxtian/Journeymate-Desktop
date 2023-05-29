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

namespace Logic
{
    public class Autentication
    {
        private static string apiVersion = "v1";

        public static async Task<int> Login(string emailToLogin, string password)
        {
            int codeStatus = (int)StatusCode.Unautorized;
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
                    codeStatus = (int)messageResponse.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at Autentication class:" +
                    ex);
            }

            return codeStatus;
        }

    }
}
