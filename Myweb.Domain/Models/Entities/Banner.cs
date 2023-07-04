using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Models.Entities
{
    public class Banner : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DisplayLink { get; set; } = string.Empty;
        public string? Link { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Position { get; set; }


        public void Update(string title, string description, string displaylink,string link, string? image,
            bool isactive, int position)
        {
            Title = title;
            Description = description;
            DisplayLink = displaylink;
            Link = link;
            Image = image;
            IsActive = isactive;
            Position = position;
        }

        public Banner()
        {

        }

        public Banner(string title, string? description, string? displayLink, string? link, 
            string image, int position)
        {
            Title = title;
            Description = description;
            DisplayLink = displayLink;
            Link = link;
            Image = image;
            IsActive = true;
            Position = position;
        }
    }

}
    
