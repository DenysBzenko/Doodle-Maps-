using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Itname2;

bool IsEqual(Itname2.Point a, Itname2.Point b)
{
    return a.Column == b.Column && a.Row == b.Row;
}

void PrintMapWithPath(string[,] map, List<Itname2.Point> path)
{
    Itname2.Point start = path[0];
    Itname2.Point end = path.Last();

    foreach (Itname2.Point p in path)
    {
        if (IsEqual(p, start))
        {
            map[p.Column, p.Row] = "A";
        }
        else if (IsEqual(p, end))
        {
            map[p.Column, p.Row] = "B";
        }
        else
        {
            map[p.Column, p.Row] = ".";
        }
    }
    new MapPrinter().Print(map);
}

bool IsWall(string c)
{
    return c == "█";
}

List<Itname2.Point> GetShortestPath(string[,] map, Itname2.Point start, Itname2.Point end)
{
    int[,] dist = new int[map.GetLength(0), map.GetLength(1)];
    for(int i = 0; i < map.GetLength(0); ++i)
    {
        for(int j = 0; j < map.GetLength(1); ++j)
        {
            dist[i, j] = 100000;
        }
    }
    Itname2.Point[,] prev = new Itname2.Point[map.GetLength(0), map.GetLength(1)];

    PriorityQueue<Itname2.Point, int> q = new PriorityQueue<Itname2.Point, int>();
    dist[start.Column, start.Row] = 0;
    prev[start.Column, start.Row] = new Itname2.Point(-1, -1);
    q.Enqueue(new Itname2.Point(start.Column, start.Row), 0);

    while(q.TryDequeue(out Itname2.Point from, out int len))
    {
        //Console.WriteLine("{0} {1} {2}", from.Column, from.Row, len);
        foreach (Itname2.Point neighbour in GetNeighbourd(map, from))
        {
            if (dist[neighbour.Column, neighbour.Row] > len + 1)
            {
                dist[neighbour.Column, neighbour.Row] = len + 1;
                prev[neighbour.Column, neighbour.Row] = from;
                q.Enqueue(new Itname2.Point(neighbour.Column, neighbour.Row), dist[neighbour.Column, neighbour.Row]);
            }
        }
    }

    List<Itname2.Point> path = new List<Itname2.Point>();
    Itname2.Point current = end;

    while (!IsEqual(current, start))
    {
        path.Add(current);
        current = prev[current.Column, current.Row];
    }
    path.Add(start);

    path.Reverse();
    return path;
}

List<Itname2.Point> GetNeighbourd(string[,] map, Itname2.Point p)
{
    List<Itname2.Point> result = new List<Itname2.Point>();

    int px = p.Column;
    int py = p.Row;

    if (py + 1 < map.GetLength(1) && py + 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py + 1]))
    {
        result.Add(new Itname2.Point(px, py + 1));
    }

    if (py - 1 < map.GetLength(1) && py - 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py - 1]))
    {
        result.Add(new Itname2.Point(px, py - 1));
    }

    if (py < map.GetLength(1) && py >= 0 && px + 1 < map.GetLength(0) && px + 1 >= 0 && !IsWall(map[px + 1, py]))
    {
        result.Add(new Itname2.Point(px + 1, py));
    }

    if (py < map.GetLength(1) && py >= 0 && px - 1 < map.GetLength(0) && px - 1 >= 0 && !IsWall(map[px - 1, py]))
    {
        result.Add(new Itname2.Point(px - 1, py));
    }
    return result;
}

List<Itname2.Point> SearchBfs(string[,] map, Itname2.Point start, Itname2.Point end)
{
    Queue<Itname2.Point> frontier = new Queue<Itname2.Point>();
    Dictionary<Itname2.Point, Itname2.Point?> cameFrom = new Dictionary<Itname2.Point, Itname2.Point?>();

    cameFrom.Add(start, null);
    frontier.Enqueue(start);

    while (frontier.Count > 0)
    {
        Itname2.Point cur = frontier.Dequeue();

        if (IsEqual(cur, end))
        {
            break;
        }
        foreach (Itname2.Point neighbour in GetNeighbourd(map, cur))
        {
            if (!cameFrom.TryGetValue(neighbour, out _))
            {
                cameFrom.Add(neighbour, cur);
                frontier.Enqueue(neighbour);
            }
        }
    }

    List<Itname2.Point> path = new List<Itname2.Point>();
    Itname2.Point? current = end;

    while (!IsEqual(current.Value, start))
    {
        path.Add(current.Value);
        cameFrom.TryGetValue(current.Value, out current);
    }
    path.Add(start);

    path.Reverse();
    return path;
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 25,
    Width = 50,
    Seed = 7355608
});

string[,] map = generator.Generate();

Itname2.Point start = new Itname2.Point(6, 2);
Itname2.Point end = new Itname2.Point(48, 24);
List<Itname2.Point> path = GetShortestPath(map, start, end);

PrintMapWithPath(map, path);