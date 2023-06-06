using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TaskLogic
    {

        private static string apiVersion = "v1";

        public static async Task<List<Models.Task>> GetTasksByRoutineId(string idRoutine)
        {
            List<Models.Task> tasks = null;

            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/taskofRoutines/" + idRoutine;
                HttpResponseMessage messageResponse = await server.GetAsync(url);

                if (messageResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await messageResponse.Content.ReadAsStringAsync();
                    ApiResponseTask responseDeserialize = JsonConvert.DeserializeObject<ApiResponseTask>(jsonResponse);

                    tasks = responseDeserialize.Response;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an error at RoutineLogic class: " + ex);
            }

            return tasks;
        }

    }
}
