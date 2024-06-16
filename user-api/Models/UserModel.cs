using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace user_api.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Surname { get; set; } = string.Empty;

        [NotMapped]
        public string FullName { get => $"{Name} {Surname}"; }

        [Precision(12,2)]
        public decimal Money { get; set; }

        public DateTime Birth { get; set; }

        public int Age { get => (int)Math.Floor((DateTime.Now - Birth).TotalDays / 365.25); }

        public ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();

        public string BasketId { get; set; } = string.Empty;

        

        
    }
}