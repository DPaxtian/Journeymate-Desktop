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




        public static async Task<string> SaveRoutine(string username, Routine routine)
        {
            string idRoutineCreated = null;

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

                if (messageResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await messageResponse.Content.ReadAsStringAsync();
                    ApiResponseRoutineCreated routineCreated = JsonConvert.DeserializeObject<ApiResponseRoutineCreated>(jsonResponse);
                    idRoutineCreated = routineCreated.Response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error saving the routine: " + ex);
            }

            return idRoutineCreated;
        }
    }
}
