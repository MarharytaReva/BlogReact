using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tipoBlog.Models
{
    public static class CryptoService
    {
        public static string getHash(string str)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            return Convert.ToBase64String(hash);
        }
    }
}
