using System.IO;
using System.Collections;
using System;

namespace Sorting{
    /*
    1.Bubble Sort - O(n^2)
    2.Selection Sort - O(n^2)
    3.intersion Sort - O(n^2)
    4.Quick Sort - O(n^2) - O(n log n)
    5.Merge Sort - O(n log n)
    6.Counting Sort - O(n + k)
    7.heap sort - O(n log n)
    8.radix sort - O(n+k)
    9.bucket sort - O(n + n log(n/m))
    */

    class Program{

        public static List<int> bubbleSort(List<int> arr){
            for(int i = 0; i<arr.Count; i++){
                for(int j = 0; j<arr.Count-1; j++){
                    if(arr[j]>arr[j+1]){
                        int temp = arr[j];
                        arr[j] = arr[j+1];
                        arr[j+1] = temp; 
                    }
                }
            }
            return arr;
        }

        public static List<int> insertion(List<int> a){
        int n = a.Count;
        for(int i = 0; i<a.Count; i++){
            int j = i;
            while(j>0 && a[j-1]>a[j]){
                int temp = a[j-1];
                a[j-1] = a[j];
                a[j] = temp;
                j--;
            }
        }
        return a;
    }


    //Lomuto Partition of quicksort
    static int partition(List<int> arr, int left, int right){
        int pivot = arr[right];
        int j = left;
        for(int i = left; i<arr.Count; i++){
            if(arr[i]<pivot){
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                j++;
            }
        }
        int temp1 = arr[j];
        arr[j] = arr[right];
        arr[right] = temp1;
        for(int i = 0; i<arr.Count; i++){
            Console.Write(arr[i]+" ");
        }
        Console.WriteLine();
        return j;
    }
    static void quickSort(List<int> arr, int left, int right){
        if(left<right){
            int pivotIndex = partition(arr,left,right);
            quickSort(arr,left,pivotIndex-1);
            quickSort(arr,pivotIndex+1,right);
        }
    }

    //Hoare Partition
    static void quicksort1(List<int> arr, int left, int right){
        int l = left;
        int r = right-1;
        int size = right-left;
        if(size>1){
            Random rand = new Random();
            int pivot = arr[rand.Next(arr.Count)];
            //int pivot = arr[r];
            while(l<r){
                while(arr[r]>pivot && r>l) r--;
                while(arr[l]<pivot && l<=r) l++;
                if(l<r){
                    int temp = arr[l];
                    arr[l] = arr[r];
                    arr[r] = temp;
                    l++;
                }
            }
            quicksort1(arr, left, l);
            quicksort1(arr, r,right);
        }
    }

    static void merge(List<int> arr, int left, int mid, int right){
        int n1 = mid-left+1;
        int n2 = right-mid;
        int[] l = new int[n1];
        int[] r = new int[n2];
        for(int i = 0; i<n1; i++){
            l[i] = arr[left+i];
        }
        for(int j = 0; j<n2; j++){
            r[j] = arr[mid+j+1];
        }
        int x = 0, y = 0;
        int k = left;
        while(x<n1 && y<n2){
            if(l[x]<=r[y]){
                arr[k] = l[x];
                x++;
            }
            else{
                arr[k] = r[y];
                y++;
            }
            k++;
        }
        while(x<n1){
            arr[k] = l[x];
            x++;
            k++;
        }
        while(y<n2){
            arr[k] = r[y];
            y++;
            k++;
        }
    }

    static void mergesort(List<int> arr, int left, int right){
        if(right>left){
            int mid = left+(right-left)/2;
            mergesort(arr,left,mid);
            mergesort(arr,mid+1,right);
            //System.Console.WriteLine(left+" "+right+" "+mid);
            merge(arr,left,mid,right);
        }
    }

    static void selectionSort(List<int> arr){
        for(int i = 0; i<arr.Count; i++){
            int min = i;
            for(int j = i+1; j<arr.Count; j++){
                if(arr[j] < arr[min]){
                    min = j;
                }
            }
            int temp = arr[i];
            arr[i] = arr[min];
            arr[min] = temp;
        }
    }

    static void CountingSort(List<int> arr){
        int[] counts = new int[arr.Max()+1];
        for(int i = 0 ; i<arr.Count; i++){
            counts[arr[i]]++;
        }
        int index = 0;
        for(int i = 0; i<counts.Length; i++){
            if(counts[i]>0){
                for(int j = 0 ; j<counts[i]; j++){
                    arr[index] = i;
                    index++;
                }
            }
        }
    }

    //heap sort
    static void heapsort(List<int> arr){
        //build max heap
        build_heap(arr);

        //swap the largest to last element till first element
        for(int i = arr.Count-1; i>=1; i--){
            int temp = arr[i];
            arr[i] = arr[0];
            arr[0] = temp;
            //build max heap after the swapping is done 
            //to do so all largest element will come to last and we reducing the index to first
            heapify(arr, 0, i);
        }

    }

    //this method is used to build max heap 
    static void build_heap(List<int> arr){
        for(int j = arr.Count/2 -1; j>=0; j--){
            heapify(arr,j,arr.Count);
        }
    }


    //this method checks the child nodes are smaller than parent node and if child node is greater than swaps and again check for heapify
    static void heapify(List<int> arr, int i , int size){
        int largest = i;
        int left = 2*i+1;
        int right = 2*i+2;

        if(left < size && arr[left]>arr[largest]) largest = left;
        if(right < size && arr[right]>arr[largest]) largest = right;
        if(largest != i){
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;
            heapify(arr,largest,size); 
        }
    }

    //radix sort -O(n+k)
    static void radixsort(List<int> arr){
        int max = arr.Max();
        for(int terms = 1; max/terms>0; terms*=10){
            radixcounting(arr,terms);
        }
    }
    static void radixcounting(List<int> arr, int terms){
        int[] res = new int[arr.Count];
        int[] counts = new int[10];
        for(int i = 0; i<arr.Count; i++){
            counts[(arr[i]/terms)%10]++;
        } 
        for(int i = 1; i<counts.Length; i++){
            counts[i] += counts[i-1];
        }
        for(int i = arr.Count - 1; i>=0; i--){
            res[counts[(arr[i]/terms)%10]-1] = arr[i];
            counts[(arr[i]/terms)%10]--;
        }
        for(int i = 0; i<arr.Count; i++){
            arr[i] = res[i];
        }
    }

    // bucket sort - O(n + n log(n/m))
    static void bucketsort(List<float> arr){
        int n = arr.Count;
        List<float>[] bucket = new List<float>[n];
        for(int i = 0; i<n; i++){
            bucket[i] = new List<float>();
        }
        for(int i = 0; i<n; i++){
            float indx = n*arr[i];
            bucket[(int)indx].Add(arr[i]);
        }
        int index = 0;
        for(int i = 0; i<n; i++){
            for(int j = 0; j<bucket[i].Count; j++){
                arr[index++] = bucket[i][j];
            }
        }
    }

    
    public static void Mainn(string[] args){
            System.Console.WriteLine("Insertion sort");
            List<int> array = new List<int>(){12,11,7,14,5,6};
            List<float> arr = new List<float>(){(float)0.1234, (float) 0.3434 , (float)0.565, (float) 0.656 , (float)0.665 ,(float)0.897 };
            bucketsort(arr);
            arr.ForEach(t => System.Console.Write(t+" "));
        }
    }
}