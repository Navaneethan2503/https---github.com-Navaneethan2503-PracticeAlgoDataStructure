namespace LinkedList{
    class LinkedList{
        class Node{
            public int data ;
            public Node next;
            public Node(int data)
            {
                this.data = data;
                this.next = null;
            }
        }

        class SinglyLinkedList{
            public Node head;
            public SinglyLinkedList(){
                this.head = null ;
            }

            public void AddAtEnd(SinglyLinkedList sl, int data){
            Node node = new Node(data);
            Node temp = sl.head;
            if(sl.head == null ){
                sl.head = node ;
            }else{
                while(temp.next != null){
                    temp = temp.next;
                }
                temp.next = node;
            }
            }

            public void AddAtHead(SinglyLinkedList sl, int data){
                Node node = new Node(data);
                if(sl.head == null){
                    head = node;
                }else{
                    node.next = sl.head;
                    sl.head = node;
                }
            }

            public void AddAtPosition(SinglyLinkedList sl, int position, int data){
                Node node = new Node(data);
                Node temp = sl.head;
                if(temp == null){
                    System.Console.WriteLine("Empty List");
                }
                for(int i = 1; i<position-1; i++){
                    temp = temp.next;
                }
                node.next = temp.next;
                temp.next = node;
            }

            public void removeAtEnd(Node head){
                Node temp = head;
                if(head == null){
                    System.Console.WriteLine("Empty List");
                }
                else{
                    while(head != null){
                        if(head.next.next == null){
                            head.next = null;
                        }
                        head = head.next;
                    }
                }
            }

            public void removeAthead(SinglyLinkedList sl){
                if(sl.head == null){
                    System.Console.WriteLine("Empty List");
                }
                sl.head = sl.head.next;
            }

            public void printSingleLinkedList(Node head){
                if(head == null){
                    System.Console.WriteLine("Empty List");
                }
                while(head != null){
                    System.Console.WriteLine(head.data);
                    head = head.next;
                }
            }

            public Node reverseLinkedList(Node head){
                Node temp = head;
                Node current = null;
                Node prev = null;
                while(temp != null){
                    if(temp == head){
                        current = temp.next;
                        temp.next = null;
                        prev = temp;
                        temp = current;
                    }
                    else{
                        current = temp.next;
                        temp.next = prev;
                        prev = temp;
                        temp = current;
                    }
                }
                return prev;
            }

            public void reversePrint(Node head){
                if(head == null){
                    return;
                }
                reversePrint(head.next);
                System.Console.WriteLine(head.data);
            }

            public Node mergeLists(Node head1, Node head2) {
                Node dummy = new Node(-1);
                Node head3 = dummy;
                while(head1 != null && head2 != null){
                    if(head1.data < head2.data){
                        head3.next = head1;
                        head1 = head1.next;
                    }
                    else{
                        head3.next = head2;
                        head2 = head2.next;
                    }
                    head3 = head3.next;
                }
                while(head1 != null){
                    head3.next = head1;
                    head1 = head1.next;
                    head3 = head3.next;
                }
                while(head2 != null){
                    head3.next = head2;
                    head2 = head2.next;
                    head3 = head3.next;
                }
                return dummy.next;
            }



            public int getNode(Node head, int position){
                int value = 0;
                bool flag = true;
                int recur(Node head, int position){
                    int pos = 0;
                    if(head == null){
                        return -1;
                    }
                    pos = pos + recur(head.next,position);
                    pos = pos + 1;
                if(pos == position){
                    value = head.data;
                    flag = false;
                    System.Console.WriteLine(head.data);
                }
                
                return pos;
                }
                recur(head, position);
                return value;
            }

            public Node removeDuplicates(Node llist){
                Node head = llist;
                Node temp = llist;
                int value = llist.data;
                llist = llist.next;
    while(llist != null){
        if(value == llist.data ){
            System.Console.WriteLine("yyes");
            temp.next = llist.next;
        }
        else{
            value = llist.data;
            temp = llist;
        }
        llist = llist.next;
    }
    return llist;
            }

        }

        
        public static void Main1(string[] args){
            SinglyLinkedList l1 = new SinglyLinkedList();
            l1.AddAtEnd(l1,3);
            l1.AddAtEnd(l1,3);
            l1.AddAtEnd(l1,3);
            l1.AddAtEnd(l1,4);
            l1.AddAtEnd(l1,5);
            var res = l1.removeDuplicates(l1.head);
            System.Console.WriteLine("Node Value:"+res);
            l1.printSingleLinkedList(l1.head);
        }
    }
}