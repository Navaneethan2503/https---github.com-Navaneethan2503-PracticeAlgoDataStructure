namespace Tree{
    public class Node{
        public int data;
        public Node left, right;
        public Node(int data)
        {
            this.data = data;
            this.left = this.right = null;
        }

        public void insert(int value){
            if(value <= data){
                if(left == null) left = new Node(value);
                else left.insert(value);
            }
            else if(value >= data){
                if(right == null) right = new Node(value);
                else right.insert(value);
            }
        }

        
    }
    class Tree{

        public Node root ;

        public Tree(){
            root = null;
        }

        
        public static void Mainm(string[] args){
            System.Console.WriteLine("Tree Data Structure");
            Tree t = new Tree();
            Node n = new Node(1);
            n.insert(2);
            n.insert(3);

            //System.Console.WriteLine(t.root.data);
        }
    }
}