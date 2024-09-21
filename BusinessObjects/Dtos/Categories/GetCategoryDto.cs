using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Categories
{
    public class GetCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
