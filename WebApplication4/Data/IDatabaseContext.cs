using MongoDB.Driver;

namespace WebApplication4.Data
{
    public interface IDatabaseContext
    {
        IMongoDatabase Database { get; }
    }
}
