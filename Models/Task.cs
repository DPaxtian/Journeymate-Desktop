using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Task
    {

        [JsonProperty("_id")]
        public string _Id {  get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("task_description")]
        public string Task_Decription { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }

        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        [JsonProperty("valorations")]
        public List<Valoration> Valorations { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Task_Comments { get; set; }
    }


    public partial class TaskItem
    {
        [JsonProperty("task")]
        public string TaskId { get; set; }
    }
}
