using Common.Dto;
using Confluent.Kafka;
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

        public async Task StartAsync(CancellationToken cancellationToken)
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

                    var scope = _serviceProvider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IReviewService>();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                            var test = (consumer.Message.Value);

                            var obj = JsonConvert.DeserializeObject<ReviewDto>(test);

                            await service.SaveReview(obj);
                            Debug.WriteLine(test);
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
