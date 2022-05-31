using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

using Fridge.Server.Services.RefrigeratorService;
using Fridge.Shared;

namespace Fridge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicatorController : ControllerBase
    {
        private readonly IRefrigeratorService _refrigeratorService;

        public IndicatorController(IRefrigeratorService refrigeratorService)
        {
            _refrigeratorService = refrigeratorService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Indicator>>> CreateIndicator(Indicator indicator)
        {
            var result = await _refrigeratorService.CreateRefrigeratorIndicator(indicator);
            return Ok(result);
        }

        [HttpGet("{refrigeratorId}"), Authorize]
        public async Task<ActionResult<ServiceResponse<List<Indicator>>>> GetIndicators(int refrigeratorId)
        {
            var result = await _refrigeratorService.GetIndicatorsAsync(refrigeratorId);
            return Ok(result);
        }
    }
}
