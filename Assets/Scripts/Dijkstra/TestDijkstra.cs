using System;
using System.Collections.Generic;
using System.Diagnostics;

using IntPair = System.ValueTuple<int, int>;



namespace Test
{

    class IntGraph : IWGraph<int>
    {

        public int getW(int node)
        {
            if (node % 2 == 0)
                return 1;
            else
                return 2;
        }

        public IEnumerable<int> Neighbors(int node)
        {
            yield return node + 1;
            yield return node + 2;
            yield return node - 1;
        }
    }

    class IntPairGraph : IWGraph<IntPair>
    {
        public int getW((int, int) node)
        {
            if (node.Item1 % 2 == 0 && node.Item2 % 2 == 0)
                return 1;
            else
                return 2;
        }

        public IEnumerable<IntPair> Neighbors(IntPair node)
        {
            yield return (node.Item1, node.Item2 + 1);
            yield return (node.Item1, node.Item2 - 1);
            yield return (node.Item1 + 1, node.Item2);
            yield return (node.Item1 - 1, node.Item2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Dijkstra Test");

            Console.WriteLine("  ");
            var intGraph = new IntGraph();
            var path = Dijkstra.GetPath(intGraph, 90, 95);
            Console.WriteLine("Test No 1 ");
            var pathString = String.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "90,92,94,95");
            path = Dijkstra.GetPath(intGraph, 85, 80);
            pathString = string.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "85,84,83,82,81,80");

            var intPairGraph = new IntPairGraph();
            var path2 = Dijkstra.GetPath(intPairGraph, (9, 5), (7, 6));
            pathString = string.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "(9, 5),(8, 5),(8, 6),(7, 6)");

            // Here we should get an empty path because of maxiterations:
            //int maxiterations = 1000;
            path2 = Dijkstra.GetPath(intPairGraph, (9, 5), (-7, -6));
            pathString = string.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "");

            Console.WriteLine("End Dijkstra Test");
        }
    }

}