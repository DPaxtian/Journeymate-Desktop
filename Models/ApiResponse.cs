using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class ApiResponseUser
    {
        public int code { get; set; }
        public User msg { get; set; }
    }
}
