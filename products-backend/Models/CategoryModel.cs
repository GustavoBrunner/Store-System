using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace products_backend.Models
{
    [Table("Category")]
    public class CategoryModel: BaseModel
    {
        public ICollection<ProductModel>? Products { get; set; }
    }
}