using System;
using System.Collections.Generic;

namespace MinCutMaxFlow
{
    public static class MinCutMaxFlow
    {
        public static int GetMinCutMaxFlow(int[,] network)
        {
            var flow = GetMinCutMaxFlowGraph(network);
            return GetTotalFlow(flow);
        }

        public static int[,] GetMinCutMaxFlowGraph(int[,] network)
        {
            //Init empty flow
            var flow = new int[network.GetLength(0), network.GetLength(0)];

            var augmentingPath = GetAugmentingPath(network, flow);
            if (augmentingPath == null)
                return flow;

            while (augmentingPath != null)
            {                
                ApplyAugmentingPath(ref flow, augmentingPath);
                augmentingPath = GetAugmentingPath(network, flow);
            }

            return flow;
        }

        public static void PrintNetwork(int[,] network)
        {
            Console.WriteLine("----Print network-----");
            for (var i = 0; i < network.GetLength(0); i++)
            {
                for (var j = 0; j < network.GetLength(0); j++)
                {
                    if(network[i,j] > 0)
                        Console.WriteLine("Node ({0}) ---{1}---> Node ({2})", i, network[i,j], j);
                }
            }
        }

        //BFS of network to find sink while tracking path and bottleneck.
        //Then return flow change.
        private static int[,] GetAugmentingPath(int[,] network, int[,] flow)
        {
            var residualNetwork = GetResidualNetwork(network, flow);
            
            var previous = new int[network.GetLength(0)];
            var queue = new Queue<int>();
            queue.Enqueue(0);

            int current = 0;            

            var visited = new int[network.GetLength(0)];            

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                visited[current] = 1;

                //Found sink
                if (current == network.GetLength(0) - 1)
                    break;

                for (var i = 0; i < network.GetLength(0); i++)
                {
                    //Already visited
                    if(visited[i] == 1)
                        continue;

                    //Path exists
                    if (residualNetwork[current,i] > 0)
                    {
                        previous[i] = current;
                        queue.Enqueue(i);
                    }
                }
            }

            //No path found!
            if (current != network.GetLength(0) - 1)
                return null;

            var bottleneck = GetBottleNeck(residualNetwork, previous, current);

            var augmentingPath = new int[network.GetLength(0), network.GetLength(0)];
            while (current != 0)
            {
                //If forward move
                if (network[previous[current], current] > 0)
                {
                    augmentingPath[previous[current], current] = bottleneck;
                }
                else //backwards move
                {
                    augmentingPath[current, previous[current]] = -bottleneck;
                }
                current = previous[current];
            }

            return augmentingPath;
        }

        //Returns a residual network from a network and current flow.
        //WARNING: Residual network may by cyclical
        private static int[,] GetResidualNetwork(int[,] network, int[,] flow)
        {
            var residualNetwork = new int[network.GetLength(0), network.GetLength(0)];

            for (var i = 0; i < network.GetLength(0); i++)
            {
                for (var j = 0; j < network.GetLength(0); j++)
                {
                    if (network[i,j] > 0)
                    {
                        residualNetwork[j,i] = flow[i,j];
                        residualNetwork[i,j] = network[i,j] - flow[i,j];
                    }
                }
            }
            return residualNetwork;
        }

        private static void ApplyAugmentingPath(ref int[,] flow, int[,] augmentingPath)
        {
            for (var i = 0; i < flow.GetLength(0); i++)
            {
                for (var j = 0; j < flow.GetLength(0); j++)    
                    flow[i,j] += augmentingPath[i,j];                
            }
        }

        //Sums total flow from source. After no more augmenting paths can be found, this should be min cut max flow value
        private static int GetTotalFlow(int[,] flow)
        {
            var totalFlow = 0;
            for (var i = 0; i < flow.GetLength(0); i++)
            {
                totalFlow += flow[0, i];
            }
            return totalFlow;
        }

        //Calculates bottleneck for augmenting path
        private static int GetBottleNeck(int[,] network, int[] path, int sink)
        {
            var current = sink;
            var bottleneck = Int32.MaxValue;
            while (current != 0)
            {
                if (network[path[current], current] < bottleneck)
                    bottleneck = network[path[current], current];

                current = path[current];
            }

            return bottleneck;
        }
    }
}
