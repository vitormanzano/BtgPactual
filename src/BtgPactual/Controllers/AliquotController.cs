using BtgPactual.Services;
using Microsoft.AspNetCore.Mvc;

namespace BtgPactual.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AliquotController : ControllerBase
    {
        private readonly IAliquotService _aliquotService;

        public AliquotController(IAliquotService aliquotService)
        {
            _aliquotService = aliquotService;
        }

        [HttpPost("{fundId}/application")]
        public async Task<IActionResult> Apply(int fundId, [FromBody] Request.Request request)
        {
            try
            {
                request.FundNumber = fundId;
                var application = await _aliquotService.Apply(request);

                return CreatedAtAction(nameof(Apply), new
                {
                    data = new
                    {
                        id = application.Id,
                        value = application.Value,
                        applicationDate = application.ApplicationDate,
                    }
                });
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException:
                        return NotFound(ex.Message);

                    default:
                        return BadRequest(ex.Message);
                }
            }
        }
    }
}
