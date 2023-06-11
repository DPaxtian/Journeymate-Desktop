using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        [JsonProperty("comment_creator")]
        public string Comment_Creator { get; set; }

        [JsonProperty("date_creation")]
        public DateTime Date_Creation { get; set; }

        [JsonProperty("comment_description")]
        public string Comment_Description { get; set; }
    }
}
