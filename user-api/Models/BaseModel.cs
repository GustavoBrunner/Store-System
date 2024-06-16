using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Models
{
    public class BaseModel
    {
        [Key]
        public string? Id { get; set; }

        public BaseModel(){
            this.Id = Guid.NewGuid().ToString();
        }
    }
}