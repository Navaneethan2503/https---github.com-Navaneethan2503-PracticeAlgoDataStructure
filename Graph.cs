namespace Graph{

    class Graph{
        
        // Representation of Graph can be Adjacent Matrix (2D - Array) , Adjacent List (array of list or array of linkedList or dictionary of array ).

        private List<int>[] _adj ;
        private int _v;

        public Graph(int v)
        {
            _adj = new List<int>[v];
            for(int i = 0; i<v; i++){
                _adj[i] = new List<int>();
            }
            _v = v;
        }

        //Directed graph
        public void addEdgedg(int v, int e){
            _adj[v].Add(e);
        }

        //undirected graph
        public void addEdgeudg(int v, int e){
            _adj[v].Add(e);
            _adj[e].Add(v);
        }


        public static void adjacencyMatrix(){
            int n = 4, m = 4;
            int[,] adj = new int[n+1,m+1];
            List<(int,int)> edges = new List<(int, int)>(){(0,1),(0,4),(1,0),(1,4),(1,3),(2,1),(2,3),(3,1),(3,2),(3,4),(4,0),(4,1),(4,3)};
            for(int i = 0; i<edges.Count; i++){
                int u = edges[i].Item1, v = edges[i].Item2;
                adj[u,v] = 1;
            }
            
            for(int i = 0; i<n+1; i++){
                for(int j = 0; j<m+1; j++){
                    System.Console.Write(adj[i,j]);
                }
                System.Console.WriteLine();
            }
        }

        public static void adjacentList(){
            int v = 5;
            List<int>[] adj = new List<int>[v];
            for(int i = 0; i<v; i++){
                adj[i] = new List<int>();
            }
            List<(int,int)> edges = new List<(int, int)>(){(0,1),(0,4),(1,0),(1,4),(1,3),(2,1),(2,3),(3,1),(3,2),(3,4),(4,0),(4,1),(4,3)};
            for(int i = 0; i<edges.Count; i++){
                adj[edges[i].Item1].Add(edges[i].Item2);
            }
            for(int i = 0; i<v; i++){
                for(int j = 0; j<adj[i].Count; j++){
                    System.Console.Write(i+" -> "+adj[i][j]+" , ");
                }
                System.Console.WriteLine();
            }

        }
    
        public void bfs(int s){//connected graph - time complexity O(v+e) and space complexity - O(v)
            List<int> queue = new List<int>();
            queue.Add(s);
            System.Console.Write(s+" ");
            bool[] visited = new bool[_v];
            visited[s] = true;
            while(queue.Count>0){
                int node = queue[0];
                queue.RemoveAt(0);
                var adj = _adj[node];
                for(int i = 0; i<adj.Count; i++){
                    if(!visited[adj[i]]){
                        queue.Add(adj[i]);
                        visited[adj[i]] = true;
                        System.Console.Write(adj[i] + " ");
                    }
                }
            }
        }
        
        public void bfsdisconnected(){ //disconnected graph - time complexity O(v+e) and space complexity - O(v)
            bool[] visited = new bool[_v];
            for(int i = 0; i<_v; i++){
                if(!visited[i]){
                    List<int> queue = new List<int>();
                    System.Console.Write(i+" ");
                    queue.Add(i);
                    visited[i] = true;
                    while(queue.Count>0){
                        int node = queue[0];
                        queue.RemoveAt(0);
                        for(int j = 0; j<_adj[node].Count; j++){
                            if(!visited[_adj[node][j]]){
                                visited[_adj[node][j]] = true;
                                queue.Add(_adj[node][j]);
                                System.Console.Write(_adj[node][j]+" ");
                            }
                        }
                    }
                }
                System.Console.WriteLine();
            }
        }

        public void dfs(int s){// connected graph - time and space complexity is O(v+e)
            bool[] visited = new bool[_v];
            void dfshelper(int v, bool[] visited){
                visited[v] = true;
                System.Console.Write(v+" ");
                foreach(var i in _adj[v]){
                    if(!visited[i]){
                        dfshelper(i,visited);
                    }
                }
            }
            dfshelper(s,visited);
        }

        public void dfsdisconnected(int s){// disconnected graph - time complexity is O(v+e) and space is O(v)
            bool[] visited = new bool[_v];
            void dfshelper(int v, bool[] visited){
                visited[v] = true;
                System.Console.Write(v+" ");
                foreach(var i in _adj[v]){
                    if(!visited[i]){
                        dfshelper(i,visited);
                    }
                }
            }
            for(int i = 0; i<_v; i++){
                if(!visited[i]){
                    dfshelper(i,visited);
                }
            }
        }

        public void dfsUsingStrak(int s){ // dfs using stack data structure iterative mode
            List<int> stack = new List<int>();
            bool[] visited = new bool[_v];
            stack.Add(s);
            visited[s] = true;
            System.Console.Write(s+" ");
            while(stack.Count>0){
                int node = stack[stack.Count-1];
                stack.RemoveAt(stack.Count-1);
                if(visited[node]==false){
                    visited[node] = true;
                    Console.Write(node+" ");
                }
                foreach(var i in _adj[node]){
                    if(!visited[i]){
                        stack.Add(i);
                    }
                }
            }
        }

        public void tropologicialSort(int s){ // time complexity - O(V+E) and space is O(V) , similer to dfs only changes is visit node as far as adj node and add to top order list
            Stack<int> stack = new Stack<int>();
            bool[] visited = new bool[_v];
            void help(int s, bool[] visited){
                visited[s] = true;
                foreach(var i in _adj[s]){
                    if(!visited[i]){
                        help(i, visited);
                    }
                }
                stack.Push(s);
            }
            
            for(int i = 5; i>=0; i--){
                if(!visited[i]){
                    help(i,visited);
                }
            }
            
            foreach(var i in stack){
                System.Console.Write(i+" ");
            }
        }

        public void allTropoloicalSortUtil(bool[] visited , List<int> stack, int[] indegree){
            bool flag = false;
            for(int i = 0; i<_v; i++){
                if(!visited[i] && indegree[i]==0){
                    visited[i] = true;
                    stack.Add(i);
                    foreach(var n in _adj[i]){
                        indegree[n]--;
                    }
                    allTropoloicalSortUtil(visited,stack,indegree);
                    visited[i] = false;
                    stack.RemoveAt(stack.Count-1);
                    foreach(var n in _adj[i]){
                        indegree[n]++;
                    }
                    flag = true;
                }
            }
            if(!flag){
                    stack.ForEach(t => Console.Write(t+" "));
                    System.Console.WriteLine();
                }
        }

        public void allTropologicalSort(){ // time complexity is O(V!) and space complexity is O(V)-using backtracking and dfs 
            bool[] visited = new bool[_v];
            List<int> stack = new List<int>();
            int[] indegree = new int[_v];
            for(int i = 0; i<_v; i++){
                foreach(var a in _adj[i]){
                    indegree[a]++;
                }
            }
            allTropoloicalSortUtil(visited,stack,indegree);
        }

        public void tropologicalSortKahns(){//time and space is O(V+E) and O(V) , it is like bfs level-wise ordering based on indegree is 0
            int[] indegree = new int[_v];
            for(int i = 0; i<_v; i++){
                foreach(var j in _adj[i]){
                    indegree[j]++;
                }
            }
            List<int> topOrder = new List<int>();
            List<int> queue = new List<int>();
            for(int i = 0; i<_v; i++){
                if(indegree[i]==0){
                    queue.Add(i);
                }
            }
            int count = 0;
            while(queue.Count>0){
                int u = queue[0];
                queue.RemoveAt(0);
                topOrder.Add(u);
                foreach(var v in _adj[u]){
                    if(--indegree[v]==0){
                        queue.Add(v);
                    }
                }
                count++;
            }
            if(count != _v) return;
            for(int i = 0; i<_v; i++){
                System.Console.Write(topOrder[i]+" ");
            }
        }

        public void topSortUtilTime(int v , bool[] visited, int[] depature, ref int time){
            visited[v] = true;
            foreach(var i in _adj[v]){
                if(!visited[i]){
                    topSortUtilTime(i,visited,depature, ref time);
                }
            }
            depature[time++] = v;
        }

        public void topSortDepatureTime(){
            bool[] visited = new bool[_v];
            int[] depature = new int[_v];
            for(int i = 0; i<_v; i++){
                depature[i] = -1;
            }
            int time = 0;
            for(int i = 0; i<_v; i++){
                if(!visited[i]){
                    topSortUtilTime(i,visited,depature, ref time);
                }
            }
            for(int i = _v-1 ; i>= 0 ; i--){
                System.Console.Write(depature[i] +" ");
            }
        }
    }

    class AdjNode{
        private int _v;
        private int _weight;

        public AdjNode(int v, int weight){
            this._v = v;
            this._weight = weight;
        }

        public int getV(){return this._v; }
        public int getWeight(){return this._weight; }
    }

    class AdjNodeWeightGraph{
        private List<AdjNode>[] adjNodeList ;
        private int _V;

        public AdjNodeWeightGraph(int V){
            this._V = V;
            adjNodeList = new List<AdjNode>[_V];
            for(int i = 0; i<_V; i++){
                adjNodeList[i] = new List<AdjNode>();
            }
        }

        public void addEdge(int u, int v, int w){
            AdjNode node = new AdjNode(v,w);
            adjNodeList[u].Add(new AdjNode(v,w));
        }

        public void topUtilSort(bool[] visited, Stack<int> stack, int v){
            visited[v] = true ;
            for(int i = 0 ; i<adjNodeList[v].Count; i++){
                AdjNode node = adjNodeList[v][i];
                topUtilSort(visited, stack, node.getV());
            }
            stack.Push(v);
        }

        public void longPathDAG(int s){
            bool[] visited = new bool[_V];
            Stack<int> stack = new Stack<int>();
            int[] dis = new int[_V];
            for(int i = 0; i<_V; i++){
                dis[i] = Int32.MinValue;
            }
            dis[s] = 0;
            for(int i = 0; i<_V; i++){
                if(!visited[i]){
                    topUtilSort(visited,stack,i);
                }
            }
            while(stack.Count>0){
                int n = stack.Pop();
                if(dis[n]!= Int32.MinValue){
                    for(int i = 0; i<adjNodeList[n].Count; i++){
                        AdjNode node = adjNodeList[n][i];
                        if(dis[node.getV()]<dis[n]+adjNodeList[n][i].getWeight()){
                            dis[node.getV()] = dis[n]+adjNodeList[n][i].getWeight();
                        }
                    }
                }
            }

            for(int i = 0; i<_V; i++){
                if( dis[i] == Int32.MinValue){
                    System.Console.Write("Inf"+" ");
                }else{
                    System.Console.Write(dis[i]+" ");
                }
            }
        }


    }
    class GraphTheory{

        
        public static void Mainn(string[] arg){
            System.Console.WriteLine("hello Graph");
            Graph g = new Graph(6);
            g.addEdgedg(5,0);
            g.addEdgedg(5,2);
            g.addEdgedg(4,0);
            g.addEdgedg(4,1);
            g.addEdgedg(2,3);
            g.addEdgedg(3,1);
        
            g.topSortDepatureTime();

            AdjNodeWeightGraph gw = new AdjNodeWeightGraph(6);
            gw.addEdge(0, 1, 5);
            gw.addEdge(0, 2, 3);
            gw.addEdge(1, 3, 6);
            gw.addEdge(1, 2, 2);
            gw.addEdge(2, 4, 4);
            gw.addEdge(2, 5, 2);
            gw.addEdge(2, 3, 7);
            gw.addEdge(3, 5, 1);
            gw.addEdge(3, 4, -1);
            gw.addEdge(4, 5, -2);
        }
    }
}