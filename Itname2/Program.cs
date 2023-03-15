using System.Collections.Generic;using Itname2;

bool IsEqueal(Point a, Point b)
{
    return a.Column == b.Column && a.Row == b.Row;
}



void PrintMathWithPath(string[,] map, List<Point> path)
{
    Point start = path[0];
    Point end = path[^1];
    
    foreach (Point p in path )
    {
        if (IsEqueal (p.== start))
        {
            map[p.Column, p.Row] = "A";
        }else if (IsEqueal (p.== end))
        {
            map[p.Column, p.Row] = "B";
        }else 

        {
            map[p.Column, p.Row] = ".";
        }
    }
    new MapPrinter().Print(map);
}

bool ISndnnfnf(string c)
{
    return c == "█";
}
List<Point> GetNeighbourd(string[,] map, Point p)
{
    List<Point> result = new List<Point>();
    int px = p.Column;
    int py = p.Row;

    if (py + 1 < map.GetLength(0) && py + 1 >= 0 && px < map.GetLength(1) && px >= 0 && ISndnnfnf(map[px, py + 1]))
    {
        result.Add(new Point(px, py +1 ));
    }
    if (py - 1 < map.GetLength(0) && py - 1 >= 0 && px < map.GetLength(1) && px >= 0 && ISndnnfnf(map[px, py - 1]))
    {
        result.Add(new Point(px, py - 1 ));
    }

    if (py < map.GetLength(0) && py >= 0 && px + 1 < map.GetLength(1) && px + 1 >= 0 && ISndnnfnf(map[px + 1, py]))
    {
        result.Add(new Point(px + 1, py));
    }

    if (py  < map.GetLength(0) && py  >= 0 && px - 1 < map.GetLength(1) && px+ 1 >= 0 && ISndnnfnf(map[px - 1, py ]))
    {
        result.Add(new Point(px - 1, py  ));

    return result;
    
}}
List<Point> Search(string[,] map, Point start, Point end)
{
    Queue<Point> fronting = new Queue<Point>();
    fronting.Enqueue(start);

    while (fronting.Count > 0)
    {
        Point cut  = fronting.Enqueue();
        if (IsEqueal((cut. == end ))
        {
            break;
        }
        
    }
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
    Seed = 12312
    
});
List<Point> path = new List<Point>(new List<Point>()
{
    new Point(0,1 ),
    new Point (1,0),
    new Point(2,0),
    new Point(3,0)
});


string[,] map = generator.Generate();
new MapPrinter().Print(map);