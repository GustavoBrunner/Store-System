using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using user_api.Extensions;
using user_api.Models;
using user_api.Models.Context;

namespace user_api.Controllers
{
    [ApiController]
    [Route("user-api/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsersAsync(){
            return await _context.Users
                .Include(u => u.Addresses)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetByIdAsync(string id){
            try{
                if(string.IsNullOrEmpty(id)){
                    return BadRequest("User not informed!");
                }
                if(!this.CheckIfUserExists(_context.Users, id)){
                    return NotFound("User does not exists");
                }
                var user = await _context.Users
                    .Include(u => u.Addresses)
                    .FirstOrDefaultAsync(u => u.Id == id);
                return Ok(user);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUserAsync(UserModel user){
            try{
                ModelState.Remove(nameof(BaseModel.Id));
                if(!ModelState.IsValid){
                    return BadRequest("Invalid data!");
                }
                if(this.CheckIfUserExists(_context.Users, user.Id)){
                    return BadRequest("User already exists!");
                }
                await _context.Users.AddAsync(user);
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest("Was not possible to add new user!");
                }
                return Ok(user);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUserAsync(UserModel user){
            try {
                if(!ModelState.IsValid){
                    return BadRequest("Invalid date!");
                }
                if(!this.CheckIfUserExists(_context.Users, user.Id)){
                    return NotFound("User does not exist!");
                }
                _context.Entry(user).State = EntityState.Modified;
                if(!(await _context.SaveChangesAsync() > 0)){
                    return BadRequest("Was not possible to update user!");
                }
                return Ok(user);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUserAsync(string id){
            try{
                if(string.IsNullOrEmpty(id)){
                    return NotFound("User not informed!");
                }
                var user = await _context.Users.FindAsync(id);
                if(user is null){
                    return BadRequest("User not found!");
                }
                _context.Users.Remove(user);
                if(!(await _context.SaveChangesAsync()>0)){
                    return BadRequest("Was not possível to remove user!");
                }
                return Ok(user);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }




    }
}