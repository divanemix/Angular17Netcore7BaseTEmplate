using System;
using System.Collections.Generic;

namespace Smartware
{
    public class UserDTO
    {
        public UserDTO()
        { 
    
            ActiveRoles = new List<string?>();

            Status = -1;
        }

        public long CitoGenUserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }

        public DateTime CreationDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Telephone { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

        public List<string?> ActiveRoles { get; set; }

        public bool LDAPRequireAuth { get; set; }


        public int Status { get; set; }
    }
}