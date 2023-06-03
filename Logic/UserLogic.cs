using Models;
using Newtonsoft.Json;
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

        public static async Task<int> SignUp(User userToSignUp)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/user/";
                var data = new
                {
                    name = userToSignUp.Name,
                    lastname = userToSignUp.Lastname,
                    username = userToSignUp.Username,
                    age = userToSignUp.Age,
                    email = userToSignUp.Email,
                    password = Encription.GetHashedPassword(userToSignUp.Password),
                };
                
                string dataToSend = JsonConvert.SerializeObject(data);
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PostAsync(url, contentToSend);

                codeStatus = (int)messageResponse.StatusCode;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at Authentication class: " + ex);
            }

            return codeStatus;
        }

    }
}
