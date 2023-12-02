var input = File.ReadAllLines("C:\\Projects\\day1_2023\\day1_2023\\input.txt");

void solve(string[] lines) {
    int total = 0;
    string? first;
    string? last;
    int result;

    foreach (var line in lines) {
        first = null;
        last  = null;

        foreach (var c in line)	{
            if (first == null && Char.IsDigit(c)) {
                first = c.ToString(); 
            } else if (Char.IsDigit(c)){
                last = c.ToString(); 
            }
        }

        result = last != null
            ? Int32.Parse(first + last)
            : Int32.Parse(first + first);

        total += result;  
    }

    Console.WriteLine($"Total: {total}");
}

string transform(string line)
{    
    return line.Replace("one", "o1e")
        .Replace("two", "t2o").Replace("three", "t3e")
        .Replace("four", "f4r").Replace("five", "f5e")
        .Replace("six", "s6x").Replace("seven", "s7v")
        .Replace("eight", "e8t").Replace("nine", "n9e");
}

solve(input);
solve(input.Select(transform).ToArray()); 
