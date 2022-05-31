using System.Threading.Tasks;
using System.Collections.Generic;
using Fridge.Shared;

namespace Fridge.Client.Services.RefrigeratorService
{
    public interface IRefrigeratorService
    {
        List<Refrigerator> Refrigerators { get; set; }
        Task GetRefrigerators();
        List<Indicator> Indicators { get; set; }
        Task GetIndicators(int refrigeratorId);
        Task<ServiceResponse<Refrigerator>> GetRefrigerator(int refrigeratorId);
        Task<Refrigerator> CreateRefrigerator(Refrigerator refrigerator);
        Task<Indicator> CreateRefrigeratorIndicator(Indicator indicator);
        Task<Refrigerator> UpdateRefrigerator(Refrigerator refrigerator);
        Task DeleteRefrigerator(Refrigerator refrigerator);
    }
}
