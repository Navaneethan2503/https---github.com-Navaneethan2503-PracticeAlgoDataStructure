using System;


namespace Array{
    class OneDArray{
        public void SingleDArray(){
            var array = new int[]{1,2,3,4,5};
            var arrayAs = new int[5]{1,2,3,4,5};
            int[] array1 = {2,3,4,5,6};
            int[] second = new int[5];
            int[] first  ;
            first = new int[]{1 , 2 , 3 , 4, 5};
            int[] firstAs ;
            firstAs = new int[5]{1,2,3,4,5};
            System.Console.WriteLine("OneDArray");

            System.Array.Reverse(first);
            System.Console.WriteLine(System.Array.BinarySearch(first,5));
            System.Console.WriteLine("Sum is {0}" , first.Sum());
            foreach(var i in first)
                    System.Console.WriteLine(i);
        }

        public void multiDArray(){// upto 32 dimension supports
            System.Console.WriteLine("Multi Dimension Array");
            int[,] first = new int[,]{{1,3 , 9},{1,4, 2}, {2, 7 , 6}};
            System.Console.WriteLine(first[0,2] + first[2,2]);
            int[, ,] threeD  = new int[2,2 ,2]{
                {
                    {1,2},{3,4}
                },
                {
                    {5,6},{7,8}
                }
            };
            System.Console.WriteLine("Three dimension is " + (threeD[0,1,1] + threeD[1,0,1]));

            foreach(var i in first){
                System.Console.WriteLine(i);
            }
        }

        public void jaggedArray(){ // Array of array
            System.Console.WriteLine("Jagged Array"); 
            int[][] jArray = new int[2][]{
                new int[2]{1, 2},
                new int[3]{3,4,5}
            };
            foreach(var i in jArray)
                foreach(var j in i)
                   System.Console.WriteLine(j);

            System.Console.WriteLine("Declaration way in Array");
            int[][] jarray = new int[3][];
            jarray[0] = new int[2]{11, 12};
            jarray[1] = new int[2]{13,14};
            jarray[2] = new int[2]{15,16};

            foreach(var i in jarray)
               foreach(var j in i)
                  System.Console.WriteLine(j);

            System.Console.WriteLine("Accessing the jagged Array  " +  jarray[1][1]);

            int[][][] arrayOfarrayOfarray = new int[2][][];
            arrayOfarrayOfarray[0] = new int[2][]{
                new int[2]{0 ,1} , 
                new int[2]{2,3}
            };
            arrayOfarrayOfarray[1] = new int[2][]{
                new int[2]{11, 12},
                new int[2]{13,14}
            };

            System.Console.WriteLine("Array of Array of Array");
            foreach(var i in arrayOfarrayOfarray)
                foreach(var j in i)
                   foreach(var k in j)
                      System.Console.WriteLine(k);

        }

        static void Main4(){
            int[] a = new int[3]{1,2,3};
            foreach(int i in a){
                Console.Write(i);
            }

        }
    }   
}