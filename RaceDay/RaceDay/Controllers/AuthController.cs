using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay;
using RaceDay.Database;
using RaceDay.Models;
using RaceDay.Models;
using System.Security.Claims;

namespace RaceDay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly RaceDayDBContext _dbContext;
        private PasswordHasher<Organiser> Organiser_hasher = new();
        private PasswordHasher<Participant> Participant_hasher = new();

        public AuthController(RaceDayDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("particpants/register")]
        public IActionResult ParticipantRegister(ParticipantRegisterDto dto) //uses the data object for register (email, password and role)
        {
            //actual User object is created using the user input from RegisterDto
            var newUser = new Participant { Name = dto.Name, Age = dto.Age, AvatarUrl= dto.AvatarUrl, PhoneNumber = dto.PhoneNumber};
            newUser.Password = Participant_hasher.HashPassword(newUser, dto.Password);//using inbuilt hashing method hash the password

            if (_dbContext.Participant.Any(u => u.PhoneNumber == dto.PhoneNumber))
            {
                return Conflict("Phone number already registered");
            }

            _dbContext.Participant.Add(newUser); //store the hashed password to the db
            _dbContext.SaveChanges();
            return Ok("Registered");
        }

        [HttpPost("organisers/register")]
        public IActionResult OrganiserRegister(ParticipantRegisterDto dto) //uses the data object for register (email, password and role)
        {
            //actual User object is created using the user input from RegisterDto
            var newUser = new Organiser { Name = dto.Name };
            newUser.Password = Organiser_hasher.HashPassword(newUser, dto.Password);//using inbuilt hashing method hash the password

            _dbContext.Users.Add(newUser); //store the hashed password to the db
            _dbContext.SaveChanges();
            return Ok("Registered");
        }


        [HttpPost("organisers/login")]
        public async Task<IActionResult> OrganiserLogin(LoginDto dto)
        {
            //looks up the user by email. If nobody has the email return 401 status code
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);

            //var user = _dbContext.Organiser.FirstOrDefault(u => u.UserID == user.UserID);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, "Organiser")
        };

            /* Step 3: Bundles the claims of the user in an object, holding info about who this person is
             * Later on when a logged in persons role needs to be checked, this will be referred to */
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            /* Step 4: This makes the API server attach a "Set-Cookie" header to its HTTP response
             * and create a cookie with the encrypted details of the user */
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            // return Ok("Logged in");
            return Ok(new { Role = user.Role });
        }

        [HttpPost("participants/login")]
        public async Task<IActionResult> ParticipantLogin(LoginDto dto)
        {
            //looks up the user by email. If nobody has the email return 401 status code
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);

            //var user = _dbContext.Organiser.FirstOrDefault(u => u.UserID == user.UserID);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, "Participant")
        };

            /* Step 3: Bundles the claims of the user in an object, holding info about who this person is
             * Later on when a logged in persons role needs to be checked, this will be referred to */
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            /* Step 4: This makes the API server attach a "Set-Cookie" header to its HTTP response
             * and create a cookie with the encrypted details of the user */
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            // return Ok("Logged in");
            return Ok(new { Role = user.Role });
        }

        [HttpPost("logout")] //clears the users cookie
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out");
        }
    }
}
