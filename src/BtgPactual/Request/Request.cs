namespace BtgPactual.Request
{
    public class Request
    {
        public int FundNumber { get; set; }
        public int ApplicationId { get; set; }
        public decimal Value { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }

        public Request(int applicationId, decimal value, DateTime date) 
        { 
            ApplicationId = applicationId;
            Value = value;
            Date = date;
        }   

        public Request() { }
    }
}
