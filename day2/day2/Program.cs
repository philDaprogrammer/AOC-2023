List<Game> parse (string filePath) {
    var lines = File.ReadAllLines(filePath);  
    List<Game> games = new List<Game>();
    
    List<Dictionary<string, int>> game;
    Dictionary<string, int> result; 
    int GameId; 
    
    foreach (var line in lines) {
        var GameIdSplit = line.Split(": "); 
        
        GameId = Int32.Parse(GameIdSplit[0].Split(" ")[1]); 
        game   = new List<Dictionary<string, int>>(); 
        
        foreach  (var cubes in GameIdSplit[1].Split("; ")) {
            result = new Dictionary<string, int>(); 

            foreach  (var cube in cubes.Split(", ")) {
                var cubeSplit = cube.Split(" ");
                result.Add(cubeSplit[1], Int32.Parse(cubeSplit[0])); 
            }

            game.Add(result);
        }

        games.Add(new Game(GameId, game)); 
    }

    return games;
}

void part1 (List<Game> games) {
    Dictionary<string, int> maxAmount = new Dictionary<string, int>() {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };

    int total = games
        .Where(game => !game.cubes.Any(c => c.Any(c => c.Value > maxAmount[c.Key])))
            .Sum(game => game.Id);

    Console.WriteLine($"part 1: {total}");  
}

int maxValue (Game game, string color) {
    return game.cubes
        .Where(c => c.ContainsKey(color))
        .Select(c => c[color])
        .Max();
}

int power (Game game) {
    return maxValue(game, "red") * maxValue(game, "green") * 
        maxValue(game, "blue"); 
}

void part2 (List<Game> games) {
    int total = games
        .Select(power)
        .Sum(); 

    Console.WriteLine($"part 2: {total}"); 
}

// driver code 
string filePath = "YOUR PATH HERE"; 

List<Game> games = parse(filePath);
part1(games);
part2(games); 

class Game {
    public int Id { get; set; }
    public List<Dictionary<string, int>> cubes { get; set; }

    public Game(int Id, List<Dictionary<string, int>> cubes) {
        this.Id = Id;
        this.cubes = cubes;
    }
}
