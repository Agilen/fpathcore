using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ffpath
{
    class Algor
    {
        public void Ping(int n, int st, int[,] w, int end, List<string> IPA)
        {
            string writePath = "\\Users\\fonta\\source\\repos\\Ffpath\\Ffpath\\Data\\res.txt";
            string[] Dd = new string[n];
            //string buf = "";
            bool[,] visited = new bool[n, n];
            int[,] D = new int[n, n];

            for (int k = 0; k < IPA.Count(); k++)
            {
                for (int i = 0; i < n; i++)
                {
                    Dd[i] = "";
                    D[k, i] = w[k, i];
                    visited[k, i] = false;
                }

                D[k, k] = 0;
                int index = 0, u = 0;

                for (int i = 0; i < n; i++)
                {
                    int min = int.MaxValue;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[k, j] && D[k, j] < min)
                        {
                            min = D[k, j];
                            index = j;
                        }
                    }

                    u = index;
                    visited[k, u] = true;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[k, j] && w[u, j] != int.MaxValue && D[k, u] != int.MaxValue && (D[k, u] + w[u, j] < D[k, j]))
                        {
                            D[k, j] = D[k, u] + w[u, j];
                            //buf = u.ToString() + " -> ";
                            //Dd[j] += Dd[u]+u.ToString() + " -> ";
                            //buf = " ";
                        }
                    }
                }
            }
            string text = "";
            if (D[st, end] != int.MaxValue)
            {
                text = $"Обмен пакетами с {IPA[end]} по 32 байт: \n" +
                    $" Ответ от {IPA[end]}: число байт=32 время={(D[st, end] + D[end, st])} TTL=32" +
                  $"\n Ответ от {IPA[end]}: число байт=32 время={(D[st, end] + D[end, st])} TTL=32" +
                  $"\n Ответ от {IPA[end]}: число байт=32 время={(D[st, end] + D[end, st])} TTL=32" +
                  $"\n Ответ от {IPA[end]}: число байт=32 время={(D[st, end] + D[end, st])} TTL=32";
            }
            else if (D[st, end] == int.MaxValue)
                text = $"Обмен пакетами с {IPA[end]} по 32 байт:\n" +
                    $"\n Ответ от {IPA[end]}: маршрут недоступен" +
                    $"\n Ответ от {IPA[end]}: маршрут недоступен" +
                    $"\n Ответ от {IPA[end]}: маршрут недоступен" +
                    $"\n Ответ от {IPA[end]}: маршрут недоступен";

            using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                sw.WriteLine(text);
            }

            using (StreamReader sr = new StreamReader(writePath, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public void Dijkstra(int n, int st, int[,] w, List<string> IPA)
        {
            string[] Dd = new string[n];
            //string buf = "";
            bool[,] visited = new bool[n, n];
            int[,] D = new int[n, n];
            for (int k = 0; k < IPA.Count(); k++)
            {
                for (int i = 0; i < n; i++)
                {
                    Dd[i] = "";
                    D[k, i] = w[k, i];
                    visited[k, i] = false;
                }

                D[k, k] = 0;
                int index = 0, u = 0;

                for (int i = 0; i < n; i++)
                {
                    int min = int.MaxValue;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[k, j] && D[k, j] < min)
                        {
                            min = D[k, j];
                            index = j;
                        }
                    }

                    u = index;
                    visited[k, u] = true;

                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[k, j] && w[u, j] != int.MaxValue && D[k, u] != int.MaxValue && (D[k, u] + w[u, j] < D[k, j]))
                        {
                            D[k, j] = D[k, u] + w[u, j];
                            //buf = u.ToString() + " -> ";
                            //Dd[j] += Dd[u]+u.ToString() + " -> ";
                            //buf = " ";
                        }
                    }
                }
            }
            Console.WriteLine("Стоимость пути из начальной вершины до остальных(Алгоритм Дейкстры):\t\n");

            int[] b = new int[n * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (D[i, j] < int.MaxValue)
                    {
                        b[j * i + j] = D[i, j];
                    }
                }
            }
            int max = b.Max();



            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (D[i, j] != int.MaxValue && D[i, j] == max)
                        Console.WriteLine((IPA[i]) + " -> " /*+ Dd[i]*/ + (IPA[j]) + " = " + (D[i, j] + D[j, i]));
                    
                }
            }

        }
    }
    
}
