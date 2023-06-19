using Catlog.API.Data;
using Catlog.API.Model;
using Catlog.API.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Catlog.API.Servises
{
    // Depndency Injection 
    public class MongoServisescs : CotaLogContextSeed, ICatalogContext, IRepo
    {

        public IMongoCollection<Product> Pdt { get; }

        // Mongo cONFIGURATION 

        public MongoServisescs(IOptions<MongoConnector> connect)
        {
            MongoClient client = new MongoClient(connect.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(connect.Value.DatabaseName);
            Pdt = database.GetCollection<Product>(connect.Value.CollectionName);


        }


        // Create Data 
        public async Task CreateProduct(Product product)
        {
            await Pdt.InsertOneAsync(product);
        }

        // Delete data 
            public async Task<bool> DeleteProduct(string id)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);
                await Pdt.DeleteOneAsync(filter);
                return true;

            }


        // Get All Data 
            public async Task<IEnumerable<Product>> GetAll()
            {
                var res =  await Pdt.Find(_ => true).ToListAsync();
              if (res.Count==0)
              {
                
                 var res2= await GetData(Pdt);
                  return res2;



              }
                  return res;
                
            }

       
        // Fetch Data from database  using CategoryName 
            public async Task<List<Product>> GetByCategory(string CategoryName)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, CategoryName);
                return await Pdt.Find(filter).ToListAsync();
            }

        // Fetch Data from  database using Getby Id 
            public async Task<List<Product>> GetById(string id)
            {
                return await Pdt.Find(x => x.Id == id).ToListAsync();
            }
        // Fetch Data from Database Using Name 
            public async Task<List<Product>> GetByName(string name)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);
                return await Pdt.Find(filter).ToListAsync();
            }
        // Update data 
            public async Task UpdateProduct(string id, Product newproduct) =>
                await Pdt.ReplaceOneAsync(x => x.Id == id, newproduct);

       

       public  async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(X => X.Id, product.Id);
            if (product.Id == null )
            {
                 return false ;

            }

           var res= await Pdt.ReplaceOneAsync(filter, product);
            return res.IsAcknowledged && res.ModifiedCount>  0;
        }
    }









    }
