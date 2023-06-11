using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum StatusCode
    {
       Ok = 200,
       ProccessError = 500,
       EntryError = 404,
       Unautorized = 401,
       Conflict = 409
    }
}
