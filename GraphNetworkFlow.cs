namespace GraphNetworkFlow
{
    class Edge
    {
        public long from,
            to;
        public Edge residual;

        public long flow;

        public readonly long capacity;

        public Edge(long from, long to, long capacity)
        {
            this.from = from;
            this.to = to;
            this.capacity = capacity;
        }

        public bool isResidual()
        {
            return capacity == 0;
        }

        public long remainingCapacity()
        {
            return this.capacity - this.flow;
        }

        public void arguments(long minEdgeValue)
        { //minEdgeValue - bottleneck value
            this.flow += minEdgeValue;
            this.residual.flow -= minEdgeValue;
        }

        public string toString(long s, long t)
        {
            string u = (from == s) ? "S" : ((from == t) ? "t" : from.ToString());
            string v = (to == s) ? "s" : ((to == t) ? "t" : to.ToString());
            return $"Edge {u} -> {v} | flow = {this.flow} | capacity = {this.capacity} | is residual : {this.isResidual()}";
        }
    }

    abstract class NetworkFlowSolverBase
    {
        public readonly long INF = long.MaxValue / 2;

        public long n,
            s,
            t;

        protected int visitedToken = 1;
        protected int[] visited;

        protected bool solved;

        protected long maxFlow;

        protected List<Edge>[] adj;

        public NetworkFlowSolverBase(long n, long s, long t)
        {
            this.n = n;
            this.s = s;
            this.t = t;
            initializeGraph();
            visited = new int[this.n];
        }

        public void initializeGraph()
        {
            adj = new List<Edge>[this.n];
            for (int i = 0; i < this.n; i++)
            {
                adj[i] = new List<Edge>();
            }
        }

        public virtual void addEdge(long from, long to, long capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity should be Greater than 0");
            var e1 = new Edge(from, to, capacity);
            var e2 = new Edge(to, from, 0);
            e1.residual = e2;
            e2.residual = e1;
            adj[from].Add(e1);
            adj[to].Add(e2);
        }

        public abstract void solve();

        private void execute()
        {
            if (solved)
                return;
            solved = true;
            solve();
        }

        public List<Edge>[] getGraph()
        {
            execute();
            return adj;
        }

        public long getMaxFlow()
        {
            execute();
            return maxFlow;
        }
    }

    class FordFulkersonDfsSolver : NetworkFlowSolverBase
    { // time complexity - O(F*E) - space complexity - O(E)
        public FordFulkersonDfsSolver(long n, long s, long t)
            : base(n, s, t) { }

        public override void solve()
        {
            for (long f = dfs(s, INF); f != 0; f = dfs(s, INF))
            {
                visitedToken++;
                maxFlow += f;
            }
        }

        public long dfs(long node, long flow)
        {
            if (node == t)
                return flow;
            visited[node] = visitedToken;
            List<Edge> edges = adj[node];
            foreach (var edge in edges)
            {
                if (edge.remainingCapacity() > 0 && visited[edge.to] != visitedToken)
                {
                    long bottleneck = dfs(edge.to, Math.Min(flow, edge.remainingCapacity()));
                    if (bottleneck > 0)
                    {
                        edge.arguments(bottleneck);
                        return bottleneck;
                    }
                }
            }
            return 0;
        }
    }

    //Bipartite Graph is those vertices where divide two groups based on independent in each other .
    //Bipartite Graph can solve problems like Lirary books to students , finding jobs to seekers , like two seperate group dependant on another group can apply this graph and follw up the below algorithms .
    /*
    To Find the Comman Matching Variations
                  
         Easy - E          BiPartitie Graph                   |        Non- BiPartite Graph   - Harder - H
    --------------------------------------------------------------------------------------------------
E - Un Weighted Graph  |  1) Max-Flow Algorithm              |  1) Edmonds blossom Algorithm
                       |  2) Repeated Argumenting Path with  |
                       |    dfs .
                       |  3) Hopcroft-Karp Algorithm         |
    ---------------------------------------------------------------------------------------------------
H - Weighted Graph     |  1) Min Cost Max Flow Algorithm     |  1) Dp Solutions for Small Graph
                       |  2) Hungarian Algorithm (Perfect
                             Matching )
                       |  3) Lp Network Simplex
    ______________________________________________________________________________________________________
    */
    class EdmondsKarpSolver : NetworkFlowSolverBase
    {
        //In this Algorithm, Differance from above ford fulkerson is only shorthest path from s to t is taken in terms of min number of edges .

        public EdmondsKarpSolver(long n, long s, long t)
            : base(n, s, t) { }

        public override void solve()
        {
            long flow = 0;
            do
            {
                visitedToken++;
                flow = bfs();
                maxFlow += flow;
            } while (flow != 0);
        }

        public long bfs()
        {
            Queue<long> queue = new Queue<long>();
            visited[s] = visitedToken;
            queue.Enqueue(s);
            Edge[] prev = new Edge[n];
            while (queue.Count > 0)
            {
                long at = queue.Dequeue();
                if (at == t)
                    break;
                foreach (var edge in adj[at])
                {
                    if (edge.remainingCapacity() > 0 && visited[edge.to] != visitedToken)
                    {
                        visited[edge.to] = visitedToken;
                        prev[edge.to] = edge;
                        queue.Enqueue(edge.to);
                    }
                }
            }

            //sink not reachable
            if (prev[t] == null)
                return 0;

            //find argumenting path and bottleneck
            long bottleneck = long.MaxValue;
            for (Edge edge = prev[t]; edge != null; edge = prev[edge.from])
            {
                bottleneck = Math.Min(bottleneck, edge.remainingCapacity());
            }

            //retrace the argumenting path and update the flow
            for (Edge edge = prev[t]; edge != null; edge = prev[edge.from])
            {
                edge.arguments(bottleneck);
            }
            return bottleneck;
        }
    }

    class CapacityScalingSolver : NetworkFlowSolverBase// time complexity is O(E^2 lof U ) if smaller path finds or O(EV log (U))
    {
        /*
        The Idea of Capacity Scaling Algorithm for each Argumenting Path it takes largest edge first then smaller one . which reduced run time than Ford Fulkerson Algorithm .
        To do this , we use two variable to keep track of largest edge values . 
        U = which is largest edge value of graph
        delta= which is highest power of base 2 that less than U .
        And the Logic is :
        The Algorithm Repeatedly finds the argumenting path with remaining capacity >= delta untill no more path satisify this criteria , then decrease the delta by 2 .  
        */
        long delta = 0;

        public CapacityScalingSolver(long n, long s, long t)
            : base(n, s, t) { }

        public override void addEdge(long from, long to, long capacity)
        {
            base.addEdge(from, to, capacity);
            delta = Math.Max(delta, capacity);
        }

        public override void solve()
        {
            System.Console.WriteLine("Capacity Solving Logic");
            delta = (long)Math.Pow(2, (int)Math.Floor(Math.Log(delta) / Math.Log(2)));
            for (long f = 0; delta > 0; delta /= 2)
            {
                do
                {
                    visitedToken++;
                    f = dfs(s, INF);
                    maxFlow += f;
                } while (f != 0);
            }
        }

        public long dfs(long node, long flow)
        {
            if (node == t)
                return flow;
            visited[node] = visitedToken;
            List<Edge> edges = adj[node];
            foreach (var edge in edges)
            {
                if (edge.remainingCapacity() >= delta && visited[edge.to] != visitedToken)
                {
                    long bottleneck = dfs(edge.to, Math.Min(flow, edge.remainingCapacity()));
                    if (bottleneck > 0)
                    {
                        edge.arguments(bottleneck);
                        return bottleneck;
                    }
                }
            }
            return 0;
        }
    }

    class Program
    {
        public static void Main(String[] args)
        {
            System.Console.WriteLine("Graph Network Flow ");
            long n = 12;
            long s = n-2;
            long t =n-1;
            NetworkFlowSolverBase network = new CapacityScalingSolver(n,s,t);
            //start
            network.addEdge(s,0,10);
            network.addEdge(s,1,5);
            network.addEdge(s,2,10);

            //middle
            network.addEdge(0,3,10);
            network.addEdge(1,2,10);
            network.addEdge(2,5,15);
            network.addEdge(3,1,2);
            network.addEdge(3,6,15);
            network.addEdge(4,1,15);
            network.addEdge(4,3,3);
            network.addEdge(5,4,4);
            network.addEdge(5,8,10);
            network.addEdge(6,7,10);
            network.addEdge(7,4,10);
            network.addEdge(7,5,7);

            //sink
            network.addEdge(6,t,15);
            network.addEdge(8,t,10);
            
            /*

            int n = 6;
            int s = n - 1;
            int t = n - 2;

            NetworkFlowSolverBase solver;
            solver = new CapacityScalingSolver(n, s, t);

            // Source edges
            solver.addEdge(s, 0, 10);
            solver.addEdge(s, 1, 10);

            // Sink edges
            solver.addEdge(2, t, 10);
            solver.addEdge(3, t, 10);

            // Middle edges
            solver.addEdge(0, 1, 2);
            solver.addEdge(0, 2, 4);
            solver.addEdge(0, 3, 8);
            solver.addEdge(1, 3, 9);
            solver.addEdge(3, 2, 6);

            */

            System.Console.WriteLine("Maximun flow is : {0}", network.getMaxFlow());

            List<Edge>[] resultGraph = network.getGraph();
            int count = 1;
            foreach (var edges in resultGraph)
            {
                foreach (var edge in edges)
                {
                    System.Console.Write((count++) + " ");
                    System.Console.WriteLine(edge.toString(s, t));
                }
            }
        }
    }
}
