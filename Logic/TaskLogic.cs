using Models;
using Newtonsoft.Json;
using System;
using System.CodeDom;
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


        public static async System.Threading.Tasks.Task<int> AddNewTaskToRoutine(string idRoutineCreated, Models.Task taskToRoutine)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/task/";
                var dataTask = new
                {
                    idRoutine = idRoutineCreated,
                    name = taskToRoutine.Name,
                    task_description = taskToRoutine.Task_Description,
                    address = taskToRoutine.Address,
                    budget = taskToRoutine.Budget,
                    isCompleted = taskToRoutine.IsCompleted
                };

                string dataSerialized = JsonConvert.SerializeObject(dataTask);
                HttpContent contentToSend = new StringContent(dataSerialized, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PostAsync(url, contentToSend);

                codeStatus = (int)messageResponse.StatusCode;

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return codeStatus;
        }


        public static async System.Threading.Tasks.Task<int> EditTask(Models.Task taskToEdit)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/task/" + taskToEdit._Id;
                var dataTask = new
                {
                    name = taskToEdit.Name,
                    task_description = taskToEdit.Task_Description,
                    address = taskToEdit.Address,
                    budget = taskToEdit.Budget,
                    isCompleted = taskToEdit.IsCompleted
                };

                string dataSerialized = JsonConvert.SerializeObject(dataTask);
                HttpContent contentToSend = new StringContent(dataSerialized, Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponse = await server.PutAsync(url, contentToSend);

                codeStatus = (int)messageResponse.StatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return codeStatus;
        }


        public static async System.Threading.Tasks.Task<int> DeleteTask(string idTask)
        {
            int codeStatus = (int)StatusCode.ProccessError;
            try
            {
                HttpClient server = new HttpClient();
                string url = "http://localhost:9000/api/" + apiVersion + "/task/" + idTask;
                
                HttpResponseMessage messageResponse = await server.DeleteAsync(url);

                codeStatus = (int)messageResponse.StatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return codeStatus;
        }

    }
}
