namespace Minesweeper;

public class Minefield
{
    public const int GameBoardX = 5;
    public const int GameBoardY = 5;
    private LocationState[,] _gameBoard = new LocationState[GameBoardX, GameBoardY];

    private bool[,] _bombLocations = new bool[GameBoardX, GameBoardY];
    private bool _isGameOver = false;

    private int _totalBombs = 0;
    private int _unRevealedLocations = 0;

    private const string GameOverMessage = "Game Over";
    private const string GameWonMessage = "Congratulations! You've won the game!";


    public int GetGameBoardX()
    {
        return GameBoardX;
    }

    public int GetGameBoardY()
    {
        return GameBoardY;
    }

    public void SetBomb(int x, int y)
    {
        _totalBombs++;
        _bombLocations[x, y] = true;
    }

    public void RevealLocation(int x, int y)
    {
        if (GetLocation(x,y).IsRevealed)
        {
            DisplayGameBoard();
            return;
        }

        if (IsBombAtLocation(x, y))
        {
            Console.WriteLine(GameOverMessage);
            _isGameOver = true;
            return;
        }

        int locationValue = CalculateLocationValue(x, y);

        if (locationValue == 0)
        {
            FloodFill(x, y);
        }

        DisplayGameBoard();

        if (hasWon())
        {
            Console.Write(GameWonMessage);
            _isGameOver = true;
            return;
        }
    }

    private void FloodFill(int x, int y)
    {
        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int checkX = x + adjacentX;
                int checkY = y + adjacentY;

                if (checkX < 0 || checkX >= _gameBoard.GetLength(0) || checkY < 0 || checkY >= _gameBoard.GetLength(1))
                {
                    continue;
                }

                if (_gameBoard[checkX, checkY].IsRevealed)
                {
                    continue;
                }

                int locationValue = CalculateLocationValue(checkX, checkY);

                if (locationValue == 0)
                {
                    FloodFill(checkX, checkY);
                }
            }
        }
    }

    public LocationState GetLocation(int x, int y)
    {
        return _gameBoard[x, y];
    }

    private bool IsBombAtLocation(int x, int y)
    {
        return _bombLocations[x, y];
    }


    private int CalculateLocationValue(int x, int y)
    {
        int bombCounter = 0;
        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY= -1; adjacentY <= 1; adjacentY++)
            {
                if(adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int checkX = x + adjacentX;
                int checkY = y + adjacentY;

                if(checkX < 0 || checkX >= _gameBoard.GetLength(0) || checkY < 0 || checkY >= _gameBoard.GetLength(1))
                {
                    continue;
                }

                if (IsBombAtLocation(checkX, checkY) == true)
                {
                    bombCounter++;
                }
            }
        }
        _unRevealedLocations--;
        _gameBoard[x, y].IsRevealed = true;
        _gameBoard[x, y].AdjacentBombCount = bombCounter;
        return bombCounter;
    }

    private void DisplayGameBoard()
    {
        Console.Write("  ");
        for (int boardX = 0; boardX < _gameBoard.GetLength(1); boardX++)
        {
            Console.Write(boardX);
        }

        Console.WriteLine();

        for (int boardY = _gameBoard.GetLength(0) - 1; boardY >= 0; boardY--)
        {
            Console.Write(boardY + "|");
            for (int boardX = 0; boardX < _gameBoard.GetLength(1); boardX++)
            {
                Console.Write(_gameBoard[boardX, boardY].DisplayValue);
            }
            Console.WriteLine();
        }
    }

    public void StartGame()
    {
        for (int boardY = _gameBoard.GetLength(0) - 1; boardY >= 0; boardY--)
        {
            for (int boardX = 0; boardX < _gameBoard.GetLength(1); boardX++)
            {
                _unRevealedLocations++;
                _gameBoard[boardX, boardY] = new LocationState();
            }
        }
        DisplayGameBoard();
    }

    public bool hasWon()
    {
        return (_unRevealedLocations - _totalBombs) == 0;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }


}
