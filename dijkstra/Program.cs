using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
{
    public class Program
    {
        static void Main(string[] args)
        {
            SimpleGraph grafo = new SimpleGraph(5);

            grafo.SetWeight(1, 2, 1);
            grafo.SetWeight(1, 3, 2);
            grafo.SetWeight(2, 4, 3);
            grafo.SetWeight(3, 4, 4);
            grafo.SetWeight(4, 5, 5);

            //vertici
            Console.WriteLine("Vertici:");
            foreach (int vertex in grafo.GetVertices())
            {
                Console.WriteLine(vertex);
            }

            int verticeSelezionato;
            Console.WriteLine("Inserire un Vertice per vedere i vicini.");
            verticeSelezionato = int.Parse(Console.ReadLine());

            Console.WriteLine("Vicini di: " + verticeSelezionato);
            foreach (int neighbor in grafo.GetNeighbors(verticeSelezionato))
            {
                Console.WriteLine(neighbor);
            }

            int v1, v2;
            Console.WriteLine("Inserire vertice partenza.");
            v1= int.Parse(Console.ReadLine());
            Console.WriteLine("Inserire vertice di arrivo.");
            v2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Peso dell'arco tra " +v1+ " e " + v2 + " è:" + grafo.GetWeight(v1, v2));
            List<int> ShortestPath = grafo.Dijkstra(1,4);
            foreach (int vertice in ShortestPath)
            {
                Console.Write(vertice + ", ");
            }
            
            Console.ReadKey();

        }


    }
    public interface IGraph<VertexType>
    {
        void SetWeight(VertexType u, VertexType v, double w);
        List<VertexType> GetVertices();
        List<VertexType> GetNeighbors(VertexType vertex);
        double GetWeight(VertexType u, VertexType v);
    }

    public class SimpleGraph : IGraph<int>
    {
        private const double NF = double.PositiveInfinity;
        private double[,] Table;

        public SimpleGraph(int numVertices)
        {
            Table = new double[numVertices, numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    Table[i, j] = NF;
                }
            }
        }

        public List<int> GetVertices()
        {
            var result = new List<int>();

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                result.Add(i + 1);
            }

            return result;
        }
        public List<int> GetNeighbors(int vertex)
        {
            var result = new List<int>();

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                if (Table[vertex - 1, i] < NF && vertex - 1 != i)
                {
                    result.Add(i + 1);
                }
            }

            return result;
        }

        public void SetWeight(int u, int v, double w)
        {
            Table[u - 1, v - 1] = w;
        }

        public double GetWeight(int u, int v)
        {
            return Table[u - 1, v - 1];
        }

        public List<int> Dijkstra(int VettorePartenza, int VettoreFine)
        {
            Dictionary<int, double> distanza = new Dictionary<int, double>();
            Dictionary<int, int> precedente = new Dictionary<int, int>();
            List<int> nonPassato = new List<int>();
            int corrente = 0;

            foreach (int vertice in GetVertices())
            {
                if (vertice == VettorePartenza)
                {
                    distanza[vertice] = 0;
                }
                else
                {
                    distanza[vertice] = double.PositiveInfinity;
                }

                precedente[vertice] = -1;
                nonPassato.Add(vertice);
            }


            double distanzaMin;
            while (nonPassato.Count() > 0)
            {
                for (int i = 0; i < nonPassato.Count(); i++)
                {
                    corrente = nonPassato[i];
                    distanzaMin = distanza[corrente];
                    if (distanza[corrente] < distanzaMin)
                    {
                        corrente = nonPassato[i];
                    }
                }
            }



            List<int> camminoMinimo = new List<int>();
            corrente = VettoreFine;

            camminoMinimo.Insert(0, corrente);
            corrente = precedente[corrente];

            return camminoMinimo;
        }


    }
}
