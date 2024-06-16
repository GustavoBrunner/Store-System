using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using products_backend.Models;
using products_backend.Models.Context;

namespace products_backend.Controllers
{

    [ApiController]
    [Route("product-api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreDbContext _productDb;

        public ProductController(StoreDbContext productDb)
        {
            _productDb = productDb;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllProductsAsync()
        {
            try
            {
                var Products = await _productDb.Products
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(Products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateProductAsync(ProductModel model)
        {
            ModelState.Remove(nameof(ProductModel.Id));
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest($"Errors found: {ModelState.ErrorCount}");
                }
                if (_productDb.Products.Any(p => p.Id == model.Id))
                {
                    return BadRequest("Product already exists!");
                }
                //find the category
                var category = await _productDb.Categories.FindAsync(model.CategoryId);
                if (category is not null)
                {
                    model.Category = category;
                    //if not null, add it to the product list of the category
                    category?.Products?.Add(model);
                    //update the category entity
                    _productDb.Entry(category).State = EntityState.Modified;
                }

                //add model to the products
                await _productDb.Products.AddAsync(model);
                if (await _productDb.SaveChangesAsync() > 0)
                {
                    return Ok(model);
                }
                else
                {
                    return BadRequest("Not possible to add product!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Not possible to add product!");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Product not informed!");
                }
                var product = await _productDb.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (product is not null)
                {
                    return Ok(product);
                }
                return NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {


            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Product not informed");
                }
                var product = _productDb.Products.FirstOrDefault(x => x.Id == id);
                if (product is not null)
                {
                    _productDb.Products.Remove(product);
                    if (await _productDb.SaveChangesAsync() > 0)
                    {
                        return Ok($"{product.Name} deleted");
                    }
                    else
                    {
                        return BadRequest($"Not possible to delete {product.Name}!");
                    }
                }
                else
                {
                    return NotFound("Product not found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    return BadRequest("Product not informed!");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Incorrect data!");
                }
                _productDb.Entry(model).State = EntityState.Modified;
                return Ok(await _productDb.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}