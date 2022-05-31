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
    public class RefrigeratorController : ControllerBase
    {
        private readonly IRefrigeratorService _refrigeratorService;

        public RefrigeratorController(IRefrigeratorService refrigeratorService)
        {
            _refrigeratorService = refrigeratorService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Refrigerator>>> CreateRefrigerator(Refrigerator refrigerator)
        {
            var result = await _refrigeratorService.CreateRefrigerator(refrigerator);
            return Ok(result);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<ServiceResponse<Refrigerator>>> UpdateRefrigerator(Refrigerator refrigerator)
        {
            var result = await _refrigeratorService.UpdateRefrigerator(refrigerator);
            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteRefrigerator(int id)
        {
            var result = await _refrigeratorService.DeleteRefrigerator(id);
            return Ok(result);
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<ServiceResponse<List<Refrigerator>>>> GetRefrigerators()
        {
            var result = await _refrigeratorService.GetRefrigeratorsAsync();
            return Ok(result);
        }

        [HttpGet("{refrigeratorId}"), Authorize]
        public async Task<ActionResult<ServiceResponse<Refrigerator>>> GetRefrigerator(int refrigeratorId)
        {
            var result = await _refrigeratorService.GetRefrigeratorAsync(refrigeratorId);
            return Ok(result);
        }
    }
}
