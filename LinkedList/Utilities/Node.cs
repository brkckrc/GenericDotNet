using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList.Utilities
{
    public class Node
    {
        public MyTestClass Data { get; set; }
        public Node Next { get; set; }

        public Node(MyTestClass data)
        {
            Data = data;
            Next = null;
        }
    }
}
