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
    }
}
