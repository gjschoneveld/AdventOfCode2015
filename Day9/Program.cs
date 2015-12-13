using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day9
{
    class Edge
    {
        public string[] vertices;
        public int distance;

        public bool HasVertex(string v)
        {
            return vertices.Contains(v);
        }

        public string OtherVertex(string v)
        {
            if (vertices[0] == v)
            {
                return vertices[1];
            }

            return vertices[0];
        }

        public static Edge Parse(string x)
        {
            // Faerun to Tristram = 65
            var parts = x.Split(' ');

            var result = new Edge
            {
                vertices = new string[] { parts[0], parts[2] },
                distance = int.Parse(parts[4])
            };

            return result;
        }
    }

    class Program
    {
        static Tuple<int, int> VisitVertex(string v)
        {
            visited.Add(v);

            var edges = connections[v];

            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var e in edges)
            {
                string neighbour = e.OtherVertex(v);
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                var remainingLength = VisitVertex(neighbour);
                int minLength = e.distance + remainingLength.Item1;
                int maxLength = e.distance + remainingLength.Item2;

                min = Math.Min(min, minLength);
                max = Math.Max(max, maxLength);
            }

            visited.Remove(v);

            if (min == int.MaxValue)
            {
                return new Tuple<int, int>(0, 0);
            }
            else
            {
                return new Tuple<int, int>(min, max);
            }
        }

        static Tuple<int, int> VisitAll()
        {
            visited = new HashSet<string>();

            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var v in connections.Keys)
            {
                var distance = VisitVertex(v);
                min = Math.Min(min, distance.Item1);
                max = Math.Max(max, distance.Item2);
            }

            return new Tuple<int, int>(min, max);
        }

        static HashSet<string> visited;
        static Dictionary<string, List<Edge>> connections;

        static void Main(string[] args)
        {
            string input = "Faerun to Tristram = 65\nFaerun to Tambi = 129\nFaerun to Norrath = 144\nFaerun to Snowdin = 71\nFaerun to Straylight = 137\nFaerun to AlphaCentauri = 3\nFaerun to Arbre = 149\nTristram to Tambi = 63\nTristram to Norrath = 4\nTristram to Snowdin = 105\nTristram to Straylight = 125\nTristram to AlphaCentauri = 55\nTristram to Arbre = 14\nTambi to Norrath = 68\nTambi to Snowdin = 52\nTambi to Straylight = 65\nTambi to AlphaCentauri = 22\nTambi to Arbre = 143\nNorrath to Snowdin = 8\nNorrath to Straylight = 23\nNorrath to AlphaCentauri = 136\nNorrath to Arbre = 115\nSnowdin to Straylight = 101\nSnowdin to AlphaCentauri = 84\nSnowdin to Arbre = 96\nStraylight to AlphaCentauri = 107\nStraylight to Arbre = 14\nAlphaCentauri to Arbre = 46";

            //string input = "London to Dublin = 464\nLondon to Belfast = 518\nDublin to Belfast = 141";

            string[] roadStrings = input.Split('\n');

            var edges = roadStrings.Select(rs => Edge.Parse(rs)).ToList();

            var vertices = edges.SelectMany(e => e.vertices).Distinct().ToList();

            connections = new Dictionary<string, List<Edge>>();
            foreach (var v in vertices)
            {
                var links = edges.Where(e => e.HasVertex(v)).ToList();
                connections.Add(v, links);
            }

            var distance = VisitAll();

            Console.WriteLine("Answer 1: {0}", distance.Item1);
            Console.WriteLine("Answer 2: {0}", distance.Item2);

            Console.ReadKey();
        }
    }
}
