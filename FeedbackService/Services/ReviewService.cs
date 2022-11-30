using Common.Dto;
using FeedbackService.Models;
using FeedbackService.Repository;


namespace FeedbackService.Services
{
    public interface IReviewService
    {
        Task<bool> SaveReview(ReviewDto reviewDto);
    }

    public class ReviewService : IReviewService
    {
        private readonly IReviewStorage _reviewStorage;

        public async Task<bool> SaveReview(ReviewDto reviewDto)
        {
            return await _reviewStorage.CreateReview(new Review
            {
                RestaurantId = reviewDto.RestaurantId,
                DeliveryId = reviewDto.DeliveryId,
                Message = reviewDto.Message,
                Rating = reviewDto.Rating
            });
        }
    }
}
