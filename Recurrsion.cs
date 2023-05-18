namespace Recurrsion{
    class Recursion{

        public static int sum(int num){
            if(num <= 0) return 0;
            return num + sum(num-1);
        }

        public static void hanoi(int disc, int source, int designation, int auxillary){
            if(disc == 1){
                System.Console.WriteLine("Move {0} disc from {1} to {2}",disc, source, designation);
            }
            else{
                hanoi(disc-1,source,auxillary, designation);
                System.Console.WriteLine("Move {0} disc from {1} to {2}",disc, source, designation);
                hanoi(disc-1,auxillary,designation,source);
            }
        }

        public static int fibanocii(int num){
            if(num < 0){
                throw new Exception("Num should be grether than 0");
            }
            if(num == 0) return 0;
            if(num == 1) return 1;
            
            return fibanocii(num-1)+fibanocii(num-2);
        }

        public static int powerSum(int X, int N)
    {
        int recur(int X, int N, int num){
            int power = Convert.ToInt32(Math.Pow(num,N));
            if(power == X) return 1;
            else if( power > X) return 0;
            return recur(X,N,num+1) + recur(X-power, N,num+1);
        }
        return recur(X,N,1);
    }

        public static void Mainw(string[] args){
            System.Console.WriteLine("Recursion");
            //Recursion.hanoi(4,1,2,3);
            System.Console.WriteLine(Recursion.powerSum(13,2));
        }
    }
}