using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private static string apiVersion = "v1";

        public static async Task<int> EditUserProfile(User user)
        {
            int resultObtained = (int)StatusCode.ProccessError;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/login";
                string userToModify = JsonSerializer.Serialize(user);
                HttpContent contentToSend = new StringContent(userToModify, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PutAsync(url, contentToSend);

                if(messageResponse.IsSuccessStatusCode)
                {
                    resultObtained = (int)StatusCode.Ok;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at Autentication class:" + ex);
            }


            return resultObtained;
        }

    }
}
