using BtgPactual.Responses;

namespace BtgPactual.Services
{
    public interface IAliquotService
    {
        Task<ApplicationDto> Apply(Request.Request request);
        Task<RescueDto> Rescue(Request.Request request);
        Task<List<FundDetailsDto>> List();
    }
}
