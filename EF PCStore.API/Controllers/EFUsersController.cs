using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFUsersController : ControllerBase
    {
        private readonly ILogger<EFUsersController> _logger;
        private IEFUnitOfWork _EFuow;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public EFUsersController(ILogger<EFUsersController> logger,
            IEFUnitOfWork ado_unitofwork,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _logger = logger;
            _EFuow = ado_unitofwork;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("/signin/{password}")]
        public async Task<ActionResult> LogInUserAsync([FromBody] User fullentity, string password)
        {
            try
            {
                var entity = await userManager.FindByNameAsync(fullentity.UserName);
                if (entity == null)
                {
                    return BadRequest("User does not exist");
                }
                var result = await signInManager.PasswordSignInAsync(entity, password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Logged In");
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpPost("LogOut")]
        public async Task<ActionResult<User>> LogOutAsync()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok("Logged Out");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpPost("{password}")]
        public async Task<ActionResult> PostUserAsync([FromBody] User fulluser, string password)
        {
            try
            {
                if (fulluser == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт є некоректним");
                }
                var entity = new User()
                {
                    UserName = fulluser.UserName,
                    Email = fulluser.Email,
                    FirstName = fulluser.FirstName,
                    LastName = fulluser.LastName,
                    Father = fulluser.Father,
                    PhoneNumber = fulluser.PhoneNumber,
                };
                var result = userManager.CreateAsync(entity, password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(entity, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpPut("/updateuser")]
        public async Task<ActionResult> UpdateTable1Async([FromBody] User updatedentity)
        {
            try
            {
                if (updatedentity == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("object is null");
                }

                var entity = await userManager.FindByNameAsync(updatedentity.UserName);
                if (entity == null)
                {
                    _logger.LogInformation($"username: {updatedentity.UserName} was not found in the database");
                    return NotFound();
                }
                entity.PhoneNumber = updatedentity.PhoneNumber;
                entity.Email = updatedentity.Email;
                entity.FirstName=updatedentity.FirstName;
                entity.LastName=updatedentity.LastName;
                entity.Father = updatedentity.Father;
                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpDelete("{uname}")]
        public async Task<ActionResult> DeleteTable1ByIdAsync(string uname)
        {
            try
            {
                var entity = await userManager.FindByNameAsync(uname);
                if (entity == null)
                {
                    _logger.LogInformation($"Id: {uname}, не був знайдейний у базі даних");
                    return NotFound();
                }
                await userManager.DeleteAsync(entity);
                await _EFuow.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("Phone/{Phone}")]
        public async Task<ActionResult<User>> GetUserByPhoneAsync(string Phone)
        {
            try
            {
                var results = await _EFuow.eFUsersRepository.GetUserByPhoneAsync(Phone);
                _logger.LogInformation($"Отримали всі User з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetUserByPhoneAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpGet("Email/{Email}")]
        public async Task<ActionResult<User>> GetUserByEmailAsync(string Email)
        {
            try
            {
                var results = await _EFuow.eFUsersRepository.GetUserByEmailAsync(Email);
                _logger.LogInformation($"Отримали всі User з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetUserByEmailAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        //GET: api/events/Id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var result = await _EFuow.eFUsersRepository.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogInformation($"User із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали User з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetUserByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
