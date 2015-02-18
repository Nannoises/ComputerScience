using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinCutMaxFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new int[6,6];
            network[0, 1] = 4;
            network[0, 2] = 10;
            network[1, 4] = 4;
            network[2, 3] = 4;
            network[2, 4] = 13;
            network[4, 5] = 10;
            network[3, 5] = 4;

            var flow = MinCutMaxFlow.GetMinCutMaxFlowGraph(network);
            MinCutMaxFlow.PrintNetwork(flow);
            Console.WriteLine("Min cut max flow value: {0}", MinCutMaxFlow.GetMinCutMaxFlow(network));
            Console.ReadLine();
        }
    }
}
