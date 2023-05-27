namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class ComboboxDto
    {
        public ComboboxDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public ComboboxDto()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
