using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Itname2;

bool IsEqual(Point a, Point b)
{
    return a.Column == b.Column && a.Row == b.Row; 
}

void PrintMapWithPath(string[,] map, List<Point> path)
{
    Point start = path.First();
    Point end = path.Last();

    foreach (Point p in path)
    {
        if (IsEqual(p,start))
        {
            map[p.Column, p.Row] = "A";
        }
        else if (IsEqual(p,end))
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

List<Point> GetNeighbourd(string[,] map, Point p)
{
    List<Point> result = new List<Point>();

    int px = p.Column;
    int py = p.Row;
    
    if (py + 1 < map.GetLength(1) && py + 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py + 1]))
    {
        result.Add(new Point(px, py + 1 ));
    }
    if (py - 1 < map.GetLength(1) && py - 1 >= 0 && px < map.GetLength(0) && px >= 0 && !IsWall(map[px, py - 1]))
    {
        result.Add(new Point(px, py - 1 ));
    }

    if (py < map.GetLength(1) && py >= 0 && px + 1 < map.GetLength(0) && px + 1 >= 0 && !IsWall(map[px + 1, py]))
    {
        result.Add(new Point(px + 1, py));
    }

    if (py < map.GetLength(1) && py >= 0 && px - 1 < map.GetLength(0) && px + 1 >= 0 && !IsWall(map[px - 1, py]))
    {
        result.Add(new Point(px - 1, py));
    }

    return result;
}

    List<Point> SearchBfs(string[,] map, Point start, Point end)
{
    Queue<Point> frointier = new Queue<Point>();
    Dictionary<Point, Point?> cameFrom = new Dictionary<Point, Point?>();
    
    cameFrom.Add(start, null);
        
    frointier.Enqueue(start);

    while (frointier.Count > 0)
    {
        Point cur = frointier.Dequeue();

        if (IsEqual(cur,end))
        {
            break;
        }
        foreach (Point neighbour in GetNeighbourd(map, cur))
        {
            if (cameFrom.TryGetValue(neighbour, out _))
            {
                cameFrom.Add(neighbour, cur);
                frointier.Enqueue(neighbour);
            }
        }
    }

    List<Point> path = new List<Point>();
    Point? current = end;

    while (IsEqual(current.Value, start))
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
    Height = 10,
    Width = 15,
    Seed = 7355608
});

string[,] map = generator.Generate();

/*foreach (Point n in GetNeighbourd(map, p))
{
    map[n.Column, n.Row] = "N";
}*/
Point start = new Point(0, 0);
Point end = new Point(0, 8);
List<Point> path = SearchBfs(map, start, end);
    
PrintMapWithPath(map,path);
//PrintMapWithPath(map,path);

//new MapPrinter().Print(map);