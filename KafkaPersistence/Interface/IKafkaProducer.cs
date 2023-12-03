using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPersistence.Interface
{
    public interface IKafkaProducer
    {
        public void ProduceMessage(MessageDTO message);
    }
}
