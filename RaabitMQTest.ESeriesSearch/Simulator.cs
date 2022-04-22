using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQTest.ESeriesSearch
{
    class Simulator
    {
        const int instances = 3;
        public void Run()
        {
            for(int i = 0; i < instances; i++)
            {
                SearchThread st = new SearchThread(i);
                Thread t = new Thread(new ThreadStart(st.Start));
                t.Start();

            }

        }  
    }
}
