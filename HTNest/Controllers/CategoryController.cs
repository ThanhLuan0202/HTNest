using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Model.Category;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;



        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;

        }



        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll(string categoryName = null)
        {
            return Ok(await _categoryService.GetAllAsync(categoryName));
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryModel createCategoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var category = _mapper.Map<Category>(createCategoryModel);
                if (category == null)
                {
                    return BadRequest("Invalid category data");
                }
                category = await _categoryService.AddAsync(category);

                return Ok(_mapper.Map<CategoryViewModel>(category));
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

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Client, Manager, Admin")]
        public async Task<ActionResult<Category>> Update(int id, [FromBody] UpdateCategoryModel UpdateCategoryModel)
        {
            var UpdateCategory = _mapper.Map<Category>(UpdateCategoryModel);
            if (id == null)
            {
                return NotFound();

            }
            if (UpdateCategory == null)
            {
                return BadRequest("Invalid category data");

            }

            var categoryUpdate = await _categoryService.UpdateAsync(UpdateCategory, id);
             
            return Ok(_mapper.Map<CategoryViewModel>(UpdateCategory));

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var cate = await _categoryService.GetByIdAsync(id);

            if (cate == null)
            {
                return NotFound();
            }

            return Ok(await _categoryService.Delete(id));

        }
    }
}
