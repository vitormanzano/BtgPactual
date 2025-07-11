﻿namespace BtgPactual.Entities
{
    public class Rescue
    {
        public int Id { get; set; }
        public decimal RescueValue { get; set; }
        public decimal IncomeTax { get; set; }
        public DateTime RescueDate { get; set; }
        public int FundId { get; set; }
        public Fund Fund { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal NetValue { get; set; }

        public Rescue(decimal rescueValue, Application application)
        {
            RescueValue = rescueValue;
            RescueDate = DateTime.Now;
            CalculateIncomeTax(application);
        }

        private Rescue() { }

        private void CalculateIncomeTax(Application application)
        {
            var profit = RescueValue - application.Value;
            var applicationTime = (RescueDate - application.ApplicationDate).TotalDays / 365;

            if (applicationTime <= 1)
                IncomeTax = profit * 0.225m;
            else if (applicationTime <= 2)
                IncomeTax = profit * 0.185m;
            else
                IncomeTax = profit * 0.15m;

            NetValue = RescueValue - IncomeTax;
        }
    }
}
