using Microsoft.AspNetCore.Mvc;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFPartOrdersController : ControllerBase
    {
        private readonly ILogger<EFPartOrdersController> _logger;
        private IEFUnitOfWork _EFuow;
        public EFPartOrdersController(ILogger<EFPartOrdersController> logger,
            IEFUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _EFuow = ado_unitofwork;
        }
        [HttpGet("OrderID/{orderid}")]
        public async Task<ActionResult<IEnumerable<PartOrder>>> GetAllPartOrdersByOrderidAsync(int orderid)
        {
            try
            {
                var results = await _EFuow.eFPartOrdersRepository.GetAllPartOrdersByOrderIDAsync(orderid);
                _logger.LogInformation($"Отримали всі PartOrders з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllPartOrdersByUserAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //GET: api/events/Id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PartOrder>> GetPartOrderByIdAsync(int id)
        {
            try
            {
                var result = await _EFuow.eFPartOrdersRepository.GetByIdAsync(id);
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
        public async Task<ActionResult> PostPartOrderAsync([FromBody] PartOrder fullpartorder)
    {
        try
        {
            if (fullpartorder == null)
            {
                _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                return BadRequest("Обєкт comment є null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                return BadRequest("Обєкт PartOrder є некоректним");
            }
            var partorder = new PartOrder()
            {
                Article = fullpartorder.Article,
                OrderId = fullpartorder.OrderId,
                Quantity = fullpartorder.Quantity,
                Price = fullpartorder.Price
            };
            _EFuow.eFPartOrdersRepository.AddAsync(partorder);
            await _EFuow.SaveChangesAsync();
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
        public async Task<ActionResult> UpdatePartOrderAsync(int id, [FromBody] PartOrder updatedPartOrder)
        {
            try
            {
                if (updatedPartOrder == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("Order object is null");
                }

                var PartOrderEntity = await _EFuow.eFPartOrdersRepository.GetByIdAsync(id);
                if (PartOrderEntity == null)
                {
                    _logger.LogInformation($"PartOrder with ID: {id} was not found in the database");
                    return NotFound();
                }
                PartOrderEntity.Article = updatedPartOrder.Article;
                PartOrderEntity.OrderId = updatedPartOrder.OrderId;
                PartOrderEntity.Quantity = updatedPartOrder.Quantity;
                PartOrderEntity.Price = updatedPartOrder.Price;

                await _EFuow.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in UpdatePartOrderAsync() method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePartOrderByIdAsync(int id)
        {
            try
            {
                var event_entity = await _EFuow.eFPartOrdersRepository.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"PartOrder із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _EFuow.eFPartOrdersRepository.DeleteAsync(event_entity);
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
