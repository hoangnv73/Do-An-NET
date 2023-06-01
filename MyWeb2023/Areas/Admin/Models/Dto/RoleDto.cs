namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class RoleDto
    {
        public RoleDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public RoleDto()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
