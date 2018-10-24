using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThread thread = new MyThread();
            thread.processorCount(); // 1
            thread.threadStart();  // 2
            thread.maxPower(); //3 sleep z wywolaniem max dostepnej ilosci watkow i informacja o zakonczeniu
            Console.ReadKey(true);
        }
    }


    class MyThread
    {
        public void processorCount()
        {
            Console.WriteLine("processor pool: {0}", Environment.ProcessorCount);
        }

        public void threadTask()
        {
            int i = 0;
            while (i < 1000)
            {
                i++;
                Console.WriteLine(i);
            }
        }

        public void maxPower()
        {
            int threadCount = Environment.ProcessorCount;
            WaitHandle[] waitHandles = new WaitHandle[threadCount];

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int j = i;
                var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                var thread = new Thread(() =>
                {
                    Thread.Sleep(j * 1000);
                    Console.WriteLine("Thread{0} exits", j+1); 
                    handle.Set();
                });
                waitHandles[j] = handle;
                thread.Start();
            }
        }

        public void threadStart()
        {
            Thread thread = new Thread(this.threadTask);
            thread.Start();
            int j = 0;
            while (j < 10)
            {
                j++;
                Console.WriteLine("main thread");
            }
            thread.Join();
            Console.WriteLine("ready!");
        }
    }
}

