using Confluent.Kafka;
using System.Diagnostics;

namespace FeedbackService.Services
{
    public class ApacheKafkaConsumer
    {
        public class ApacheKafkaConsumerService : IHostedService
        {
            private readonly string topic = "feedback";
            private readonly string groupId = "feedback_group";
            private readonly string bootstrapServers = "localhost:9092";

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

                        try
                        {
                            while (true)
                            {

                                var consumer = consumerBuilder.Consume
                                   (cancelToken.Token);
                                var test = (consumer.Message.Value);
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
}
