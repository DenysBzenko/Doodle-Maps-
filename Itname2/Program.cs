using Itname2;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 35,
    Width = 90,
    Seed = 100,
    Noise = (float)0.3
});

string[,] map = generator.Generate();
new MapPrinter().Print(map);