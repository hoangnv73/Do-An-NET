using System.Diagnostics;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Brand : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public void Update(string name)
        {
            Name = name;
        }
        public Brand(string name)
        {
            Name = name;

        }
        public Brand()
        {
            
        }
    }


}
