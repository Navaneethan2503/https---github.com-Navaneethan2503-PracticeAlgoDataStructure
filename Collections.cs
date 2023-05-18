using System.Collections;
using System;
using System.Collections.Generic;
using School;
using System.Linq;

namespace Collections{
    class AList{

        public void ArrayList(){

            var aList = new ArrayList();
            aList.Add(1);
            aList.Add("2");
            aList.Add(true);
            aList.Add("c");
            ArrayList aList2 = new ArrayList(){ "1" , 2 , false};
            aList.AddRange(aList2);
            Queue q = new Queue();
            q.Enqueue("Hello");
            q.Enqueue("World");
            aList.AddRange(q);
            Stack s = new Stack();
            s.Push("First");
            s.Push("Second");
            aList.AddRange(s);
            int[] list = new int[4]{ 2,4,6,8};
            var newArray = aList.ToArray();    
        }
        
        public void List(){
            List<int> scores = new List<int>(){100 , 95 , 84 , 99 };
            scores.Add(102);
            int[] array = new int[2]{70 , 75};
            scores.AddRange(array);
            scores.Insert(2, 103);
            scores.Remove(95);
            scores.Sort();
            scores.Reverse();
            
            System.Console.WriteLine(scores.LastIndexOf(102));
            System.Console.WriteLine(scores.BinarySearch(100));

            var Teachers = new List<School.Staff>(){
                new School.Staff(){ Name = "Navaneethan" , Amount = 1000 },
                new School.Staff(){Name = "Neha" , Amount = 2000}
            };

            var result = from s in Teachers
                         where s.Name == "Neha"
                         select s ;
            
            foreach(var i in Teachers)
                 System.Console.WriteLine(i.Name + " " + i.Amount);

            
                  
        }

        public void SortedList(){
            SortedList<int, string> s = new SortedList<int, string>();
            SortedList<string, string > s1 = new SortedList<string , string>(){
                {"Chennai" , "Tamilnadu"},
                {"Bangalore", "Karnataka"}
            };
            s.Add(1,"One");
            s.Add(5,"Five");
            s.Add(2,"Two");
            s[1] = "one to Okkati";
            s.Remove(1);
            foreach(KeyValuePair<int, string> i in s){
                System.Console.WriteLine("Keys:{0} , Values: {1}",i.Key , i.Value );
            }
            System.Console.WriteLine("This is Index 1 :{0} ",s[2]);
        }

        public void Dictionary(){
            IDictionary<int , string> dic = new Dictionary<int , string>();
            IDictionary<string , string> dic2 = new Dictionary<string , string>(){
                {"Bangalore" , "Karnataka"},
                {"Tamil nadu " , "Kaniyakumari"}
            };
            dic.Add(1 , "One");
            dic.Add(2, "Two");
            dic.Add(3,"Three");
            dic[3] = "Three to Moonu";
            dic.Add(4, "Four");
            foreach(var i in dic)
                   System.Console.WriteLine("Keys : {0} , Values : {1}" , i.Key , i.Value);
            
            Console.WriteLine(dic[3]);
            if(dic.ContainsKey(4))
                System.Console.WriteLine(dic[4]);

            dic.Remove(4);

            string result ; 
            if(dic.TryGetValue(4 , out result))
               System.Console.WriteLine(dic[4]);

            Console.WriteLine(dic.ElementAt(1).Key);
            System.Console.WriteLine("The Keys : " );
            var r = dic.Keys;
            foreach(var i in r)
                    System.Console.Write(i + ",");

            var re = dic.ToArray();
            foreach(var i in re)
                    System.Console.WriteLine(i);
            
            Console.WriteLine(dic.GetEnumerator());
        }

        public void HashTable(){
            Hashtable hashtable = new Hashtable();
            hashtable.Add(1 , "One");
            hashtable.Add(2,"Two");
            hashtable.Add(0,"Zero");
            Hashtable hashtable1 = new Hashtable(){
                {"TN" , "Tamil Nadu"},
                {"KA" , "Karnataka"}
            };
            foreach(DictionaryEntry i in hashtable)
                    Console.WriteLine(i.Key +  " " +  i.Value);
                
            hashtable.Add(4, "Four");
            hashtable[4] = "Four to Nallu";

            var result = hashtable[2] ;
            System.Console.WriteLine(result);
            if(hashtable.ContainsKey(4))
               System.Console.WriteLine(hashtable[4]);
            
            hashtable.Remove(4);

        }

        public void Stack(){
            Stack s = new Stack();
            s.Push("One");
            s.Push("Two");
            s.Push(1);
            s.Push(true);
            s.Push("d");

            Stack<int> s1 = new Stack<int>();
            s1.Push(4);
            s1.Push(1);
            s1.Push(5);
            
            foreach(var i in s1)
                    System.Console.WriteLine(i);

            s1.Pop();
            s1.Pop();
            if(s1.Contains(4))
               s1.Pop();
            int result ;
            s1.TryPeek(out result);
            System.Console.WriteLine("This is Try peek : "+ result);
        }

        public void Queue(){
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            
            System.Console.WriteLine("Dequeue is : "+ queue.Dequeue());
            
            foreach(var i in queue)
                    System.Console.WriteLine(i);

            System.Console.WriteLine(queue.Contains(2));
            System.Console.WriteLine("Total Element is : " + queue.Count());

        }
        public static void Mainn(){
            AList a = new AList();
            //ValueTuple
            var t = (1 , "Navaneethan" , "Neha");
            var t1 = (Id: 2 , firstName : "Neha" , lastName :"Navaneethan");
            System.Console.WriteLine("This is Item 2 :"+t.Item2);
            System.Console.WriteLine("This is t1 firstname "+ t1.firstName );

        }
    }
}