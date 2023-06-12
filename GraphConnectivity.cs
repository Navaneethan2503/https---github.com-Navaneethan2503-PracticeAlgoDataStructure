namespace GraphConnectivity{
    class Graph{
        private int _N;

        private int _E;

        private List<int>[] adj;

        public Graph(int n){
            this._N = n;
            adj = new List<int>[_N];
            for(int i = 0; i<_N; i++){
                adj[i] = new List<int>();
            }
        }

        public void addEdge(int u, int v){
            adj[u].Add(v);
            adj[v].Add(u);
        }

        public void addEdgeDg(int u, int v){
            adj[u].Add(v);
        }

        public void printGraph(){
            for(int i = 0; i<_N; i++){
                for(int j = 0; j<adj[i].Count; j++){
                    System.Console.Write(i+"-"+adj[i][j]+",");
                }
                System.Console.WriteLine();
            }
        }

        //Global member for bridge algorithm
        private bool[] visited ;
        private int[] ids ;
        private int[] lowLinkValue ;
        private List<int> bridges ;

        private int id = 0;

        public void bridgedfsUtil(int cur , int prev){
            visited[cur] = true;
            id = id + 1;
            ids[cur] = lowLinkValue[cur] = id;  
            foreach(var to in adj[cur]){
                if(to == prev) continue;
                if(!visited[to]){
                    bridgedfsUtil(to,cur);
                    lowLinkValue[cur] = Math.Min(lowLinkValue[to], lowLinkValue[cur]);
                    if(ids[cur]<lowLinkValue[to]){
                        bridges.Add(cur);
                        bridges.Add(to);
                    }
                }
                else{
                    lowLinkValue[cur] = Math.Min(lowLinkValue[cur], lowLinkValue[to]);
                }
            }
        }

        /*This Algorithm is simple which is in dfs , 
        we are tracking low link index values(lowest index node that reachable to cur node here lowest index id is low link value to cur node ) 
        and index for each nodes in incremental . 
        Bridge is Found Only when id of from-edge is less than to low link value of to-edge .
        initially Keep Id and low link value as Id and later update the low link value in linear time .*/

        public void findBridge(){ // time complexity is O(v+E) and space is O(V)
            visited = new bool[_N];
            ids = new int[_N];
            lowLinkValue = new int[_N];
            bridges = new List<int>();
            for(int i = 0; i<_N; i++){
                if(!visited[i]){
                    bridgedfsUtil(i, -1);
                }
            }

            for(int i = 0; i<bridges.Count/2; i++){
                Console.WriteLine(bridges[2*i]+"-"+bridges[2*i+1]);
            }
        }

        //dfs for articulation points use same gobal members and define addition members below
        //visited , ids , lowlink , id are defined above 
        bool[] isArt;
        int outEdgeCount = 0;

        public void artdfsUtil(int root, int at, int parent){
            if(parent == root) outEdgeCount++;//this is for tracking the root does have how many edges 
            visited[at] = true;
            id = id+1;
            ids[at] = lowLinkValue[at] = id;
            foreach(var to in adj[at]){
                if(to == parent) continue;
                if(!visited[to]){
                    artdfsUtil(root,to,at);
                    lowLinkValue[at] = Math.Min(lowLinkValue[at], lowLinkValue[to]);
                    if(ids[at]<lowLinkValue[to]){// find articulation points on bridge
                        isArt[at] = true;
                    }
                    if(ids[at] == lowLinkValue[to]){// find articulation points on cycle
                        isArt[at] = true;
                    }
                }
                else{
                    lowLinkValue[at] = Math.Min(lowLinkValue[at], lowLinkValue[to]);
                }
            } 
        }

        /*
        this approach is same to the above where we tracking low link value and ids of nodes .
        only changes in finding articulation points , we check for ids of from-edge is less than are equal to low link of to-edge ,
        which detects the bridge node and the node in cycle have more than 1 edge .
        */

        public void findArticulationPoints(){//time and space complexity is O(V+E) and O(V)
            visited = new bool[_N];
            lowLinkValue = new int[_N];
            ids = new int[_N];
            isArt = new bool[_N];
            for(int i = 0; i<_N; i++){
                if(!visited[i]){
                    artdfsUtil(i,i,-1);
                    isArt[i] = (outEdgeCount>1);
                }
            }

            for(int i = 0; i<_N; i++){
                if(isArt[i] == true) System.Console.WriteLine(i);
            }
        }


        //use the global members are visited , ids , low-link , id defined earlier .
        private Stack<int> stack;

        private bool[] onStack;
        private int scc ;

        public void tarjansdfs(int at, int parent){
            visited[at] = true;
            ids[at] = lowLinkValue[at] = ++id;
            stack.Push(at);
            onStack[at] = true;
            foreach(var to in adj[at]){
                if(!visited[to]){
                    tarjansdfs(to,at);
                }
                if(onStack[to]){
                    lowLinkValue[at] = Math.Min(lowLinkValue[at], lowLinkValue[to]);
                }
            }
            if(ids[at] == lowLinkValue[at]){
                for(var node = stack.Pop();;node = stack.Pop()){
                    onStack[node] = false; 
                    lowLinkValue[node] = ids[at];
                    if(node == at) break;  
                }
                scc++;
            }
        }

        /*
        In this algorithm, we maintain Stack to invarient the strongly connected components,
        update the low-link value only when the node present in stack . and remove all node when each connected component is found .  
        */
        public void tarjansAlgorithm(){// time complexity is O(V+E) and space is O(V)
            visited = new bool[_N];
            ids = new int[_N];
            lowLinkValue = new int[_N];
            id = 0;
            stack = new Stack<int>();
            onStack = new bool[_N];
            for(int i = 0; i<_N; i++){
                if(!visited[i]){
                    tarjansdfs(i, -1);
                }
            }
            System.Console.WriteLine("There are {0} Strongly Connected Components found in Graph .", scc);
            for(int i = 0; i<_N; i++){
                System.Console.WriteLine(i+" "+ lowLinkValue[i]);
            } 
        }

        /* 
        Find Eulerian Path and Circuit
        here we use , In, Out array , start 
        */
        private int[] outDegree;
        private int[] inDegree ;

        private int start ;

        private Stack<int> solution ;

        //compute the in and out degree for each vertex 
        public void calculateDegree(){
            outDegree = new int[_N];
            inDegree = new int[_N];
            for(int from = 0; from<_N; from++){
                for(int to = 0; to<adj[from].Count; to++){
                    outDegree[from]++;
                    inDegree[adj[from][to]]++;
                    this._E++;
                }
            }
        }

        //Check The graph whether it is Eulerian Path or Not;
        /*
                           Eulerian Circuit     |        Eulerian Path
        ----------------------------------------------------------------------------------
        un-Directed        all vertax have even | Either All vertex have even degree or     
                           degree               | excatly two vertex have odd degree
        ----------------------------------------------------------------------------------

        Directed           All vertex have equal | At most one vertex has out - in = 1 and    
                           in and out degree     | at most one vertex has in - out = 1 and 
                                                    all other vertex have equal in and out degree .         
        */
        public bool isEulerianPath(){
            int start = 0 , end = 0; //start have atmost 1 out vertex and end have at most 1 in vertex
            for(int i = 0; i<_N; i++){
                if(outDegree[i] - inDegree[i] > 1 && inDegree[i]-outDegree[i]>1) return false;
                if(outDegree[i] - inDegree[i] == 1) start++;
                if(inDegree[i] - outDegree[i] == 1) end++;
            }
            return (start == 0 && end == 0) || (start == 1 && end == 1);
        }

        public bool isEulerianCircuit(bool isDirected){
            if(isDirected){
                for(int i = 0; i<_N; i++){
                    if(outDegree[i] != inDegree[i] ) return false;
                }
            }
            else{
                for(int i = 0; i<_N; i++){
                    if(outDegree[i]%2 != 0) return false;
                }
            }
            return true;
        }

        public int findStartNode(){
            int start = 0;
            for(int i = 0; i<_N; i++){
                if(outDegree[i]-inDegree[i] == 1) return i;
                if(outDegree[i]>0) start=i;
            }
            return start;
        }
        
        public void dfs(int at){
            while(outDegree[at] != 0){
                int to = adj[at][--outDegree[at]];
                dfs(to);
            }
            solution.Push(at);
        }

        public void findEulerianPath(){// time Complexity - O(E), space compexity is O(E)
            solution =  new Stack<int>();
            calculateDegree();
            if(!isEulerianPath()) return ;
            dfs(findStartNode());
            if(solution.Count != _E+1) return ;
            for(int i = 0; i<solution.Count; i++){
                System.Console.Write(solution.Peek());
            }
        }

    }

    class Program{
        public static void Mainn(String[] args){
            System.Console.WriteLine("Graph Connectivity Algorithm ");
            Graph g = new Graph(5);
            g.addEdgeDg(0,1);
            g.addEdgeDg(1,2);
            g.addEdgeDg(2,1);
            g.addEdgeDg(1,3);
            g.addEdgeDg(3,4);
            g.findEulerianPath();
            }
    }
}