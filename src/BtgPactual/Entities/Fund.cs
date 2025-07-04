namespace BtgPactual.Entities
{
    public class Fund
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Application> Applications { get; set; }
        public List<Rescue> Rescues { get; set; }
        
        public Fund(string name)
        {
            Name = name;
        }


        private Fund() { }

        public Application GetApplication(int applicationId)
        {
            var application = Applications.Find(x => x.Id == applicationId);

            if (application == null)
                throw new ArgumentException("Application not found!");

            return application;
        }
    }
}
