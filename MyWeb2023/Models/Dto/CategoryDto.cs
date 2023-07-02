namespace MyWeb2023.Models.Dto
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string? Image { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}
}
