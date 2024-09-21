using BusinessObjects.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Categories
{
    public class GetCategoryIncludeDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public ICollection<GetCategoryWithProductsDto> Products { get; set; } = [];
    }
}
