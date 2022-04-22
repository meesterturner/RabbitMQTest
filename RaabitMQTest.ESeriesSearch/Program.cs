using System;

namespace RabbitMQTest.ESeriesSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Credit Search Simulator");
            Console.WriteLine("========================");
            Console.WriteLine("");
            Console.WriteLine("This program will simulate applications getting searched");
            Console.WriteLine("AFTER they are triggered by Blink. So Blink would return");
            Console.WriteLine("a Pending status");
            Console.WriteLine("");
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            Simulator s = new Simulator();
            s.Run();

            Console.WriteLine("Press [ENTER] to end");
            Console.ReadLine();
        }
    }
}
