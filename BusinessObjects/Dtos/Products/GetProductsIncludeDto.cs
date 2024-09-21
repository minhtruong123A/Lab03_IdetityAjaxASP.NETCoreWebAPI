using BusinessObjects.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Products
{
    public class GetProductsIncludeDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public GetCategoryDto? Category { get; set; }
    }
}
