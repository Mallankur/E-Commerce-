using Catlog.API.Model;
using MongoDB.Driver;

namespace Catlog.API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Pdt { get; }

        
    }
}
