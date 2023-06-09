namespace MyWeb2023.Areas.Admin.Models
{
    public class CreateCategoryModel
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
		public IFormFile? File { get; set; }

	}
}
