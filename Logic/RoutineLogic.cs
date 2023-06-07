using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Security.Policy;

namespace Logic
{
    public static class RoutineLogic
    {
        private static string apiVersion = "v1";

        public static async Task<List<Routine>> GetRoutines()
        {
            List<Routine> routines = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/";
                HttpResponseMessage messageResponse = await server.GetAsync(url);
                string jsonResponse = await messageResponse.Content.ReadAsStringAsync();

                if (messageResponse.IsSuccessStatusCode)
                {
                    ApiResponseRoutine routinesDescerialized = JsonConvert.DeserializeObject<ApiResponseRoutine>(jsonResponse);
                    routines = routinesDescerialized.Routines;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at RoutineLogic class: " + ex);
            }

            return routines;
        }


        public static async Task<List<Routine>> GetRoutinesCreated(string username)
        {
            List<Routine> routines = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/routinesByUser/" + username;
                HttpResponseMessage messageResponse = await server.GetAsync(url);
                string jsonResponse = await messageResponse.Content.ReadAsStringAsync();

                if (messageResponse.IsSuccessStatusCode)
                {
                    ApiResponseRoutine routinesDescerialized = JsonConvert.DeserializeObject<ApiResponseRoutine>(jsonResponse);
                    routines = routinesDescerialized.Routines;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at RoutineLogic class: " + ex);
            }

            return routines;
        }


        public static async Task<List<Routine>> GetRoutinesFollowed(string username)
        {
            List<Routine> routines = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/routinesFollowed/" + username;
                HttpResponseMessage messageResponse = await server.GetAsync(url);
                string jsonResponse = await messageResponse.Content.ReadAsStringAsync();

                if (messageResponse.IsSuccessStatusCode)
                {
                    ApiResponseRoutine routinesDescerialized = JsonConvert.DeserializeObject<ApiResponseRoutine>(jsonResponse);
                    routines = routinesDescerialized.Routines;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error at RoutineLogic class: " + ex);
            }

            return routines;
        }


        public static async Task<int> SaveRoutine(string username, Routine routine)
        {
            int resultCode = (int)StatusCode.ProccessError;

            try
            {
                var data = new
                {
                    routine_creator = username,
                    name = routine.Name,
                    city = routine.City,
                    country = routine.Country,
                    routine_description = routine.Routine_Description,
                    visibility = routine.Visibility,
                    label_category = routine.Label_Category,
                    state_country = routine.State_Country,
                    town = routine.Town
                };
                string dataToSend = JsonConvert.SerializeObject(data);


                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PostAsync(url, contentToSend);

                resultCode = (int)messageResponse.StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error saving the routine: " + ex);
            }
            return resultCode;
        }


        public static async Task<int> DeleteRoutine(string idRoutine)
        {
            int resultCode = (int)StatusCode.ProccessError;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/" + idRoutine;
                HttpResponseMessage message = await server.DeleteAsync(url);

                resultCode = (int)message.StatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error deleting the routine: " + ex);
            }

            return resultCode;
        }


        public static async Task<int> UpdateRoutine(string idRoutine, Routine routineToUpdate)
        {
            int resultCode = (int)StatusCode.ProccessError;

            try
            {
                var data = new
                {
                    name = routineToUpdate.Name,
                    city = routineToUpdate.City,
                    country = routineToUpdate.Country,
                    routine_description = routineToUpdate.Routine_Description,
                    visibility = routineToUpdate.Visibility,
                    label_category = routineToUpdate.Label_Category,
                    state_country = routineToUpdate.State_Country,
                    town = routineToUpdate.Town,
                    followers = routineToUpdate.Followers,
                    routine_creator = routineToUpdate.Routine_Creator
                };
                string dataToSend = JsonConvert.SerializeObject(data);


                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/" + idRoutine;
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await server.PutAsync(url, contentToSend);

                resultCode = (int)message.StatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error deleting the routine: " + ex);
            }

            return resultCode;
        }


        public static async Task<int> FollowRoutine(string usarname, string idRoutine)
        {
            int resultCode = (int)StatusCode.ProccessError;

            try
            {
                var data = new
                {
                    username = usarname,
                    idRoutine = idRoutine
                };
                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/followRoutine";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await server.PostAsync(url, contentToSend);

                resultCode = (int)message.StatusCode;
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error following the routine: " + ex);
            }

            return resultCode;
        }


        public static async Task<int> UnfollowRoutine(string usarname, string idRoutine)
        {
            int resultCode = (int)StatusCode.ProccessError;

            try
            {
                var data = new
                {
                    username = usarname,
                    idRoutine = idRoutine
                };
                string dataToSend = JsonConvert.SerializeObject(data);

                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/routines/unfollowRoutine";
                HttpContent contentToSend = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await server.PostAsync(url, contentToSend);

                resultCode = (int)message.StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error following the routine: " + ex);
            }

            return resultCode;
        }
    }
}
