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
		public string Name { get; set; }
		public int Rating { get; set; }
		public string Comment { get; set; }
		public DateTime PostDate { get; set; }

		public Review(string name, int rating, string comment, DateTime postdate)
		{
			Name = name;
			Rating = rating;
			Comment = comment;
			PostDate = postdate;
	}
		public Review()
		{

		}
	}
	
}
