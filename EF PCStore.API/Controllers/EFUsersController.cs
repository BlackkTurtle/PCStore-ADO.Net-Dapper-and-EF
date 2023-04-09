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
        public EFUsersController(ILogger<EFUsersController> logger,
            IEFUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _EFuow = ado_unitofwork;
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

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostUserAsync([FromBody] User fulluser)
        {
            try
            {
                if (fulluser == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт User є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт User є некоректним");
                }
                var user = new User()
                {
                    Password = fulluser.Password,
                    Email = fulluser.Email,
                    FirstName = fulluser.FirstName,
                    LastName=fulluser.LastName,
                    Father = fulluser.Father,
                    Phone = fulluser.Phone
                };
                _EFuow.eFUsersRepository.AddAsync(user);
                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostUserAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] User updatedUser)
        {
            try
            {
                if (updatedUser == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("User object is null");
                }

                var UserEntity = await _EFuow.eFUsersRepository.GetByIdAsync(id);
                if (UserEntity == null)
                {
                    _logger.LogInformation($"User with ID: {id} was not found in the database");
                    return NotFound();
                }
                UserEntity.Password = updatedUser.Password;
                UserEntity.FirstName = updatedUser.FirstName;
                UserEntity.LastName = updatedUser.LastName;
                UserEntity.Father = updatedUser.Father;
                UserEntity.Phone = updatedUser.Phone;
                UserEntity.Email = updatedUser.Email;

                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in UpdateUserAsync() method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserByIdAsync(int id)
        {
            try
            {
                var event_entity = await _EFuow.eFUsersRepository.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"User із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _EFuow.eFUsersRepository.DeleteAsync(event_entity);
                await _EFuow.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteUserByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
