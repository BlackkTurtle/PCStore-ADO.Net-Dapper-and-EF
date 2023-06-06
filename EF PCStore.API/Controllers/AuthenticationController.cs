using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EF_PCStore.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PCStoreEF.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EF_PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> UserManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly IConfiguration Configuration;
        public AuthenticationController(UserManager<User> userManager,RoleManager<Role> roleManager,IConfiguration configuration)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user= await UserManager.FindByNameAsync(model.UserName);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles=await UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));
                }
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var userExists = await UserManager.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exist" });
            }
            var user = new User()
            {
                FirstName=registerModel.FirstName,
                LastName=registerModel.LastName,
                Father=registerModel.Father,
                Email=registerModel.Email,
                PhoneNumber=registerModel.PhoneNumber,
                UserName=registerModel.UserName,
                SecurityStamp=Guid.NewGuid().ToString()
            };
            var result=await UserManager.CreateAsync(user,registerModel.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creating error" });
            }
            return Ok(new Response { Status = "Success", Message = "User created" });
        }
        [HttpPost]
        [Route("Register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var userExists = await UserManager.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exist" });
            }
            var user = new User()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Father = registerModel.Father,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await UserManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creating error" });
            }
            if (!await RoleManager.RoleExistsAsync(UserRoles.Admin))
                await RoleManager.CreateAsync(new Role(UserRoles.Admin));
            if (!await RoleManager.RoleExistsAsync(UserRoles.User))
                await RoleManager.CreateAsync(new Role(UserRoles.User));
            if (await RoleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await UserManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await RoleManager.RoleExistsAsync(UserRoles.User))
            {
                await UserManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created" });
        }
        private JwtSecurityToken GetToken(List<Claim> authclaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer : Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                expires:DateTime.Now.AddHours(3),
                claims:authclaims,
                signingCredentials:new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
                ) ;
            return token ;
        }
    }
}
