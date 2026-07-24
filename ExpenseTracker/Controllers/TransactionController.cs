using ExpenseTracker.Models;
using ExpenseTracker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTransaction(TransactionRequest request)
        {
            TransactionRepository transaction = new TransactionRepository(_configuration);

            long transactionId = await transaction.AddTransaction(request);

            if (transactionId > 0)
            {
                return Ok(new { amount = request.amount, merchant = request.merchant });
            }
            return BadRequest("Somewent wrong, try again later");
        }
    }
}
