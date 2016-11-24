using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDTO ParentCategory { get; set; }
        public string ProductText { get; internal set; }
    }
}
