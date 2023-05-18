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
        static int[] findStartingPoint(List<string> matrix){
            int[] index = new int[2];
            bool isFind = true;
            for(int i = 0; i<matrix.Count && isFind; i++){
                for(int j = 0; j<matrix[i].Length; j++){
                    if(matrix[i][j] == 'M'){
                        index[0] = i;
                        index[1] = j;
                        isFind = false;
                    }
                }
            }
            return index;
        }

        static bool isValid(int i , int j , int n , int m){
            if(i>=n || j>=m || i<0 || j<0 ) return false;
            return true;
        }
        public static string countLuck(List<string> matrix, int k) {
            int waves = 0;
            var index = Solution.findStartingPoint(matrix);
            int mi = index[0], mj = index[1];
            var queue = new List<(int,int)>();
            queue.Add((mi,mj));
            var direction = new List<(int,int)>(){
                (1,0), (-1,0),(0,-1), (0,1)
            }; 
            IDictionary<(int,int),int> isVisited = new Dictionary<(int,int), int>();
            isVisited.Add((mi,mj), 1);
            while(queue.Count > 0){
                int i = queue[0].Item1, j = queue[0].Item2;
                if(matrix[i][j] == '*') break;
                queue.RemoveAt(0);
                int count = 0;
                foreach(var dir in direction){
                    int di = dir.Item1, dj = dir.Item2;
                    if( isValid((di+i), (dj+j), matrix.Count, matrix[i].Length) && !(isVisited.ContainsKey(((di+i),(dj+j))))){
                        if(matrix[di+i][dj+j] == '.' || matrix[di+i][dj+j] == '*'){
                            queue.Add((di+i, dj+j));
                            isVisited.Add((di+i,dj+j), 1);
                            count++;
                        }
                    }
                }
                if(count>1) waves++;
            }
            return (waves == k)?"Impressed":"Oops!";
        }

        public static void Mainn(string[] arg)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> matrix = new List<string>(){".X.X......X", ".X*.X.XXX.X", ".XX.X.XM...", "......XXXX."};
            List<string> mat = new List<string>(){"*..", "X.X", "..M"};
            var res = countLuck(mat, 1);
            System.Console.WriteLine(res);
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.ElapsedMilliseconds * 0.001);
        }
    }
}
