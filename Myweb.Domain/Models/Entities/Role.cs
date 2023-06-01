using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Models.Entities
{

    public class Role : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public void Update(string name)
        {
            Name = name;
        }
        public Role()
        {

        }
    }
}
