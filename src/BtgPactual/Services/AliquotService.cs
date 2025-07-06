
using BtgPactual.Configurations;
using BtgPactual.Entities;
using BtgPactual.Mapper;
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

        public async Task<List<FundDetailsDto>> List()
        {
            var funds = await _context.Funds
                .Include(x => x.Applications)
                .Include(x => x.Rescues)
                .ToListAsync();

            var fundsDto = funds.Select(x => x.MapToDto()).ToList();

            return fundsDto;
        }

        public async Task<RescueDto> Rescue(Request.Request request)
        {
            if (request.Value < 0)
                throw new ArgumentException("Rescue value must be greather than zero");

            var client = await _context.Clients
                .Include(x => x.Rescues)
                .Include(x => x.Applications)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId);

            if (client is null)
                throw new InvalidOperationException("Client not found!");

            var fund = await _context.Funds
                .FindAsync(request.FundNumber);

            if (fund is null)
                throw new InvalidOperationException("Fund not found!");

            await _context.Entry(fund).Collection(x => x.Applications).LoadAsync();

            var application = fund.GetApplication(request.ApplicationId);

            if (application.Value < request.Value)
                throw new ArgumentException("Rescue value cannot be fewer than application value");

            application.WithdrawBalance(request.Value);

            var rescue = new Rescue(request.Value, application);
            rescue.ClientId = request.ClientId;
            rescue.FundId = request.FundNumber;

            _context.Applications.Update(application);
            _context.Funds.Update(fund);

            _context.Rescues.Add(rescue);

            await _context.SaveChangesAsync();

            return new RescueDto(rescue.Id, rescue.RescueValue, rescue.RescueDate, rescue.IncomeTax, rescue.NetValue);
        }
    }
}
