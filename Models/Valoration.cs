using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Valoration
    {
        [JsonProperty("user")]
        public string user { set; get; }

        [JsonProperty("valoration")]
        public int valoration { get; set; }
    }
}
