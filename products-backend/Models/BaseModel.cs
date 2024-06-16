using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace products_backend.Models
{
    
    public class BaseModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}