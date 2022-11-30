using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserFeedbackController : ControllerBase
    {

        private readonly IKafkaProducerService _kafkaProducerService;

        public UserFeedbackController(IKafkaProducerService kafkaProducerService)
        {
            _kafkaProducerService = kafkaProducerService;
        }

        [Authorize]
        [HttpPost(Name = "feedback")]
        public async Task<bool> CreateFeedback([FromBody] ReviewDto reviewDto)
        {
            return await _kafkaProducerService.SendReviewRequest("create_review", JsonConvert.SerializeObject(reviewDto));
        }
    }
}