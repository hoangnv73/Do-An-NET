namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; } 
        public string StatusName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Note { get; set; }
    }
}
