using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQTest.QueueConfig;

namespace RabbitMQTest.ESeriesSearch
{
    class SearchThread
    {
        bool bored = false;
        ConsoleColor colour;
        int id;
        Random random = new Random();

        public SearchThread(int id)
        {
            List<ConsoleColor> colours = new List<ConsoleColor>()
            {
                ConsoleColor.Yellow,
                ConsoleColor.Blue,
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Magenta
            };
            
            this.id = id;
            this.colour = colours[id];

            ColourWrite($"Thread {id} started");
        }

        public void Start()
        {
            Configuration cfg = new Configuration();
            using (IModel channel = cfg.Queue())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    bool success = DoSearch(Convert.ToInt32(message));

                    if(cfg.PersistentQueue && success)
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "eseries.search",
                                     autoAck: !cfg.PersistentQueue,
                                     consumer: consumer);

                while(!bored)
                {
                    Thread.Sleep(2);
                }
            }
        }

        private bool DoSearch(long appNumber)
        {
            try
            {

                ColourWrite($"Starting search {appNumber}");
                WaitForRandomTime();
                ColourWrite($"Finished search {appNumber}");

                return true;
            }
            catch(Exception ex)
            {
                ColourWrite($"Exception search {appNumber}");
                return false;
            }
        }

        private void WaitForRandomTime()
        {
            int secs = random.Next(1, 20);
            Thread.Sleep(secs * 1000);

        }

        private void ColourWrite(string text)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine($"{id}: {text}");
        }
    }
}
