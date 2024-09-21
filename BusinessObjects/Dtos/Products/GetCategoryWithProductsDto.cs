using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Products
{
    public class GetCategoryWithProductsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
