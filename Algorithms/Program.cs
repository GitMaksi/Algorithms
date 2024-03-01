using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Wybierz algorytm do przetestowania:");
            Console.WriteLine("1. Sortowanie bąbelkowe");
            Console.WriteLine("2. Przeszukiwanie grafu BFS");
            Console.WriteLine("3. Sortowanie przez wstawianie");
            Console.WriteLine("4. Przeszukiwanie binarne");
            Console.WriteLine("5. Algorytm Dijkstry");
            Console.WriteLine("6. Wyjście");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BubbleSortDemo();
                    break;
                case "2":
                    BfsDemo();
                    break;
                case "3":
                    InsertionSortDemo();
                    break;
                case "4":
                    BinarySearchDemo();
                    break;
                case "5":
                    DijkstraDemo();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór");
                    break;
            }
        }
    }

    private static void BubbleSortDemo()
    {
        int[] array = { 5, 3, 8, 4, 2 };
        BubbleSort(array);
        Console.WriteLine("Posortowana tablica: " + string.Join(", ", array));
    }

    private static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    private static void BfsDemo()
    {
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>> {
            { 0, new List<int> { 1, 2 } },
            { 1, new List<int> { 0, 3, 4 } },
            { 2, new List<int> { 0, 5 } },
            { 3, new List<int> { 1 } },
            { 4, new List<int> { 1, 5 } },
            { 5, new List<int> { 2, 4 } }
        };

        Console.WriteLine("Przeszukiwanie BFS zaczynając od wierzchołka 0:");
        BFS(graph, 0);
    }

    private static void BFS(Dictionary<int, List<int>> graph, int startNode)
    {
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(startNode);
        visited.Add(startNode);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            Console.WriteLine(node);

            foreach (var neighbor in graph[node])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
    private static void InsertionSortDemo()
    {
        int[] array = { 34, 8, 50, 3, 22 };
        InsertionSort(array);
        Console.WriteLine("Posortowana tablica (Insertion Sort): " + string.Join(", ", array));
    }

    private static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    private static void BinarySearchDemo()
    {
        int[] array = { 3, 8, 22, 34, 50 }; // Musi być posortowany
        int target = 22;
        int index = BinarySearch(array, target);

        if (index != -1)
            Console.WriteLine($"Element {target} znajduje się na pozycji: {index}");
        else
            Console.WriteLine($"Element {target} nie został znaleziony.");
    }

    private static int BinarySearch(int[] array, int target)
    {
        int low = 0, high = array.Length - 1;
        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            if (array[mid] == target)
                return mid;
            if (array[mid] < target)
                low = mid + 1;
            else
                high = mid - 1;
        }
        return -1;
    }

    private static void DijkstraDemo()
    {
        // Graf jako słownik par (wierzchołek, lista sąsiedztwa z wagami)
        var graph = new Dictionary<int, List<(int, int)>> {
            { 0, new List<(int, int)> { (1, 4), (2, 1) } },
            { 1, new List<(int, int)> { (3, 1) } },
            { 2, new List<(int, int)> { (1, 2), (3, 5) } },
            { 3, new List<(int, int)>() }
        };

        var distances = Dijkstra(graph, 0);
        Console.WriteLine("Najkrótsze ścieżki algorytmem Dijkstry od wierzchołka 0:");
        for (int i = 0; i < distances.Length; i++)
        {
            Console.WriteLine($"Do {i}: {distances[i]}");
        }
    }

    private static int[] Dijkstra(Dictionary<int, List<(int, int)>> graph, int startNode)
    {
        var distances = new int[graph.Count];
        for (int i = 0; i < distances.Length; i++)
            distances[i] = int.MaxValue;

        distances[startNode] = 0;
        var priorityQueue = new SortedSet<(int, int)>(Comparer<(int, int)>.Create((a, b) => a.Item2 != b.Item2 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)));
        priorityQueue.Add((startNode, 0));

        while (priorityQueue.Count != 0)
        {
            var (node, currentDistance) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentDistance > distances[node])
                continue;

            foreach (var (neighbor, weight) in graph[node])
            {
                int newDistance = distances[node] + weight;
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    priorityQueue.Add((neighbor, newDistance));
                }
            }
        }

        return distances;
    }
}
