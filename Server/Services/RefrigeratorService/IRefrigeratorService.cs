using System.Threading.Tasks;
using System.Collections.Generic;
using Fridge.Shared;

namespace Fridge.Server.Services.RefrigeratorService
{
    public interface IRefrigeratorService
    {
        Task<ServiceResponse<List<Refrigerator>>> GetRefrigeratorsAsync();
        Task<ServiceResponse<List<Indicator>>> GetIndicatorsAsync(int refrigeratorId);
        Task<ServiceResponse<Refrigerator>> GetRefrigeratorAsync(int refrigeratorId);
        Task<ServiceResponse<Refrigerator>> CreateRefrigerator(Refrigerator refrigerator);
        Task<ServiceResponse<Indicator>> CreateRefrigeratorIndicator(Indicator indicator);
        Task<ServiceResponse<Refrigerator>> UpdateRefrigerator(Refrigerator refrigerator);
        Task<ServiceResponse<bool>> DeleteRefrigerator(int refrigeratorId);
    }
}
