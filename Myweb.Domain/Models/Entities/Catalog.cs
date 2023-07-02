using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Models.Entities
{
    public class Catalog : EntityBase
    {
        public string Title { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; }

    }
}
