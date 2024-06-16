using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace products_backend.Models
{
    [Table("Product")]
    public class ProductModel : BaseModel
    {
        public string? ProductDescription { get; set; }


        [Precision(12,2)]
        public decimal? Price { get; set; }

        public int? Stock { get; set; }

        public string? CategoryId { get; set; }

        [JsonIgnore]
        public CategoryModel? Category { get; set; }

    }
}