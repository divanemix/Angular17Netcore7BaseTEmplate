using SmartWareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnits
{
    public class AuthenticationUnit : IAuthenticationUnit
    {
        private readonly SmartwareContext _context;

        public AuthenticationUnit(SmartwareContext context)
        {
            _context = context;
        }

        #region Authentication

        public bool VerifyPasswordHash(string password, string md5psw)
        {
            try
            {

                string md5password = GetMD5HashCode(password);
                return string.Equals(md5password, md5psw, StringComparison.OrdinalIgnoreCase);

            }
            catch (System.Exception)
            {

                return false;
            }

        }
        public string GetMD5HashCode(string password)
        {
            try
            {
                string md5password;
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    return md5password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }


        #endregion
    }


}
