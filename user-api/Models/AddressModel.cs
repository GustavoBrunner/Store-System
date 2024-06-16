using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Models
{
    public class AddressModel : BaseModel
    {
        
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}