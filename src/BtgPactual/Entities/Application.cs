namespace BtgPactual.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int FundId { get; set; }
        public Fund Fund { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        private Application() { }

        public Application(decimal value, int fundId, int clientId) 
        { 
            if (value <= 0)
            {
                throw new ArgumentException("Value of application cannot be fewer or equal zero");
            }

            Value = value;
            ApplicationDate = DateTime.Now;
            FundId = fundId;
            ClientId = clientId;
        }

        public void WithdrawBalance(decimal balance)
        {
            Value = Value - balance;
        }
    }
}
