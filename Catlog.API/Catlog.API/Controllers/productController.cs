using Catlog.API.Model;
using Catlog.API.Repositories;
using Catlog.API.Servises;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace Catlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly MongoServisescs _mongoServisescs;
      
        public productController(MongoServisescs mongoServisescs)
        {
            _mongoServisescs = mongoServisescs;
            


        }
        [HttpGet]
        public async Task<List<Product>> getAlldataASYNC()
        {
            return (List<Product>)await _mongoServisescs.GetAll();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var fun = await _mongoServisescs.GetById(id);
            if(fun is null ) return NotFound();

            await _mongoServisescs.DeleteProduct(id);
            return NoContent();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(string id)
        {
            var fun2= await _mongoServisescs.GetById(id);
            if( fun2 is null ) return NotFound();
            return Ok(fun2);
        }
        [HttpPost]
        public async Task<ActionResult>Post(Product newid)
        {
            await _mongoServisescs.CreateProduct(newid);    
            return Ok(newid);
        }
        [HttpPut]
        public async Task<ActionResult>Update( Product udatepdt)
        {
            var idexit = await _mongoServisescs.UpdateProduct(udatepdt);
            if (udatepdt.Id is null ) return NotFound();
            await _mongoServisescs.UpdateProduct( udatepdt);
            return Ok(idexit);

        }
      
        
       

    }
}
