using Confluent.Kafka;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KafkaPersistence.Serializers
{
    public class MessageDTOSerializer : ISerializer<MessageDTO>
    {
        public byte[] Serialize(MessageDTO data, SerializationContext context)
        { 

            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}
