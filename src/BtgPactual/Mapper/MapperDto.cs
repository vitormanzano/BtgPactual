using BtgPactual.Entities;
using BtgPactual.Responses;

namespace BtgPactual.Mapper
{
    public static class MapperDto
    {
        public static FundDetailsDto MapToDto(this Fund fund)
        {
            return new FundDetailsDto(
                fund.Id,
                fund.Name,
                fund.Applications
                .Select(x => new ApplicationDto(x.Id, x.Value, x.ApplicationDate)).ToList(),

                fund.Rescues
                .Select(x => new RescueDto(x.Id, x.RescueValue, x.RescueDate, x.IncomeTax, x.NetValue)).ToList());
        }
    }
}
