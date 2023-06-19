using Catlog.API.Model;
using Catlog.API.Repositories;
using Catlog.API.Servises;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace Catlog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly MongoServisescs _mongoServisescs;
        private readonly ILogger<productController> _logger;

        public productController(MongoServisescs mongoServisescs, ILogger<productController> logger)
        {
            _mongoServisescs = mongoServisescs;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));



        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<List<Product>> getAlldataASYNC()
        {
            return (List<Product>)await _mongoServisescs.GetAll();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var fun = await _mongoServisescs.GetById(id);
            if (fun is null) return NotFound();

            await _mongoServisescs.DeleteProduct(id);
            return NoContent();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(string id)
        {
            var fun2 = await _mongoServisescs.GetById(id);
            if (fun2 is null)
                return NotFound();
            return Ok(fun2);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product newid)
        {
            await _mongoServisescs.CreateProduct(newid);
            return Ok(newid);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Product udatepdt)
        {
            if (udatepdt.Id is null ) return NotFound();
            var idexit = await _mongoServisescs.UpdateProduct(udatepdt);
           // await _mongoServisescs.UpdateProduct( udatepdt);
            return Ok(idexit);

        }
      
        
       

    }
}
