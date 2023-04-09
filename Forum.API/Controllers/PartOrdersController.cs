using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Types = Forum.DAL.Entities.Types;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartOrdersController : ControllerBase
    {
        private readonly ILogger<PartOrdersController> _logger;
        private IUnitOfWork _ADOuow;
        public PartOrdersController(ILogger<PartOrdersController> logger,
            IUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet("OrderID/{orderid}")]
        public async Task<ActionResult<IEnumerable<PartOrders>>> GetAllPartOrdersByOrderidAsync(int orderid)
        {
            try
            {
                var results = await _ADOuow._artOrdersRepository.GetAllPartOrdersByOrderIDAsync(orderid);
                _ADOuow.Commit();
                _logger.LogInformation($"Отримали всі PartOrders з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllPartOrdersByOrderidAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PartOrders>> GetPartOrderByIdAsync(int id)
        {
            try
            {
                var result = await _ADOuow._artOrdersRepository.GetAsync(id);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"PartOrder із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Partorder з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetPartOrderByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostPartOrderAsync([FromBody] PartOrders order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт PartOrder є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт partorder є некоректним");
                }
                var created_id = await _ADOuow._artOrdersRepository.AddAsync(order);
                _ADOuow.Commit();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostPartOrderAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePartOrderAsync(int id, [FromBody] PartOrders order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт partorder є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт partorder є некоректним");
                }

                var event_entity = await _ADOuow._artOrdersRepository.GetAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"partorder із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _ADOuow._artOrdersRepository.ReplaceAsync(order);
                _ADOuow.Commit();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdatePartOrderAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePartOrderByIdAsync(int id)
        {
            try
            {
                var event_entity = await _ADOuow._artOrdersRepository.GetAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"PartOrder із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _ADOuow._artOrdersRepository.DeleteAsync(id);
                _ADOuow.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeletePartOrderByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
