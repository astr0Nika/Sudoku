using Sudoku;

var sudoku = new SudokuLogic();
FillExample(sudoku);

//PrintSudoku();
//Console.WriteLine();

Console.WriteLine("Please wait...");
bool result = sudoku.GetStarted();

if(!result)
{
    Console.WriteLine("Table couldn't be solved.");
}
else
{
    Console.Clear();
    Console.WriteLine("Table is solved!");
}

Console.WriteLine();
PrintSudoku();

void FillExample(SudokuLogic sudoku)
{
    sudoku.SetValue(0, 1, 3);
    sudoku.SetValue(1, 3, 1);
    sudoku.SetValue(1, 4, 9);
    sudoku.SetValue(1, 5, 5);
    sudoku.SetValue(2, 2, 8);
    sudoku.SetValue(2, 7, 6);

    sudoku.SetValue(3, 0, 8);
    sudoku.SetValue(3, 4, 6);
    sudoku.SetValue(4, 0, 4);
    sudoku.SetValue(4, 3, 8);
    sudoku.SetValue(4, 8, 1);
    sudoku.SetValue(5, 4, 2);

    sudoku.SetValue(6, 1, 6);
    sudoku.SetValue(6, 6, 2);
    sudoku.SetValue(6, 7, 8);
    sudoku.SetValue(7, 3, 4);
    sudoku.SetValue(7, 4, 1);
    sudoku.SetValue(7, 5, 9);
    sudoku.SetValue(7, 8, 5);
    sudoku.SetValue(8, 7, 7);
}

void PrintSudoku()
{
    string GetContent(int y, int x)
    {
        int result = sudoku.GetValue(y, x);
        return result == 0 ? " " : result.ToString();
    }

    Console.WriteLine(" |-----------------------|");

    Console.WriteLine($" | {GetContent(0, 0)} {GetContent(0, 1)} {GetContent(0, 2)} | {GetContent(0, 3)} {GetContent(0, 4)} {GetContent(0, 5)} | {GetContent(0, 6)} {GetContent(0, 7)} {GetContent(0, 8)} |");
    Console.WriteLine($" | {GetContent(1, 0)} {GetContent(1, 1)} {GetContent(1, 2)} | {GetContent(1, 3)} {GetContent(1, 4)} {GetContent(1, 5)} | {GetContent(1, 6)} {GetContent(1, 7)} {GetContent(1, 8)} |");
    Console.WriteLine($" | {GetContent(2, 0)} {GetContent(2, 1)} {GetContent(2, 2)} | {GetContent(2, 3)} {GetContent(2, 4)} {GetContent(2, 5)} | {GetContent(2, 6)} {GetContent(2, 7)} {GetContent(2, 8)} |");
    Console.WriteLine(" |-----------------------|");

    Console.WriteLine($" | {GetContent(3, 0)} {GetContent(3, 1)} {GetContent(3, 2)} | {GetContent(3, 3)} {GetContent(3, 4)} {GetContent(3, 5)} | {GetContent(3, 6)} {GetContent(3, 7)} {GetContent(3, 8)} |");
    Console.WriteLine($" | {GetContent(4, 0)} {GetContent(4, 1)} {GetContent(4, 2)} | {GetContent(4, 3)} {GetContent(4, 4)} {GetContent(4, 5)} | {GetContent(4, 6)} {GetContent(4, 7)} {GetContent(4, 8)} |");
    Console.WriteLine($" | {GetContent(5, 0)} {GetContent(5, 1)} {GetContent(5, 2)} | {GetContent(5, 3)} {GetContent(5, 4)} {GetContent(5, 5)} | {GetContent(5, 6)} {GetContent(5, 7)} {GetContent(5, 8)} |");
    Console.WriteLine(" |-----------------------|");

    Console.WriteLine($" | {GetContent(6, 0)} {GetContent(6, 1)} {GetContent(6, 2)} | {GetContent(6, 3)} {GetContent(6, 4)} {GetContent(6, 5)} | {GetContent(6, 6)} {GetContent(6, 7)} {GetContent(6, 8)} |");
    Console.WriteLine($" | {GetContent(7, 0)} {GetContent(7, 1)} {GetContent(7, 2)} | {GetContent(7, 3)} {GetContent(7, 4)} {GetContent(7, 5)} | {GetContent(7, 6)} {GetContent(7, 7)} {GetContent(7, 8)} |");
    Console.WriteLine($" | {GetContent(8, 0)} {GetContent(8, 1)} {GetContent(8, 2)} | {GetContent(8, 3)} {GetContent(8, 4)} {GetContent(8, 5)} | {GetContent(8, 6)} {GetContent(8, 7)} {GetContent(8, 8)} |");
    Console.WriteLine(" |-----------------------|");
}