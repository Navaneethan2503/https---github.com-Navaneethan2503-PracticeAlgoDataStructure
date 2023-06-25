using System.Text;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Numerics;

namespace HackerRank
{
    class Solution
    {
        public static int quickestWayUp(List<List<int>> ladders, List<List<int>> snakes)
        {
            bool[] visited = new bool[101];
            IDictionary<int,int> graph = new Dictionary<int,int>();
            for(int i = 0; i<ladders.Count; i++){
                graph.Add(ladders[i][0], ladders[i][1]);
            }
            for(int i = 0; i<snakes.Count; i++){
                graph.Add(snakes[i][0], snakes[i][1]);
            }
            Queue<(int,int)> queue = new Queue<(int,int)>();
            queue.Enqueue((1,0));
            while(queue.Count>0){
                var current = queue.Dequeue();
                int node = current.Item1;
                int rolls = current.Item2;
                if(node == 100) return rolls;
                visited[node] = true;
                for(int i = 1; i<7; i++){
                    int nextNode = node+i;
                    if(nextNode<=100 && !visited[nextNode]){
                        if(graph.ContainsKey(nextNode)){
                            queue.Enqueue((graph[nextNode],rolls+1));
                        }
                        else{
                            queue.Enqueue((nextNode,rolls+1));
                        }
                    }
                }
            }
            return -1;
        }

        public static void Mainn(string[] arg)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<List<int>> ladders = new List<List<int>>();
            ladders.Add(new List<int>(){32,62});
            ladders.Add(new List<int>(){42,68});
            ladders.Add(new List<int>(){12,98});
            List<List<int>> snakes = new List<List<int>>();
            snakes.Add(new List<int>(){95,13});
            snakes.Add(new List<int>(){97,25});
            snakes.Add(new List<int>(){93,37});
            snakes.Add(new List<int>(){79,27});
            snakes.Add(new List<int>(){75,19});
            snakes.Add(new List<int>(){49,47});
            snakes.Add(new List<int>(){67,17});
            var res = quickestWayUp(ladders, snakes);
            System.Console.WriteLine(res);
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.ElapsedMilliseconds * 0.001);
        }
    }
}
