using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ReviewDto
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryId { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
    }
}
