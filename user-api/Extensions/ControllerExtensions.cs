using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_api.Controllers;
using user_api.Models;

namespace user_api.Extensions
{
    public static class ControllerExtensions
    {
        public static bool CheckIfUserExists(this ControllerBase @this, DbSet<UserModel> set, string id ){
            return set.Any(u => u.Id == id);
        }
    }
}