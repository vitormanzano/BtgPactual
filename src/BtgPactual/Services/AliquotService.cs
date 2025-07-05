
using BtgPactual.Configurations;
using BtgPactual.Entities;
using BtgPactual.Responses;
using Microsoft.EntityFrameworkCore;

namespace BtgPactual.Services
{
    public class AliquotService : IAliquotService
    {
        private readonly ApplicationDbContext _context;

        public AliquotService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<ApplicationDto> Apply(Request.Request request)
        {
            if (request.Value <= 0)
                throw new ArgumentException("Application value should be greather than zero");

            var client = await _context.Clients
                .Include(x => x.Applications)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId);

            if (client is null)
                throw new InvalidOperationException("Client not found!");

            var fund = await _context.Funds
                .FindAsync(request.FundNumber);

            if (fund is null)
                throw new InvalidOperationException("Fund not found!");

            await _context.Entry(fund).Collection(x => x.Applications).LoadAsync();

            var application = new Application(
                request.Value, 
                request.FundNumber, 
                request.ClientId);

            fund.Applications.Add(application);
            client.Applications.Add(application);

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return new ApplicationDto(application.Id, application.Value, application.ApplicationDate);
        }

        public Task<FundDetailsDto> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<RescueDto> Rescue(Request.Request request)
        {
            throw new NotImplementedException();
        }
    }
}
