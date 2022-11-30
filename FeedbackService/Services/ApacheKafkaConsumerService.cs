using Common.Dto;
using Confluent.Kafka;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FeedbackService.Services
{
    public class ApacheKafkaConsumerService : IHostedService
    {
        private readonly string topic = "create_review";
        private readonly string groupId = "feedback_group";
        private readonly string bootstrapServers = "localhost:9092";

        private readonly IServiceProvider _serviceProvider;


        public ApacheKafkaConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    using var scope = _serviceProvider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IReviewService>();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                            var test = consumer.Message.Value;


                            var obj = System.Text.Json.JsonSerializer.Deserialize<ReviewDto>(test);
                            Debug.WriteLine(obj.Message);

                            //service.SaveReview(obj);

                            using (SqlConnection conn = new SqlConnection())
                            {
                                conn.ConnectionString = "Server=127.0.0.1,5434;Database=Reviews;User Id=sa;Password=S3cur3P@ssW0rd!;TrustServerCertificate=True";

                                conn.Open();

                                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Reviews (UserId, RestaurantId, DeliveryId, Message, ReviewDate, Rating) VALUES (@0, @1, @2, @3, @4, @5)", conn);
                                sqlCommand.Parameters.Add(new SqlParameter("0", obj.UserId));
                                sqlCommand.Parameters.Add(new SqlParameter("1", obj.RestaurantId));
                                sqlCommand.Parameters.Add(new SqlParameter("2", obj.DeliveryId));
                                sqlCommand.Parameters.Add(new SqlParameter("3", obj.Message));
                                sqlCommand.Parameters.Add(new SqlParameter("4", DateTime.UtcNow));
                                sqlCommand.Parameters.Add(new SqlParameter("5", obj.Rating));

                                sqlCommand.ExecuteNonQuery();

                            }

                            //lav DB kald
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
