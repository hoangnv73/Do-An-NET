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
        public string Description { get; set; } = string.Empty; 
        public string DisplayLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string? Image { get; set; } 
        public bool IsActive { get; set; }
        public int Position { get; set; }

        public Banner(string title, string description, string displaylink, string link, string image,
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

    }

}
    
