using Microsoft.AspNetCore.Mvc;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFOrdersController : ControllerBase
    {
        private readonly ILogger<EFOrdersController> _logger;
        private IEFUnitOfWork _EFuow;
        public EFOrdersController(ILogger<EFOrdersController> logger,
            IEFUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _EFuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet("UserID/{userid}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByUserAsync(int userid)
        {
            try
            {
                var results = await _EFuow.eFOrdersRepository.GetAllOrdersByUserIDAsync(userid);
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
        public async Task<ActionResult<Order>> GetOrderByIdAsync(int id)
        {
            try
            {
                var result = await _EFuow.eFOrdersRepository.GetByIdAsync(id);
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
        public async Task<ActionResult> PostOrderAsync([FromBody] Order fullorder)
        {
            try
            {
                if (fullorder == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт comment є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт comment є некоректним");
                }
                var comment = new Order()
                {
                    OrderDate=DateTime.Now,
                    Adress=fullorder.Adress,
                    UserId=fullorder.UserId,
                    StatusId=fullorder.StatusId
                };
                _EFuow.eFOrdersRepository.AddAsync(comment);
                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostCommentAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderAsync(int id, [FromBody] Order updatedOrder)
        {
            try
            {
                if (updatedOrder == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("Comment object is null");
                }

                var OrderEntity = await _EFuow.eFOrdersRepository.GetByIdAsync(id);
                if (OrderEntity == null)
                {
                    _logger.LogInformation($"Comment with ID: {id} was not found in the database");
                    return NotFound();
                }

                // Update only the specific properties of the comment entity
                OrderEntity.StatusId = updatedOrder.StatusId;
                OrderEntity.Adress = updatedOrder.Adress;
                OrderEntity.UserId = updatedOrder.UserId;

                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in UpdateCommentAsync() method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderByIdAsync(int id)
        {
            try
            {
                var event_entity = await _EFuow.eFOrdersRepository.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"comment із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _EFuow.eFOrdersRepository.DeleteAsync(event_entity);
                await _EFuow.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteCommentByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
