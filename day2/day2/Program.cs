using System.Data;

class Game
{
    public int Id { get; set; }
    public List<Dictionary<string, int>> cubes { get; set; }

    public Game(int Id, List<Dictionary<string, int>> cubes)
    {
        this.Id = Id;
        this.cubes = cubes;
    }
}

class Solve
{
    static List<Game> parse (string filePath)
    {
        var lines        = File.ReadAllLines(filePath);  
        List<Game> games = new List<Game>();
        List<Dictionary<string, int>> game;
        int GameId; 
        
        foreach (var line in lines)
        {
            var GameIdSplit = line.Split(": ");

            GameId = Int32.Parse(GameIdSplit[0].Split(" ")[1]); 
            game   = new List<Dictionary<string, int>>(); 
            
            foreach  (var rolls in GameIdSplit[1].Split("; "))
            {
                var cubes = new Dictionary<string, int>(); 

                foreach  (var cube in rolls.Split(", "))
                {
                    var cubeSplit = cube.Split(" ");
                    cubes.Add(cubeSplit[1], Int32.Parse(cubeSplit[0])); 
                }

                game.Add(cubes);
            }

            games.Add(new Game(GameId, game)); 
        }

        return games;
    }

    static void part1 (List<Game> games)
    {
        Dictionary<string, int> total = new Dictionary<string, int>() {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        int validGames = 0; 
        
        foreach (var game in games)
        {
            var invalid = game.cubes.Any(c => c.Any(c => c.Value > total[c.Key]));

            if (!invalid) { validGames += game.Id; }     
        }

        Console.WriteLine($"total: {validGames}"); 
    }

    static void part2 (List<Game> games)
    {
        int total = 0; 
        
        foreach (var game in games)
        {
            int red   = game.cubes.Where(c => c.ContainsKey("red")).Select(c => c["red"]).Max();
            int green = game.cubes.Where(c => c.ContainsKey("green")).Select(c => c["green"]).Max();
            int blue  = game.cubes.Where(c => c.ContainsKey("blue")).Select(c => c["blue"]).Max(); 

            total += red * green * blue; 
        }

        Console.WriteLine($"total: {total}"); 
    }

    public static void Main()
    {
        string filePath  = "C:\\Projects\\AOC\\2023\\day2\\day2\\input.txt"; 
        
        List<Game> games = parse(filePath);
        part1(games);
        part2(games); 
    }
}
