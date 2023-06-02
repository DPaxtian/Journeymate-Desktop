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
        public string _Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Routine_Description { get; set; }
        public string Visibility { get; set; }
        public string label_category { get; set; }
        public string State_Country { get; set; }
        public string Town { get; set; }
        public string Creator { get; set; }
        public string Budget { get; set; }

        public int CountComments { get; set; }

    }
}

