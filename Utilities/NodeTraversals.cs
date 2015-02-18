using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public static class NodeTraversals
    {
        public static void DepthFirstTraversal(INode root, Action<INode> visit, Action<INode> leave)
        {
            if(root == null)
                return;

            if(visit != null)
                visit.Invoke(root);

            if (root.Children == null)
                return;

            foreach (var child in root.Children)
            {
                DepthFirstTraversal(child, visit, leave);
            }

            if(leave != null)
                leave.Invoke(root);
        }

        public static void NonRecursiveDepthFirstTraversal(INode root, Action<INode> visit)
        {
            if (root == null)
                return;

            var nodeStack = new Stack<INode>();
            nodeStack.Push(root);

            while (nodeStack.Count > 0)
            {
                var current = nodeStack.Pop();
                if (visit != null)
                    visit.Invoke(current);

                if (current.Children != null && current.Children.Count > 0)
                {                    
                    //This results in right-first DFS, reverse children for left-first
                    foreach (var child in current.Children)
                    {
                        nodeStack.Push(child);
                    }
                }
            }
        }

        public static void BreadthFirstSearch(INode root, Action<INode> visit)
        {
            if (root == null)
                return;

            var queue = new Queue<INode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if(visit != null)
                    visit.Invoke(current);

                if (current.Children != null)
                {
                    foreach (var child in current.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public static void RecursieBreadthFirstSearch(INode root, Action<INode> visit)
        {
            if (root == null)
                return;

            if (root.Children == null)
                return;

            foreach (var child in root.Children)
            {
                RecursieBreadthFirstSearch(child, visit);
            }
        }
    }
}
