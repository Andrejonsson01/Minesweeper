namespace Minesweeper;

class Minesweeper
{
    static void Main()
    {
        var field = new Minefield();

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        /*the mine field should look like this now:
          0 1 2 3 4
        4|1 X 1
        3|1 1 1 1 1
        2|2 2 1 1 X
        1|X X 1 1 1
        0|X 3 1
        */

        field.StartGame();

        while (!field.IsGameOver())
        {
            Console.WriteLine("\n");
            string[] selectedLocation = Console.ReadLine().Split(' ');
            if (selectedLocation.Length == 2 && int.TryParse(selectedLocation[0], out int x) && int.TryParse(selectedLocation[1], out int y))
            {
                if (x < field.GetGameBoardX() && y < field.GetGameBoardY() && x >= 0 && y >= 0)
                {
                    field.RevealLocation(x, y);
                }
                else
                {
                    Console.WriteLine("Provide valid coordinates between 0<=x<=" + (field.GetGameBoardX()-1) + " and  0<y=<=" + (field.GetGameBoardY()-1));
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Provide valid coordinates in the format x y");
            }
        }


    }
}
