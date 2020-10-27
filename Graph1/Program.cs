using System;
using System.Collections.Generic;

namespace Graph1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Вы хотите ввести свой список соседей, или использовать пример? " +
                              "1 - ввести свой" +
                              " 2 - использовать пример");

            var choice = int.Parse(Console.ReadLine());

            Graph graph = new Graph();

            if (choice == 1)
            {
                Console.WriteLine("Введите количество вершин графа");
                var count = int.Parse(Console.ReadLine());

                var adjList = new int[count][];

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Введите размер текущей строки");
                    var c = int.Parse(Console.ReadLine());
                    adjList[i] = new int[c];
                    Console.WriteLine("Введите список соседей (строка {0} )", i);
                    var enterString = Console.ReadLine();
                    var massiveString = enterString.Split(' ');

                    for (int j = 0; j < massiveString.Length; j++)
                    {
                        adjList[i][j] = int.Parse(massiveString[j]);
                    }
                }

                graph.setDots(adjList);
            }
            else
            {
                int[][] adjList =
                {
                    new[] {1, 2},
                    new[] {0, 3, 4},
                    new[] {0, 6},
                    new[] {1, 5},
                    new[] {1},
                    new[] {3},
                    new[] {2, 7, 8},
                    new[] {6},
                    new[] {6}
                };
                graph.setDots(adjList);
            }


            Console.WriteLine("Матрица смежности - ");

            var adjMatr = graph.getGraphAsAdjacencyMatrix();

            for (var i = 0; i < adjMatr.GetLength(0); i++)
            {
                Console.WriteLine();
                for (var j = 0; j < adjMatr.GetLength(1); j++)
                {
                    Console.Write(adjMatr[i, j] + " ");
                }
            }

            var bfs = graph.bfs();

            
            Console.WriteLine(" \n Bfs result - ");
            foreach (var el in bfs)
            {
                Console.Write("{0}, ", el);
            }
            
            var dfs = graph.dfs();
            
            Console.WriteLine("\n Dfs result - ");
            foreach (var el in dfs)
            {
                Console.Write("{0}, ", el);
            }
            
            var waveRes = graph.waveAlgorithm(8,0);
            
            Console.WriteLine("\n WaveAlgorithm result - ");
            foreach (var VARIABLE in waveRes)
            {
                Console.Write("{0}, ", VARIABLE);
            }
        }
    }

    class Graph
    {
        private int[][] _dots;

        public Graph()
        {
        }

        public Graph(int[][] dots)
        {
            _dots = dots;
        }

        public void setDots(int[][] dots)
        {
            _dots = dots;
        }

        public int[,] getGraphAsAdjacencyMatrix()
        {
            var length = _dots.Length;

            var result = new int[length, length];

            for (int i = 0; i < _dots.Length; i++)
            {
                for (int j = 0; j < _dots[i].Length; j++)
                {
                    var ind = _dots[i][j];
                    result[i, ind] = 1;
                }
            }

            return result;
        }

        public List<int> bfs()
        {
            var adjacencyMatrix = getGraphAsAdjacencyMatrix();
            var queue = new Queue<int>(); //Это очередь, хранящая номера вершин
            var u = 0; //Точка, с которой начинаем
            var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
            var bfsResult = new List<int>();


            visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

            queue.Enqueue(u);
            while (queue.Count != 0)
            {
                u = queue.Dequeue();
                bfsResult.Add(u);

                for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                {
                    if (Convert.ToBoolean(adjacencyMatrix[u, i]))
                    {
                        if (!visited[i])
                        {
                            visited[i] = true;
                            queue.Enqueue(i);
                        }
                    }
                }
            }

            return bfsResult;
        }

        public List<int> dfs()
        {
            var adjacencyMatrix = getGraphAsAdjacencyMatrix();
            var stack = new Stack<int>(); //Это стэк, хранящий номера вершин
            var u = 0; //Точка, с которой начинаем
            var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
            var dfsResult = new List<int>();


            visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

            stack.Push(u);

            while (stack.Count != 0)
            {
                u = stack.Pop();
                dfsResult.Add(u);

                for (int i = adjacencyMatrix.GetLength(0) - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(adjacencyMatrix[u, i]))
                    {
                        if (!visited[i])
                        {
                            visited[i] = true;
                            stack.Push(i);
                        }
                    }
                }
            }

            return dfsResult;
        }


        public List<int> waveAlgorithm(int from, int to)
        {
            var adjacencyMatrix = getGraphAsAdjacencyMatrix();
            var queue = new Queue<int>(); //Это очередь, хранящая номера вершин
            int u = 0; //Точка, с которой начинаем
            var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
            var bfsResult = new List<int>();

            var wave = new int[adjacencyMatrix.GetLength(0)];

            visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

            queue.Enqueue(u);

            while (queue.Count != 0)
            {
                u = queue.Dequeue();

                bfsResult.Add(u);


                for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                {
                    if (Convert.ToBoolean(adjacencyMatrix[u, i]))
                    {
                        if (!visited[i])
                        {
                            visited[i] = true;
                            queue.Enqueue(i);
                            wave[i] = u;
                        }
                    }
                }
            }

            var result = new List<int>();
            var parent = wave[from];
            while (true)
            {
                result.Add(parent);
                parent = wave[parent];
                if (parent == to)
                {
                    result.Add(to);
                    break;
                }
            }

            return result;
        }
    }
}