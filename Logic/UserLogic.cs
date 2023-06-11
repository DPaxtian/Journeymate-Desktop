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
using System.Web;

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


        public static async Task<int> EditProfile(User userToEdit)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/user/";

                var userData = new
                {
                    username = userToEdit.Username,
                    name = userToEdit.Name,
                    lastname = userToEdit.Lastname,
                    age = userToEdit.Age,
                    email = userToEdit.Email,
                    phone_number = userToEdit.PhoneNumber,
                    country = userToEdit.Country,
                    city = userToEdit.City,
                    user_description = userToEdit.UserDescription,
                };

                string profile_data = JsonConvert.SerializeObject(userData);
                HttpContent contentToSend = new StringContent(profile_data, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PatchAsync(url, contentToSend);

                codeStatus = (int)messageResponse.StatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error updating the user: " + ex);
            }

            return codeStatus;
        }


        public static async Task<User> RecoverUserByUsername(string username)
        {
            User userObtained = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/user/" + username;
                HttpResponseMessage messageResponse = await server.GetAsync(url);

                if(messageResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await messageResponse.Content.ReadAsStringAsync();
                    ApiResponseUser responseDeserialize = JsonConvert.DeserializeObject<ApiResponseUser>(jsonResponse);

                    userObtained = responseDeserialize.Result;
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine("There is an error recovering the user: " + ex);
            }

            return userObtained;
        }

    }
}
