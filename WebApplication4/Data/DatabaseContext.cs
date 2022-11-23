using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication4.Model;

namespace WebApplication4.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        public IMongoDatabase Database { get; set; }

        public DatabaseContext(IOptions<AppSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            Database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }
    }
}
