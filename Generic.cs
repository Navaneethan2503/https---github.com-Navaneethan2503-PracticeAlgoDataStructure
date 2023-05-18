using System;
using System.Diagnostics;

namespace Generic{
    class Generic<T> {

        private T Total ;

        public T Status { get{return this.Total; } set{} }
        
        public void Calculate<K>(T data, K method){
            if(method != null)
               this.Total = data ;
        }
        
    }

    class Program{
        public static void Mainn(string[] args){
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Generic<int> nickil = new Generic<int>();
            nickil.Calculate<string>(200, "Add");

            System.Console.WriteLine(nickil.Status);
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.Elapsed);
        }
    }
}