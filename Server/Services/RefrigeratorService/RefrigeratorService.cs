using System;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Fridge.Server.Data;
using Fridge.Shared;


namespace Fridge.Server.Services.RefrigeratorService
{
    public class RefrigeratorService : IRefrigeratorService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefrigeratorService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<Refrigerator>>> GetRefrigeratorsAsync()
        {
            var response = new ServiceResponse<List<Refrigerator>>
            {
                Data = await _context.Refrigerators
                    .Where(r => !r.Deleted)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Indicator>>> GetIndicatorsAsync(int refrigeratorId)
        {
            var response = new ServiceResponse<List<Indicator>>
            {
                Data = await _context.Indicators
                    .Where(i => i.FridgeId == refrigeratorId)
                    .OrderByDescending(i => i.DateCreated)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Refrigerator>> GetRefrigeratorAsync(int refrigeratorId)
        {
            var response = new ServiceResponse<Refrigerator>();
            Refrigerator refrigerator = null;

            refrigerator = await _context.Refrigerators
                    .FirstOrDefaultAsync(r => r.Id == refrigeratorId && !r.Deleted);

            if (refrigerator == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this refrigerator does not exist.";
            }
            else
            {
                response.Data = refrigerator;
            }

            return response;
        }

        public async Task<ServiceResponse<Refrigerator>> CreateRefrigerator(Refrigerator refrigerator)
        {
            _context.Refrigerators.Add(refrigerator);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Refrigerator> { Data = refrigerator };
        }

        public async Task<ServiceResponse<Indicator>> CreateRefrigeratorIndicator(Indicator indicator)
        {
            _context.Indicators.Add(indicator);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Indicator> { Data = indicator };
        }

        public async Task<ServiceResponse<Refrigerator>> UpdateRefrigerator(Refrigerator refrigerator)
        {
            var dbRefrigerator = await _context.Refrigerators
                .FirstOrDefaultAsync(r => r.Id == refrigerator.Id);

            if (dbRefrigerator == null)
            {
                return new ServiceResponse<Refrigerator>
                {
                    Success = false,
                    Message = "Refrigerator not found."
                };
            }

            dbRefrigerator.Name = refrigerator.Name;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Refrigerator> { Data = refrigerator };
        }



        public async Task<ServiceResponse<bool>> DeleteRefrigerator(int refrigeratorId)
        {
            var dbRefrigerator = await _context.Refrigerators.FindAsync(refrigeratorId);
            if (dbRefrigerator == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Refrigerator not found."
                };
            }

            dbRefrigerator.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
