using System.Security.Cryptography;
using BCrypt.Net;
using System.Text;

namespace Admin.Core.Global
{
    public class Hash
    {
        public static string hashPassword(string Password)
        {
           return BCrypt.Net.BCrypt.HashPassword(Password); 
        }
    }
}