using Catlog.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Catlog.API.Repositories
{
    public interface IRepo
    {
        Task<IEnumerable<Product>> GetAll();
        Task<List<Product>> GetById(string id);
        Task<List<Product>> GetByName(string name);
        Task<List<Product>> GetByCategory(string CategoryName);

        Task CreateProduct(Product product);

        Task UpdateProduct(string id ,Product product);
        Task<bool> DeleteProduct(string id);
        Task<bool> UpdateProduct(Product product);
    }
}
