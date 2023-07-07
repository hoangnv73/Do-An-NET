namespace MyWeb2023.Areas.Admin.Models
{
    public class CreateUserModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public IFormFile? File { get; set; }
        public int? RoleId { get; set; }

    }
}
