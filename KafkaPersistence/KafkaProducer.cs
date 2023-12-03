using Confluent.Kafka;
using Domain.Models.DTO;
using KafkaPersistence.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPersistence
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, MessageDTO> _producer;
        private readonly string _topic;

        public KafkaProducer(IProducer<string, MessageDTO> producer, string topic)
        {
            _producer = producer;
            _topic = topic;
        }

        public void ProduceMessage(MessageDTO message)
        {
            var result = _producer.ProduceAsync(_topic, new Message<string, MessageDTO> { Key = Guid.NewGuid().ToString(), Value = message }).Result;

        }


    }
}
