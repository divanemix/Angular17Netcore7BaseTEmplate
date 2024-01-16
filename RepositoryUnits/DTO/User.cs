using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnits
{
    public class User
    {
        public User()
        {
            
        }

        public long UserId { get; set; } 
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Telephone { get; set; } = string.Empty;
        public string? Mobile { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

    }
}
