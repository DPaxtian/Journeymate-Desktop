using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Encription
    {
        public static String GetHashedPassword(string password)
        {
            string passwordHashed = "";
            string key = "$_jourNeyMateIsTheB35TPlatForMForJourney5&EnJoy-$$";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHashed = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            return passwordHashed;            
        }
    }
}
