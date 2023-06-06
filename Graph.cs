namespace Graph{

    class Graph{
        // Representation of Graph can be Adjacent Matrix (2D - Array) , Adjacent List (array of list or array of linkedList or dictionary of array ).
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
    }
    class GraphTheory{

        
        public static void Main(string[] arg){
            System.Console.WriteLine("hello Graph");
            Graph.adjacentList();
        }
    }
}