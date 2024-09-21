using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Categories
{
    public class UpdateCategoryDto
    {
        [Required]
        public int CategoryId { get; set; }
        [StringLength(40)]
        public string CategoryName { get; set; } = null!;
    }
}
