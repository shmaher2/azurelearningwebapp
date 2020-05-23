using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisualStudioWebJob
{
    public class Functions
    {
        public static void ProcessQueue([QueueTrigger("test")] Order order, ILogger logger)
        {
            Console.WriteLine($"Received the message in the Queue {order.OrderId} and {order.ResturantName}");
        }

        public static void TimerTrigger([TimerTrigger("0 */2 * * * *", RunOnStartup =true)]TimerInfo timerInfo, ILogger logger)
        {
            logger.LogInformation($"My Trigger triggered At {DateTime.Now.ToString()}");
        }

    }

    public class Order
    {
        public string OrderId { get; set; }
        public string ResturantName { get; set; }
    }
}



