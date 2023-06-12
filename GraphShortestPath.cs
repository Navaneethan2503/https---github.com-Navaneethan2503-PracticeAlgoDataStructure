namespace GraphShortestPath{
    
    class AdjNode{
        private int v;
        private int w;

        public AdjNode(int vn, int weight){
            this.v = vn;
            this.w = weight;
        }

        public int getV(){
            return this.v;
        }

        public int getw(){
            return this.w;
        }
    }
    class Graph{
        private int _N;
        private List<AdjNode>[] adj ;

        private int[] prev;

        readonly static int INF = 9999;

        public Graph(int n){
            this._N = n;
            this.adj = new List<AdjNode>[_N];
            for(int i = 0; i<_N; i++){
                adj[i] = new List<AdjNode>();
            }
        }

        public void addEdgeUdg(int u, int v, int w){
            AdjNode node = new AdjNode(v,w);
            AdjNode nodev = new AdjNode(u,w);
            adj[u].Add(node);
            adj[v].Add(nodev);
        }

        public void addEdgeDg(int u, int v, int w){
            AdjNode node = new AdjNode(v,w);
            adj[u].Add(node);
        }

        public void printGraph(){
            for(int i = 0; i<_N; i++){
                foreach(var j in adj[i]){
                    System.Console.Write(i+" "+j.getV()+" = "+ j.getw());
                }
                System.Console.WriteLine();
            }
        }

        public int dijkstras(int start, int end){// time complexity is O(E logV ), space is O(V)
            bool[] visited = new bool[this._N];
            int[] dis = new int[this._N];
            prev = new int[_N];
            for(int i = 0; i<_N; i++){
                dis[i] = Int32.MaxValue;
            }
            dis[start] = 0;
            PriorityQueue<int,int> queue = new PriorityQueue<int,int>();
            queue.Enqueue(0,0);
            while(queue.Count>0){
                int u = queue.Dequeue();
                visited[u] = true;
                //if(uw > dis[u]) continue // this is optimization 
                foreach(var neigh in this.adj[u]){
                    int v = neigh.getV();
                    int vw = neigh.getw();
                    if(visited[v]) continue;
                    int nextDis = dis[u]+vw;
                    if(nextDis<dis[v]){
                        prev[v] = u;
                        dis[v] = nextDis;
                        //you can check for element already exists are not for deletion of duplicate
                        queue.Enqueue(v,nextDis);
                    }
                }
                if(u==end){
                    return dis[end];
                }
            }
            foreach(var i in dis){
                System.Console.Write(i+" ");
            }
            return Int32.MaxValue;
        }

        public void reconstructPath(int start, int end){
            if(end <0 || end >= _N) throw new ArgumentOutOfRangeException("Invalid End Points");
            if(start < 0 || end >= _N ) throw new ArgumentOutOfRangeException("Invalid Start Points");
            int dis = dijkstras(start, end);
            List<int> path = new List<int>();
            if(dis == Int32.MaxValue) System.Console.WriteLine("No Route Path Found");
            else {
                for(int i = end; i!=start; i = prev[i]){
                    path.Add(i);
                }
                path.Add(start);
            }
            for(int i = path.Count-1; i>=0; i--){
                System.Console.Write(path[i]+" ");
            }
        }

        public void bellmanFord(int start){ // time complexity - O(VE) , space is O(V)
        //initial dis to all node as infinite except start 
            int[] dis = new int[_N];
            for(int i = 0; i<_N; i++){
                dis[i] = Int32.MaxValue;
            }
            dis[start] = 0;

            //relax all edges by iterating v-1 times and update shortesh distance in dis array
            for(int i = 0 ; i<_N-1; i++){
                for(int j = 0; j<_N; j++){
                    foreach(var edge in adj[j]){
                        int v = edge.getV();
                        int w = edge.getw();
                        if(dis[j]+w < dis[v]) dis[v] = dis[j]+w;
                    }
                }
            }

            //detect negative cycle by iterating v-1 time and mark it as minus infinity . if any nodes in loop then it is directly effected and also reachable nodes by the affected nodes are also marked as minus infinity .
            //in this iteration if current distance is less than to the adj distance then it is in cycle so assign minus infinate .
            for(int i = 0 ; i<_N-1; i++){
                for(int j = 0; j<_N; j++){
                    foreach(var edge in adj[j]){
                        int v = edge.getV();
                        int w = edge.getw();
                        if(dis[j]+w < dis[v]) dis[v] = Int32.MinValue;
                    }
                }
            }

            foreach(var i in dis){
                System.Console.Write(i+" ");
            }
        }

        public void floydWarshall(int[,] matrixgraph, int n){//time complexity is O(v^3) and space is O(V^2)
            //ideas is take all vertices from 0 to k-1 , where each time selected vertices considered as intermediate node between i and j route , 
            //find shortest path for all pair i and j through possible k and update the dis when dp[i][k]+dp[k][j] < dp[i][j] .

            //first steps involed here is copy adj to dp matrix to track shortest distance .
            int[,] dp = new int[n,n];
            for(int i = 0; i<n; i++){
                for(int j = 0; j<n; j++){
                    dp[i,j] = matrixgraph[i,j];
                }
            }

            int[,] next = new int[n,n];

            //steps 2 the idea of this algorithm works
            for(int k = 0; k<n; k++){
                for(int i = 0; i<n; i++){
                    for(int j = 0; j<n; j++){
                        if(dp[i,k]+dp[k,j]<dp[i,j]){
                            dp[i,j] = dp[i,k]+dp[k,j];
                            next[i,j] = j;
                        }
                    }
                }
            }

            //optional to detect the negative cycle
            detectNegativeCycle(dp,n,next);

            //print
            for(int i = 0; i<n; i++){
                for(int j = 0; j<n; j++){
                    if(dp[i,j]==int.MinValue){
                        System.Console.Write("INF ");
                    }
                    else{
                        System.Console.Write(dp[i,j]+" ");
                    }
                }
                System.Console.WriteLine();
            }
        }

        public void detectNegativeCycle(int[,] dp, int n, int[,] next){
            for(int k = 0; k<n; k++){
                for(int i = 0; i<n; i++){
                    for(int j = 0; j<n; j++){
                        if(dp[i,j]>dp[i,k]+dp[k,j]) {
                            dp[i,j] = int.MinValue;
                            next[i,j] = -1; 
                        }
                    }
                }
            }
        }


    }
    public class Program{
        public static void Mainn(String[] args){
            Console.WriteLine("Hello Lets Starts Shorthest Path Algos");
            Graph g = new Graph(7);
            g.addEdgeDg(0,1,-1);
            g.addEdgeDg(0,2,4);
            g.addEdgeDg(1,2,3);
            g.addEdgeDg(1,3,2);
            g.addEdgeDg(1,4,2);
            g.addEdgeDg(3,2,5);
            g.addEdgeDg(3,1,1);
            g.addEdgeDg(4,3,-3);

            int INF = 9999;

            int[, ] graph = { { 0, 5, INF, 10 },
                          { INF, 0, 3, INF },
                          { INF, INF, 0, 1 },
                          { INF, INF, INF, 0 } };

            g.floydWarshall(graph,4);
        }
    }

}









