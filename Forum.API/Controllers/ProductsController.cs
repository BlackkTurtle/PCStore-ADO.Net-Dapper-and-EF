using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Types = Forum.DAL.Entities.Types;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IUnitOfWork _ADOuow;
        public ProductsController(ILogger<ProductsController> logger,
            IUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProductsAsync()
        {
            try
            {
                var results = await _ADOuow._productsRepository.GetAllAsync();
                _ADOuow.Commit();
                _logger.LogInformation($"Отримали всі Products з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllEventsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("BrandID")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductsByBrandIDAsync(int id)
        {
            try
            {
                var result = await _ADOuow._productsRepository.GetProductsByBrandAsync(id);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"Products із BrandID: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Products з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByBrandIDAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("TypeID")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductsByTypeIDAsync(int id)
        {
            try
            {
                var result = await _ADOuow._productsRepository.GetProductsByTypeAsync(id);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"Products із TypeID: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Products з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByTypeIDAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("NameLike")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductsByNAMELikeAsync(string namelike)
        {
            try
            {
                var result = await _ADOuow._productsRepository.GetProductsByNameLikeAsync(namelike);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"Products із NameLike: {namelike}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Products з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByNAMELikeAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
