namespace BtgPactual.Responses
{
    public record FundDetailsDto(
        int Id,
        string Name,
        List<ApplicationDto> Applications,
        List<RescueDto> Rescues
    );
   
    public  record ApplicationDto(int Id, decimal Value, DateTime ApplicationDate);

    public record RescueDto(int Id, decimal Value, DateTime RescueDate, decimal IncomeTax, decimal NetValue);
}
