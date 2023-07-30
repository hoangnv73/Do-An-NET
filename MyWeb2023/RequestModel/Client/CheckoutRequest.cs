namespace MyWeb2023.RequestModel.Client
{
    public class CheckoutRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string? Note { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CouponCode { get; set; }
        public string Payment { get; set; } = "cod";
        public List<CartItem> CartItems { get; set; } = new List<CartItem> { };
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
