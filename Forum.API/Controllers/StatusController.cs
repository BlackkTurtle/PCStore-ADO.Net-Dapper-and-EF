using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Types = Forum.DAL.Entities.Types;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private IUnitOfWork _ADOuow;
        public StatusController(ILogger<StatusController> logger,
            IUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statuses>>> GetAllStatusesAsync()
        {
            try
            {
                var results = await _ADOuow._statusesRepository.GetAllAsync();
                _ADOuow.Commit();
                _logger.LogInformation($"Отримали всі statuses з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllstatusesAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Statuses>> GetStatusByIdAsync(int id)
        {
            try
            {
                var result = await _ADOuow._statusesRepository.GetAsync(id);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"status із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали status з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetStatusByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
