namespace BtgPactual.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();
        public List<Rescue> Rescues { get; set; } = new List<Rescue>();

        private Client() { }
    }
}
