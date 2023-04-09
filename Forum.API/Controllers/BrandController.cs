using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Types = Forum.DAL.Entities.Types;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private IUnitOfWork _ADOuow;
        public BrandController(ILogger<BrandController> logger,
            IUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }
        //GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetAllBrandsAsync()
        {
            try
            {
                var results = await _ADOuow._brandRepository.GetAllAsync();
                _ADOuow.Commit();
                _logger.LogInformation($"Отримали всі Brands з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllEventsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
