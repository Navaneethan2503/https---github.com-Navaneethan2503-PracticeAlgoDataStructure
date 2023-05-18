namespace SearchingAlgorithm
{
    class SearchingAlgo
    {
        //There sre two Types of Seach Algorithm Are 1. Sequencial Search , 2. Interval Search
        /*
        1. Linear Search- O(n) - we can use transposition, move to front , hastable to reduce complexity for same key again and again
        2.Binary Search - O(log n)
        3.Ternary Search - O(log n base 3) - but it is more efficent than binary search . here we have two mids and three seperations
        4. Jump Search - O(Square root of n)
        5.Interpolation Search - O(n) - improvement of binary search
        6. Exponential Search - O(log n) - which can be used for larger array in size .
        */

        public static int linearSearch(List<int> arr, int target)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] == target)
                    return i;
            }
            return -1;
        }

        public static int binarySearch(List<int> arr, int target, int left, int right)
        {
            if (right > left)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == target)
                    return mid;
                if (target < arr[mid])
                    return binarySearch(arr, target, left, mid - 1);
                else if (target > arr[mid])
                    return binarySearch(arr, target, mid + 1, right);
            }
            return -1;
        }

        public static int ternarySearch(List<int> arr, int target, int left, int right)
        {
            if (right > left)
            {
                int mid1 = left + (right - left) / 3;
                int mid2 = right - (right - left) / 3;
                if (arr[mid1] == target)
                    return mid1;
                else if (arr[mid2] == target)
                    return mid2;
                else if (target < arr[mid1])
                    return ternarySearch(arr, target, left, mid1 - 1); // 1st part
                else if (target > arr[mid2])
                    return ternarySearch(arr, target, mid2 + 1, right); //3nd part
                else
                {
                    return ternarySearch(arr, target, mid1 + 1, mid2 - 1); //2nd part
                }
            }
            return -1;
        }

        public static int jumpSearch(List<int> arr, int target)
        {
            int jump = (int)Math.Sqrt((double)arr.Count);
            int m = 0;
            for (
                int minStep = Math.Min(jump, arr.Count);
                arr[minStep] < target;
                minStep = Math.Min(jump, arr.Count) - 1
            )
            {
                m = jump;
                jump += (int)Math.Sqrt(arr.Count);
                if (m >= arr.Count)
                    return -1;
            }
            while (arr[m] < target)
            {
                m++;

                // If we reached next block or end of
                // array, element is not present.
                if (m == Math.Min(m, arr.Count))
                    return -1;
            }

            // If element is found
            if (arr[m] == target)
                return m;
            return -1;
        }

        public static int interpolation(List<int> arr, int lo, int hi, int x){
            int pos ;
            if(lo<=hi && x>=arr[lo] && x<=arr[hi]){
                pos = lo + (((hi - lo) /
                (arr[hi] - arr[lo])) *
                      (x - arr[lo]));
                if(arr[pos] == x) return pos;
                else if(arr[pos]<x) return interpolation(arr,pos+1,hi,x);
                else if(arr[pos]>x) return interpolation(arr,lo,pos-1,x);
            }
            return -1;
        }

        public static int exponentialSearch(List<int> arr, int lo, int hi, int x){
            if(arr[0]==x) return 0;
            int i = 1;
            while(i<arr.Count && arr[i]<=x){
                i*=2;
            }
            return binarySearch(arr,x,i/2,Math.Min(arr.Count-1,i));

        }

        


        public static void Mainn(string[] arg)
        {
            System.Console.WriteLine("Hello Lets Fire The Searching Algo");
            List<int> arr = new List<int>() { 10, 50, 30, 70, 80, 20, 90, 40, 15, 25, 35, 45, 55 };
            arr.Sort();
            var res = exponentialSearch(arr,0,arr.Count-1,25);
            System.Console.WriteLine(res);
        }
    }
}
