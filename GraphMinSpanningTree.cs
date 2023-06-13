using System;

namespace GraphMinSpanningTree{

    class AdjNode{
        private int v;
        private int w;
        public AdjNode(int v, int w){
            this.v = v;
            this.w = w;
        }

        public int getV(){
            return this.v;
        }

        public int getW(){
            return this.w;
        }
    }
    class Graph{
        private int _N;
        private List<AdjNode>[] adj;

        public Graph(int n){
            this._N = n;
            adj = new List<AdjNode>[_N];
            for(int i = 0; i<_N; i++){
                adj[i] = new List<AdjNode>();
            }
        }

        public void addEdgeDg(int u, int v, int w){
            AdjNode node = new AdjNode(v,w);
            adj[u].Add(node);
        }

        public void addEdgeUDg(int u, int v, int w){
            AdjNode node = new AdjNode(v,w);
            AdjNode node1 = new AdjNode(u,w);
            adj[u].Add(node);
            adj[v].Add(node1);
        }

        private bool[] visited;

        private PriorityQueue<(int,int, int), int> pq;

        //Lazy prims algorithm - we can find the minimum total edge cost to connect all vertex in graph .
        //with the Prioritized Queue implementation we find O(E* log E) which is called Lazy Prims Algorithm .
        /*
        Ideas is add all the adj of current node in pq and go with min weight and if already visited node then continue to next node in pq do on like so till edge count reach till n-1 edges .
        */
        public void addAllEdgesofNodeToPq(bool[] visited, int u){
            visited[u] = true;
            for(int v = 0; v<adj[u].Count; v++){
                int av = adj[u][v].getV();
                if(!visited[av]){
                    int aw = adj[u][v].getW(); 
                    pq.Enqueue((u,av, aw), aw);
                }
            }
        }

        public void primsLazyMST(int s){//Time Complexity is O(E*log E) and space is O(E)
            int m = _N-1; //MSP Edge will be n-1 edges , so assign like this 
            visited = new bool[_N];
            pq = new PriorityQueue<(int, int, int), int>();
            int edgeCount = 0 , edgeCost = 0;
            List<(int,int, int)> MST = new List<(int,int, int)>();
            addAllEdgesofNodeToPq(visited,s);
            while(pq.Count != 0  && (m != edgeCount)){
                var next = pq.Dequeue();
                if(visited[next.Item2]) continue;
                MST.Add(next);
                edgeCount++;
                edgeCost += next.Item3;
                addAllEdgesofNodeToPq(visited, next.Item2);
            }
            if(edgeCount != m) System.Console.WriteLine("NO MST EXISTS");
            System.Console.WriteLine("Total Cost of Edges of MST is : "+ edgeCost);
            for(int i = 0; i<m; i++){
                System.Console.WriteLine(MST[i].Item1+"-"+MST[i].Item2);
            }
        }

        /*
        In Eager Prims Algorithm Time Complexity is O(E log (V)) , main difference from lazy prims is 
        Instead of adding edges blindly in Priority Queue , we can maintain Index Priority Queue where 
        we can update the edge cost for visiting node adj if already exist in Indexed Priority Queue . 
        if the Edge cost of node for already presented is high and visiting new edge cost is low means update the value here .
        Index Priority Queue => index key = (start , end , cost ) , cost .
        by doing this we can excluse the unwanted edges / already added edges in MST . 
        */
        private IDictionary<int, PriorityQueue<(int,int,int),int>> IPQ;

        public void primsEagerMST(int s){
            bool[] visited = new bool[_N];
            int edgeCount = 0, edgeCost = 0;
            int m = _N-1;
            IPQ = new Dictionary<int, PriorityQueue<(int,int,int),int>>();
            List<int> MST = new List<int>();
            addRelaxToIPQ(visited , s);
            while(IPQ.Count != 0 && m != edgeCount ){
                //var next = IPQ.dequeue();
                edgeCount++;
                //MST.Add((next.Item1,next.Item2));
                //edgeCost += next.Item3;
                //addRelaxToIPQ(visited,NotSupportedException.Item2);
            }
            if(m!=edgeCost) System.Console.WriteLine("NO MST Exist");
            for(int i = 0; i<m; i++){
                System.Console.WriteLine(MST[i]);
            }
        }

        public void addRelaxToIPQ(bool[] visited, int at){
            visited[at] = true;
            foreach(var edge in adj[at]){
                int v = edge.getV();
                int w = edge.getW();
                if(!visited[v]){
                    var pq = new PriorityQueue<(int , int , int), int>();
                    pq.Enqueue((at,v,w),w);
                    IPQ.Add(v,pq);
                }
                else if(IPQ.ContainsKey(at)){
                    var res = IPQ[at];
                    //IPQ.Decrese(node,edge);
                }
            }
        }
        

    }

    class Program{

        public static void Mainn(string[] arg){
            Graph g = new Graph(9);
            g.addEdgeUDg(0,1,4);
            g.addEdgeUDg(0,7,8);
            g.addEdgeUDg(7,1,11);
            g.addEdgeUDg(7,8,7);
            g.addEdgeUDg(7,6,1);
            g.addEdgeUDg(1,2,8);
            g.addEdgeUDg(2,8,2);
            g.addEdgeUDg(8,6,6);
            g.addEdgeUDg(6,5,2);
            g.addEdgeUDg(2,5,4);
            g.addEdgeUDg(2,3,7);
            g.addEdgeUDg(3,5,14);
            g.addEdgeUDg(3,4,9);
            g.addEdgeUDg(5,4,10);
            g.primsLazyMST(0);
        }
    }
}