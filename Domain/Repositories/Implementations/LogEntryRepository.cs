using Model.Entities.Log;

namespace Domain.Repositories.Implementations;

public class LogEntryRepository : ARepository<LogEntry>, ILogEntryRepository
{
    public LogEntryRepository(IMongoDbConnectionService mongoDbConnectionService) : base(mongoDbConnectionService)
    {
    }
}