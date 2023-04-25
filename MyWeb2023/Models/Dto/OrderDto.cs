namespace MyWeb2023.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
        public double Total { get; set; }
    }
}
