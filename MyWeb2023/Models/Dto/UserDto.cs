namespace MyWeb2023.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Avatar { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int StatusId { get; set; }
    }
}
