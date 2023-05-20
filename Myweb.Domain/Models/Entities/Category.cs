using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Category: EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public Category(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
        public Category()
        {

        }

        public void Update(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
   
   
    
}
