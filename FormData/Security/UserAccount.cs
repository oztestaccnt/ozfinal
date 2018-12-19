using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;

namespace FormData.Security
{
    public class UserAccount
    {
        /// <summary>
        /// Generate a SHA1 Hash for entered value
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Hashed value</returns>
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var bytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(bytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i ++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static int GetUserId()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);

            return Convert.ToInt16(ticket.Name);
        }

    } // end of UserAccount class
}