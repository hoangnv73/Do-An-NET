namespace Myweb.Domain.Models.Entities
{
    public class Cake : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
