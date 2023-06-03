﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//faltan atributos aun...
namespace Models
{
    public class Routine
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("routine_description")]
        public string Routine_Description { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("label_category")]
        public string Label_Category { get; set; }

        [JsonProperty("state_country")]
        public string State_Country { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("budget")]
        public string Budget { get; set; }

        [JsonProperty("valoration")]
        public List<Valoration> Valorations { get; set; }

        [JsonProperty("tasks")]
        public List<string> Tasks { get; set; }

        [JsonProperty("routine_comments")]
        public List<Comment> Routine_Comments { get; set; }

    }
}

