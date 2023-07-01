namespace MyWeb.Infrastructure.Client
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
