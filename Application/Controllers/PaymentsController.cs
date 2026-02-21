using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {

        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService) {
         _paymentService = paymentService;   
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
        {
            // Global Exception Handling Middleware will catch errors
            var result = await _paymentService.ProcessPaymentAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Implementation omitted for brevity
            return Ok();
        }
    }
}
