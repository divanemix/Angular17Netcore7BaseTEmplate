using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnits
{
    public  interface IAuthenticationUnit
    {
        bool VerifyPasswordHash(string password, string md5psw);
        public string GetMD5HashCode(string password);
    }
}
