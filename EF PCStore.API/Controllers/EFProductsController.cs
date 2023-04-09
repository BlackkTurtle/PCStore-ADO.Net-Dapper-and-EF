using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProductsController : ControllerBase
    {
        private readonly ILogger<EFProductsController> _logger;
        private IEFUnitOfWork _EFuow;
        public EFProductsController(ILogger<EFProductsController> logger,
            IEFUnitOfWork unitOfWork)
        {
            _logger = logger;
            _EFuow = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            try
            {
                var results = await _EFuow.eFProductsRepository.GetAllAsync();
                _logger.LogInformation($"Отримали всі Products з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllProductsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("BrandID")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByBrandIDAsync(int id)
        {
            try
            {
                var results = await _EFuow.eFProductsRepository.GetProductsByBrandAsync(id);
                _logger.LogInformation($"Отримали всі Products з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllProductsByBrandAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("TypeID")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByTypeIDAsync(int id)
        {
            try
            {
                var results = await _EFuow.eFProductsRepository.GetProductsByTypeAsync(id);
                _logger.LogInformation($"Отримали всі Products з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByTypeIDAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        [HttpGet("NameLike")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByNAMELikeAsync(string namelike)
        {
            try
            {
                var results = await _EFuow.eFProductsRepository.GetProductsByNameLikeAsync(namelike);
                _logger.LogInformation($"Отримали всі Products з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByNAMELikeAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
