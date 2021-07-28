using GameInfoService.Rating.Domain.Models.DTOs;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameInfoService.Rating.Services.ModuleCommunication
{
    public class RatingUpdatedQueueSender : IRatingUpdatedQueueSender
    {
        public string SendUpdateRatingQueue(UpdateRatingDto updateRatingInfo)
        {
            ConnectionFactory factory = new ConnectionFactory();
            var updateRatingInfoSerialized = JsonSerializer.Serialize(updateRatingInfo);
            var messageBody = Encoding.UTF8.GetBytes(updateRatingInfoSerialized);
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "UpdateRatingQueue", false, false, false, null);
            channel.BasicPublish(exchange: "", routingKey: "UpdateRatingQueue", basicProperties: null, body: messageBody);
            return "Sent";
        }
    }
}
