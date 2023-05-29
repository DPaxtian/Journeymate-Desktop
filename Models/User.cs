using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string userDescription { get; set; }
        public List<string> users_followed { get; set; }
        public List<string> routines_created { get; set; }
        public List<string> followed_routines { get; set; }
    }
}
