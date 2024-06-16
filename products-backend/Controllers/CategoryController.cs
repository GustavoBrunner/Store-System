using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using products_backend.Models;
using products_backend.Models.Context;

namespace products_backend.Controllers
{
    [ApiController]
    [Route("/product-api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public CategoryController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategoryAsync(){
            return await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryByIdAsync(string id){
            try{
                Console.WriteLine(id);
                if(string.IsNullOrEmpty(id)){
                    return NotFound("Category not informed!");
                }
                if(!_context.Categories.Any(c => c.Id == id)){
                    return NotFound("Category not found!");
                }
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if(category == null){
                    return NotFound("Category does not exists!");
                }
                return Ok(category);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> CreateCategoryAsync(CategoryModel model){
            try	{
                ModelState.Remove(nameof(CategoryModel.Id));
                if(!ModelState.IsValid){
                    return BadRequest("Invalid data informed");
                }
                if(_context.Categories.Any(c => c.Name == model.Name)){
                    return BadRequest($"Alreasy existis a category named: {model.Name}");
                }
                if(_context.Categories.Any(c => c.Id == model.Id)){
                    return BadRequest("Category alreasy exists");
                }
                
                await _context.Categories.AddAsync(model);
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest($"Was not possible to add category: {model.Name}");
                }
                
                return Ok(model);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpPut]
        public async Task<ActionResult> EditCategoryAsync(CategoryModel model){
            try{
                if(!ModelState.IsValid){
                    return BadRequest("Invalid data!");
                }
                if(!_context.Categories.Any(_c => _c.Id == model.Id)){
                    return NotFound("Category does not exists!");
                }
                _context.Entry(model).State = EntityState.Modified;
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest("Not possible to add the category to the server!");
                }
                return Ok($"Category {model.Name} edited!");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(string id){
            try{
                if(string.IsNullOrEmpty(id)){
                    return NotFound("Category not informed");
                }
                if(!_context.Categories.Any(c => c.Id == id)){
                    return NotFound("Category not found");
                }
                var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id); 
                _context.Categories.Remove(category);
                if(!(await _context.SaveChangesAsync()> 0)){
                    return BadRequest("Not possible to delete category");
                }
                return Ok($"Category {category.Name} deleted");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    
    
    
    
    }
}