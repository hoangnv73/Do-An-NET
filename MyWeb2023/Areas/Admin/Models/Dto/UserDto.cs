namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ResetPassword { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

    }
}
