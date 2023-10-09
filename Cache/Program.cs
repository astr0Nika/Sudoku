

int nr1 = 1;
int nr2 = 2;

Console.WriteLine($"{nr1} {nr2}");

int cache = nr2;
nr2 = nr1;
nr1 = cache;

Console.WriteLine($"{nr1} {nr2}");
