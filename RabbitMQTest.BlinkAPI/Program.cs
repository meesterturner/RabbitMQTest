using System;
using System.Threading;
namespace RabbitMQTest.BlinkAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Blink/RabbitMQ Simulator");
            Console.WriteLine("========================");
            Console.WriteLine("");
            Console.WriteLine("This program will simulate applications arriving via Blink");
            Console.WriteLine("and trigger the searches asynchronously via RabbitMQ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            Simulator s = new Simulator();
            s.Run();

            Console.WriteLine("");
            Console.WriteLine("**** ENDED ****");
            Thread.Sleep(3000);
        }
    }
}
