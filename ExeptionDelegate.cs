using System ;

namespace ExeptionDelegate{
    class Exeptions{

        public static void MethodAB(string mesg){
            Console.WriteLine(mesg);
        }
        public static void Try(){
            System.Console.WriteLine("This is Expetion Delegate");
            try{
                var dividor = 0 ;
                var result = 10 / dividor ;
            }
            catch{
                System.Console.WriteLine("Exception Occured");
            }
            finally{
                System.Console.WriteLine("Re-Enter the Different Number");
            }
        }
    }

    class Delegate{
        public delegate void  MyDelegate();

        public static void Print(){
            System.Console.WriteLine("hello navaneethan");
        }

        public static void Mainn(string parameter)
        {
            MyDelegate dele = delegate( ){
                System.Console.WriteLine("hello");
            };

            dele();
        }
    }
}