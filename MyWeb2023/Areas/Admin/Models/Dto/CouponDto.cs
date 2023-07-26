namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class CouponDto
    {
        public int Id { get; set; } 
        public string CouponCode { get; set; }
        public double Value { get; set; }
        public int TypeId { get; set; }
        public int Quantity { get; set; }
        public DateTime Expired { get; set; }
    }
}
