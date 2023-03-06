using System;
using System.Runtime.InteropServices;
using Confluent.Kafka;
using KafkaProducerFirst;
using Newtonsoft.Json;

namespace KafkaProducerFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var topicName = "topic_0";
            var kafkaBootStrapServers = "pkc-lzvrd.us-west4.gcp.confluent.cloud:9092";
            var username = "B2GUYKSUAYO56XE2";
            var password = "dqBOlwAkb+/nkzLOGDkk/hTQsERKWCNbFrtP7P/hgBYFmvhzg7WjCnmFZvga5wPm";

            var kafkaConfig = new ProducerConfig()
            {
                BootstrapServers = kafkaBootStrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = username,
                SaslPassword = password
            };

            using var producer = new ProducerBuilder<string, string>(kafkaConfig).Build();

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "firstTest"
            };

            var jstr = JsonConvert.SerializeObject(product);

            var kafkaMessage = new Message<string, string>
            {
                Key = product.Id.ToString(),
                Value = jstr
            };

            var result =  producer.ProduceAsync(topicName, kafkaMessage).GetAwaiter().GetResult();

            Console.WriteLine($"Status: {result.Status}");
    


        }

    }
}