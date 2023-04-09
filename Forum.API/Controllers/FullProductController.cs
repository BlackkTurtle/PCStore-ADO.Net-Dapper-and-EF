using Microsoft.AspNetCore.Mvc;
using PCStoreBLL.Entities;
using PCStoreBLL.Repositories.Contracts;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullProductController : ControllerBase
    {
        private readonly ILogger<TypeController> _logger;
        private IBLLUnitOfWork _ADOuow;
        public FullProductController(ILogger<TypeController> logger,
            IBLLUnitOfWork ado_unitofwork)
        {
            _logger = logger;
            _ADOuow = ado_unitofwork;
        }

        //GET: api/events/Id
        [HttpGet("{article}")]
        public async Task<ActionResult<FullProduct>> GetFullProductByArticleAsync(int article)
        {
            try
            {
                var result = await _ADOuow._productsRepository.GetFullProductsByArticleAsync(article);
                _ADOuow.Commit();
                if (result == null)
                {
                    _logger.LogInformation($"FullProduct із Article: {article}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали FullProduct з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetFullProductsByArticleAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
