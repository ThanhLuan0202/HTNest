using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Model.Product;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetAll(string? nameProduct)
        {
            return Ok(await _service.GetAll(nameProduct));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Admin, Client")]

        public async Task<ActionResult<Product>> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        // POST api/<ProductController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Manager, Admin")]

        public async Task<ActionResult<Product>> Add([FromForm] CreateProductModel createProductModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               

                var result = await _service.Add(createProductModel);
                return Ok(_mapper.Map<ProductViewModel>(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Manager, Admin")]

        public async Task<ActionResult<Product>> Update(int id, [FromForm] UpdateProductModel updateProductModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var productUpdate = await _service.Update(id, updateProductModel);

            return Ok(_mapper.Map<ProductViewModel>(productUpdate));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]

        public async Task<ActionResult<Product>> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid product data");
            }

            var productDelete = await _service.DeleteById(id);

            return Ok(productDelete);
           


        }
    }
}
