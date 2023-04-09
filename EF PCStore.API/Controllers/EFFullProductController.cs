using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PCStoreEF.BLL.EFRepositories.Contracts;
using PCStoreEF.BLL.Entities;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFFullProductController : ControllerBase
    {
        private readonly ILogger<EFTypesController> _logger;
        private IEFUnitOfWork ef_unitow;
        private IEFBLLUnitOfWork _EFuow;
        public EFFullProductController(ILogger<EFTypesController> logger,
            IEFBLLUnitOfWork unitOfWork,
            IEFUnitOfWork ef_unitow)
        {
            _logger = logger;
            _EFuow = unitOfWork;
            this.ef_unitow = ef_unitow;
        }

        //GET: api/events
        [HttpGet("{article}")]
        public async Task<ActionResult<FullProduct>> GetFullProductByArticleAsync(int article)
        {
            try
            {
                var results = await _EFuow.eFProductsRepository.GetCompleteFullProductAsync(ef_unitow ,article);
                _logger.LogInformation($"Отримали fullProduct з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetFullProductByArticleAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
