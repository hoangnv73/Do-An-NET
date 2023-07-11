using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public Review(string name, int rating, string comment, DateTime createDate, int productId)
        {
            Name = name;
            Rating = rating;
            Comment = comment;
            CreateDate = createDate;
        }
        public Review()
        {
           
        }
    }

}
