
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using RepositoryUnits;

using System.DirectoryServices;
using System.Runtime.Versioning;
using Smartware;

namespace SmartWare
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IAuthenticationUnit _authUnit;

        public AuthController(IAuthenticationUnit authUnit,
            ILogger<AuthController> logger,
            IConfiguration config)
        {

            //_mapper = mapper;

            //_repo = repo;
            //_repoAdmin = repoAdmin;
            //_repositoryUsers = repositoryUsers;
            //_hostingEnvironment = hostingEnvironment;
            //_creoConfig = creoConfig;
            _authUnit = authUnit;
            _logger = logger;
            _config = config;
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        [SupportedOSPlatform("Windows")]
        public async Task<IActionResult> Login(User userDto)
        {
            var serviceUser = _config.GetSection("AppSettings:UserService").Value;
            var servicePassword = _config.GetSection("AppSettings:Password").Value ?? "None";//=> a
            try
            {
                User userFromRepo;

                if (userDto?.Username?.ToLower() == serviceUser?.ToLower()) //=> user is not present in db is only for NPLUST use
                {
                    if (!_authUnit.VerifyPasswordHash(userDto?.Password ?? "", servicePassword))
                    {
                        return BadRequest("Service user data not found.");
                    }
                    userFromRepo = new User();
                    userFromRepo.Username = userDto?.Username;

                }
                else
                {
                    Tuple<bool, User> ldapInfo = await TryLDAPAuthentication(userDto?.Username ?? "", userDto?.Password ?? "");
                    bool bLDAPAutenticated = ldapInfo.Item1;

                    if (!bLDAPAutenticated)
                        return BadRequest($"{userDto?.Username} => Username o password non trovati, LDAP authentication failed");

                    userFromRepo = ldapInfo.Item2;
                }

                UserDTO uservalid = await GetLoggedUserData(userFromRepo);

                return Ok(uservalid);
            }
            catch (Exception ex)
            {

                return BadRequest($"Login failed {ex.Message}");
            }

        }


        [AllowAnonymous]
        [Route("test")]
        [HttpGet]
        public IActionResult Test()
        {


            return Ok("Ci sono!");


        }
        [SupportedOSPlatform("Windows")]
        private async Task<Tuple<bool, User>> TryLDAPAuthentication(string userName, string password)
        {
            bool bLDAPAutenticated = false;
            User usr = new User();
            Tuple<bool, User> defaultData = Tuple.Create(false, usr);
            try
            {

                string domain = _config.GetSection("LDAP:Domanin").Value ?? "LDAP://NplusT";

                using (System.DirectoryServices.DirectoryEntry directoryEntry = new System.DirectoryServices.DirectoryEntry(domain, userName, password, AuthenticationTypes.Secure))
                {
                    try
                    {
                        bLDAPAutenticated = directoryEntry.NativeObject?.ToString() != null;
                    }
                    catch (Exception)
                    {
                        //Accesso alla actuive directory corretto, utente non trovato
                    }


                    usr = GetLDAPUserData(userName, directoryEntry);

                    Tuple<bool, User> resp = Tuple.Create(bLDAPAutenticated, usr);
                    return await Task.FromResult<Tuple<bool, User>>(resp);

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Si è verificato un errore durante l'autenticazione LDAP {userName}: {ex.Message}", ex.InnerException);
            }
            return await Task.FromResult<Tuple<bool, User>>(defaultData);

        }

        [SupportedOSPlatform("Windows")]
        private User GetLDAPUserData(string username, System.DirectoryServices.DirectoryEntry directoryEntry)
        {
            User usr;
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry);
            searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";

            SearchResult? result = searcher.FindOne();

            if (result != null)
            {
                System.DirectoryServices.DirectoryEntry? foundUser = result?.GetDirectoryEntry();
                if (foundUser != null)
                {
                    object? firstName = foundUser.Properties["givenName"]?.Value;
                    object? lastName = foundUser.Properties["sn"]?.Value;
                    object? email = foundUser.Properties["mail"]?.Value;

                    usr = new User
                    {
                        Username = username,
                        FirstName = firstName?.ToString() ?? "",
                        LastName = lastName?.ToString() ?? "",
                        Email = email?.ToString() ?? ""
                    };
                    return usr;
                }
            }

            return null!;
        }


        private async Task<UserDTO> GetLoggedUserData(User userFromRepo)
        {
            //var rolesPerUser = roles.Select(it => it.Name).Distinct().ToList();
            //  User uservalid = _mapper.Map<Citogenuser, creouserDTO>(userFromRepo);
            //laboratoryDTO labDTO = _mapper.Map<Laboratory, laboratoryDTO>(laboratory);
            //  = _mapper.Map<Citogenuser, creouserDTO>(userFromRepo);
            UserDTO uservalid = new UserDTO();
            uservalid.Username = userFromRepo.Username;
            var claims = new[]
            {
                new Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, uservalid.Username!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(720),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            uservalid.Token = tokenHandler.WriteToken(token);


            return await Task.FromResult<UserDTO>(uservalid);
        }
    }
}