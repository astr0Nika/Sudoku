namespace Sudoku;

public class SudokuLogic
{
    private int[,] _arr2dim = new int[9, 9];

    public int GetValue(int y, int x)
    {
        return _arr2dim[y, x];
    }

    public void SetValue(int y, int x, int value)
    {
        if (_arr2dim[y, x] != 0)
        {
            throw new Exception("Field already ocupied!");
        }

        _arr2dim[y, x] = value;

        if (!ValidBoard(new Coordinate(x, y)))
        {
            _arr2dim[y, x] = 0;
            throw new Exception("Value not valid!");
        }
    }

    public bool GetStarted()
    {
        return SolveTable();
        //return result ? "Table is solved!" : "Table couldn't be solved.";
    }

    private bool SolveTable(Coordinate? lastMove = null)
    {
        if (lastMove.HasValue && !ValidBoard(lastMove.Value))
        {
            return false;
        }

        Coordinate? freeMove = GetNextFreeMove();

        if (!freeMove.HasValue)
        {
            return true;
        }

        var nextMove = freeMove.Value;

        for (int i = 1; i <= 9; i++)
        {
            _arr2dim[nextMove.Y, nextMove.X] = i;

            if (SolveTable(nextMove))
            {
                return true;
            }

            _arr2dim[nextMove.Y, nextMove.X] = 0;
        }

        return false;
    }

    private bool ValidBoard(Coordinate lastMove)
    {
        var value = _arr2dim[lastMove.Y, lastMove.X];

        if (value == 0)
        {
            return true;
        }

        // colums with coordinates        
        for (int y = 0; y < 9; y++)
        {
            if (lastMove.Y != y && value == _arr2dim[y, lastMove.X])
            {
                return false;
            }
        }

        // rows with coordinates
        for (int x = 0; x < 9; x++)
        {
            if (lastMove.X != x && value == _arr2dim[lastMove.Y, x])
            {
                return false;
            }
        }

        // 3x3 box from coordinates
        var startY = lastMove.Y / 3 * 3;
        var startX = lastMove.X / 3 * 3;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                var yG = startY + y;
                var xG = startX + x;

                if (lastMove.Y == yG && lastMove.X == xG)
                {
                    continue;
                }

                if (value == _arr2dim[yG, xG])
                {
                    return false;
                }
            }
        }

        #region old version
        // check rows and columns
        //for (int y = 0; y < 9; y++)
        //{
        //    for (int x = 0; x < 9; x++)
        //    {
        //        var value = _arr2dim[y, x];

        //        for (int i = 0; i < 9; i++)
        //        {
        //            if (value != 0 && x != i && value == _arr2dim[y, i])
        //            {
        //                return false;
        //            }
        //            if (value != 0 && y != i && value == _arr2dim[i, x])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        // rows with list
        //for (int y = 0; y < 9; y++)
        //{
        //    var list = new List<int>();
        //    for (int x = 0; x < 9; x++)
        //    {
        //        var v = _arr2dim[y, x];
        //        if (v == 0)
        //            continue;

        //        if (list.Contains(v))
        //        {
        //            return false;
        //        }
        //        list.Add(v);
        //    }
        //}

        //columns with list
        //for (int x = 0; x < 9; x++)
        //{
        //    var list = new List<int>();
        //    for (int y = 0; y < 9; y++)
        //    {
        //        var v = _arr2dim[y, x];
        //        if (v == 0)
        //            continue;

        //        if (list.Contains(v))
        //        {
        //            return false;
        //        }
        //        list.Add(v);
        //    }
        //}

        // check all 3x3 boxes
        //0 3 6->start position box
        //for (int yB = 0; yB < 7; yB += 3)
        //{
        //    for (int xB = 0; xB < 7; xB += 3)
        //    {
        //        // box
        //        var list = new List<int>();
        //        for (int yInn = 0; yInn < 3; yInn++)
        //        {
        //            for (int xInn = 0; xInn < 3; xInn++)
        //            {
        //                var v = _arr2dim[yB + yInn, xB + xInn];

        //                if (v == 0)
        //                    continue;

        //                if (list.Contains(v))
        //                {
        //                    return false;
        //                }
        //                list.Add(v);
        //            }
        //        }
        //    }
        //}

        // only top left box
        //var countY = 0;
        //var countX = 0;

        //while (countY < 3 && countX < 3)
        //{
        //    for (int y = 0; y < 3; y++)
        //    {
        //        for (int x = 0; x < 3; x++)
        //        {
        //            var value = _arr2dim[countY, countX];
        //            if (y != countY && x != countX && value != 0 && _arr2dim[y, x] == value)
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    if (countY != 2 && countX == 2)
        //    {
        //        countY++;
        //        countX = 0;
        //    }
        //    else
        //    {
        //        countX++;
        //    }
        //}
        #endregion

        return true;
    }

    private Coordinate? GetNextFreeMove()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (_arr2dim[y, x] == 0)
                {
                    return new Coordinate(x, y);
                }
            }
        }

        return null;
    }

    //public int[,] GetStarted(int row, int column, Dictionary<int, List<int>>? dictionary = null)
    //{
    //    if (dictionary == null)
    //    {
    //        dictionary = new Dictionary<int, List<int>>();
    //    }

    //    //dictionary[(row * 9) + column] = GetAllPosibleNumbers(row, column);
    //    _arr2dim[row, column] = dictionary[(row * 9) + column].FirstOrDefault();
    //    var nextKasten = IsCombinationPossible(row, column);

    //    // forword, current or back box
    //    if (!nextKasten)
    //    {
    //        if (dictionary[(row * 9) + column] == null)
    //        {
    //            if (column == 0 && row != 0)
    //            {
    //                column = 8;
    //                row--;
    //            }
    //            else if (column != 0)
    //            {
    //                column--;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (column == 8 && row != 8)
    //        {
    //            column = 0;
    //            row++;
    //        }
    //        else if (column != 8)
    //        {
    //            column++;
    //        }
    //    }

    //    return GetStarted(row, column, dictionary);
    //}

    // kasten voll?
    //internal bool IsCombinationPossible(int row, int column)
    //{
    //    // columns and rows
    //    for (int i = 0; i < 9; i++)
    //    {
    //        if (_arr2dim[row, column] == _arr2dim[row, i] || _arr2dim[row, column] == _arr2dim[i, column])
    //        {
    //            return false;
    //        }
    //    }

    //    // boxes columns
    //    if (row == 1 || row == 4 || row == 7)
    //    {
    //        if (_arr2dim[row, column] == _arr2dim[row, column + 1] || _arr2dim[row, column] == _arr2dim[row, column - 1])
    //        {
    //            return false;
    //        }
    //    }

    //    // box rows
    //    if (column == 1 || column == 4 || column == 7)
    //    {
    //        if (_arr2dim[row, column] == _arr2dim[row + 1, column] || _arr2dim[row, column] == _arr2dim[row - 1, column])
    //        {
    //            return false;
    //        }
    //    }

    // box corners
    //if ((column == 1 && row == 1) || (column == 4 && row == 4) || (column == 7 && row == 7))
    //{
    //    if (_arr2dim[row + 1, column + 1] == )
    //    {

    //    }
    //}

    //return true;
    //}
}