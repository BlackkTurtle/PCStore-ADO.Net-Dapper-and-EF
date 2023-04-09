using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Types = Forum.DAL.Entities.Types;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private IUnitOfWork _ADOuow;
        public OrdersController(ILogger<OrdersController> logger,
            IUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet("UserID/{userid}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetAllOrdersByUserAsync(int userid)
        {
            try
            {
                var results = await _ADOuow._rdersRepository.GetAllOrdersByUserIDAsync(userid);
                _ADOuow.Commit();
                _logger.LogInformation($"Отримали всі Orders з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllOrdersByUserAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Orders>> GetOrderByIdAsync(int id)
        {
            try
            {
                var result = await _ADOuow._rdersRepository.GetAsync(id);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"Order із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали order з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetOrderByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostOrderAsync([FromBody] Orders order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт Order є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт order є некоректним");
                }
                var created_id = await _ADOuow._rdersRepository.AddAsync(order);
                _ADOuow.Commit();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostOrderAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderAsync(int id, [FromBody] Orders order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт order є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт order є некоректним");
                }

                var event_entity = await _ADOuow._rdersRepository.GetAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"order із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _ADOuow._rdersRepository.ReplaceAsync(order);
                _ADOuow.Commit();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateOrderAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderByIdAsync(int id)
        {
            try
            {
                var event_entity = await _ADOuow._rdersRepository.GetAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"Order із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _ADOuow._rdersRepository.DeleteAsync(id);
                _ADOuow.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteOrderByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
