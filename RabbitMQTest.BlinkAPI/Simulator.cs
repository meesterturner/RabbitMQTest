using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQTest.QueueConfig;

namespace RabbitMQTest.BlinkAPI
{
    class Simulator
    {
        bool bored = false;
        Random random = new Random();

        public void Run()
        {
            long appNumber = 10123456;

            Console.WriteLine("Simulator started...");

            while (!bored)
            {
                WaitForRandomTime();

                if (!bored)
                {
                    AppRecieved(appNumber);
                    appNumber += random.Next(1, 5);
                }
            }
        }

        private void WaitForRandomTime()
        {
            int secs = random.Next(1, 7);
            DateTime expiry = DateTime.Now.AddSeconds(secs);

            while(DateTime.Now < expiry)
            {
                Thread.Sleep(2);
                if (Console.KeyAvailable == false)
                {
                    // Do Nothing
                }
                else
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        bored = true;
                        return;
                    }
                }
            }

        }

        private void AppRecieved(long appNumber)
        {
            Configuration cfg = new Configuration();
            using (IModel channel = cfg.Queue())
            {
                byte[] body = Encoding.UTF8.GetBytes(appNumber.ToString());
                
                IBasicProperties props = channel.CreateBasicProperties();
                props.Persistent = cfg.PersistentQueue;

                channel.BasicPublish(exchange: "",
                                 routingKey: "eseries.search",
                                 basicProperties: props,
                                 body: body);
            }

            Console.WriteLine($"Application {appNumber} submitted");
        }
    }
}
