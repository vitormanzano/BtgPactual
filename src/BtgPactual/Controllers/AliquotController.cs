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

        [HttpPost("{fundId}/rescue")]
        public async Task<IActionResult> Rescue(int fundId, [FromBody] Request.Request request)
        {
            try
            {
                request.FundNumber = fundId;
                var rescue = await _aliquotService.Rescue(request);

                return CreatedAtAction(nameof(Rescue), new
                {
                    data = new
                    {
                        id = rescue.Id,
                        value = rescue.Value,
                        applicationDate = rescue.RescueDate,
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

        [HttpGet("list")]
        public async Task<IActionResult> Rescue()
        {
            try
            {
                var fundApplications = await _aliquotService.List();

                return Ok(new
                {
                    data = fundApplications
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
