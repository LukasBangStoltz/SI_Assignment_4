using FeedbackService.Context;
using FeedbackService.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Repository
{
    public interface IReviewStorage
    {
        public Task<bool> CreateReview(Review review);
        public Task<Review> GetReviewByReviewId(int reviewId);
        public Task<List<Review>> GetReviewsByUserId(int userId);
        public Task<List<Review>> GetReviewsByRestaurantId(int restaurantId);
        public Task<List<Review>> GetReviewsByDeliveryUserId(int deliveryUserId);

    }
    public class ReviewStorage : IReviewStorage
    {
        private readonly DBApplicationContext _dbContext;

        public ReviewStorage(DBApplicationContext dBApplicationContext)
        {
            _dbContext = dBApplicationContext;
        }
        public async Task<bool> CreateReview(Review review)
        {
            try
            {
                await _dbContext.AddAsync(review);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<Review> GetReviewByReviewId(int reviewId)
        {
            try
            {
                var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                return review;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Review>> GetReviewsByDeliveryUserId(int deliveryUserId)
        {
            try
            {
                var review = await _dbContext.Reviews.Where(x => x.DeliveryId == deliveryUserId).ToListAsync();
                return review;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<Review>> GetReviewsByRestaurantId(int restaurantId)
        {
            try
            {
                var review = await _dbContext.Reviews.Where(x => x.RestaurantId == restaurantId).ToListAsync();
                return review;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Review>> GetReviewsByUserId(int userId)
        {
            try
            {
                var review = await _dbContext.Reviews.Where(x => x.UserId == userId).ToListAsync();
                return review;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
