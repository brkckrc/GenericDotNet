using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList.Utilities
{
    public class MyLinkedList
    {
        private Node head;

        public void Add(MyTestClass data)
        {
            Node newNode = new Node(data);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public void Print()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine($"Id: {current.Data.Id}, Adi: {current.Data.Adi}, Aciklama: {current.Data.Aciklama}");
                current = current.Next;
            }
        }
    }
}
