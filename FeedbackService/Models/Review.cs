namespace FeedbackService.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryId { get; set; }
        public string Message { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
    }
}
