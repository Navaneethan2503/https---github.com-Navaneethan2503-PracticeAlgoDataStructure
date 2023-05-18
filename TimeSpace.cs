
using System;
using System.Diagnostics;
using System.Threading;

namespace TimeSpace{
    public class Example
    {
        public static void Mainv()
        {
            Stopwatch stopwatch = new Stopwatch();
    
            stopwatch.Start();
            Thread.Sleep(5);
            stopwatch.Stop();
            var second = stopwatch.ElapsedMilliseconds * 0.001 ;
            Console.WriteLine("Elapsed Time is {0} in second", second);
            
            decimal myDecimal = 0.5m ;
            var d = string.Format("{0:N6}", myDecimal);
            System.Console.WriteLine(d);
        }
    }
} 
