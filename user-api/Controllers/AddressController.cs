using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_api.Models;
using user_api.Models.Context;

namespace user_api.Controllers
{
    [ApiController]
    [Route("user-api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly UserDbContext _context;

        public AddressController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(string id){
            try{
                if(string.IsNullOrEmpty(id)){
                    return BadRequest("Object not informed");
                }
                if(!await _context.Addresses.AnyAsync(a => a.Id == id)){
                    return NotFound("Object does not exists!");
                }
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.Id == id);
                return Ok(address);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/user/{id}")]
        public async Task<ActionResult<IEnumerable<AddressModel>>> GetByUserIdAsync(string id){
            try {
                if(string.IsNullOrEmpty(id)){
                    return BadRequest("User not informed!");
                }
                if(!await _context.Addresses.AnyAsync(a => a.UserId == id)){
                    return NotFound("User not found!");
                }
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserId == id);
                return Ok(address);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressModel>> CreateAddressAsync(AddressModel addressModel){
            try {
                if(!ModelState.IsValid){
                    return BadRequest("Invalid data!");
                }
                if(await _context.Addresses.AnyAsync(a => a.Id == addressModel.Id)){
                    return BadRequest("Address already exists");
                }
                await _context.Addresses.AddAsync(addressModel);
                if(!(await _context.SaveChangesAsync()> 0)){
                    return BadRequest("Was not possible to add address");
                }
                var user = _context.Users.FirstOrDefault(u => u.Id == addressModel.UserId);
                if(user is null){
                    return BadRequest("");
                }
                user.Addresses.Add(addressModel);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(addressModel);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<AddressModel>> DeleteByIdAsync(string id){
            try{
                if(string.IsNullOrEmpty(id)){
                    return BadRequest("Address not informed!");
                }
                if(!await _context.Addresses.AnyAsync(a => a.Id == id)){
                    return NotFound("Address not found!");
                }
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.Id == id);
                _context.Addresses.Remove(address);
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest("Not possible to delete address!");
                }
                return Ok(address);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<AddressModel>> UpdateAddressAsync(AddressModel addressModel){
            try{
                if(!ModelState.IsValid){
                    return BadRequest("Invalid data!");
                }
                if(!await _context.Addresses.AnyAsync(a => a.Id==addressModel.Id)){
                    return NotFound("Addres does not exist!");
                }
                _context.Entry(addressModel).State = EntityState.Modified;
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest("Was not possible update address");
                }
                return Ok(addressModel);    
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}