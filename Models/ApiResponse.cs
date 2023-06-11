using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class ApiResponseUser
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public User Result { get; set; }
    }

    public partial class ApiResponseRoutine
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("response")]
        public List<Routine> Routines { get; set; }
    }

    public partial class ApiResponseTask
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("response")]
        public List<Models.Task> Response { get; set; }
    }


    public partial class ApiResponseRoutineCreated
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }
    }


    public partial class ApiResponseRoutine<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public T Msg { get; set; }
    }
}
