namespace P_P{
public class BaseCharacter
{
    public string icon;
    public string ability;
    public int movementCapacity;
    public int playerStartColumn;
    public int playerStartRow;
    public BaseCharacter(string name, string ability, int movementCapacity, int playerStartColumn, int playerStartRow)
    {
        this.icon = name ?? throw new ArgumentNullException(nameof(name));
        this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        this.movementCapacity = movementCapacity;
        this.playerStartColumn = playerStartColumn;
        this.playerStartRow = playerStartRow;
    }
    public void Move(ref int playerStartRow, ref int playerStartColumn, string[,] gameBoard, Board board)
    {
        ConsoleKeyInfo key = Console.ReadKey();
        int newRow = playerStartRow;
        int newColumn = playerStartColumn;

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                newRow--;
                break;
            case ConsoleKey.DownArrow:
                newRow++;
                break;
            case ConsoleKey.LeftArrow:
                newColumn--;
                break;
            case ConsoleKey.RightArrow:
                newColumn++;
                break;
        }

        // Verificar si la nueva posición es válida (no es pared)
        if (newRow >= 0 && newRow < gameBoard.GetLength(0) && 
            newColumn >= 0 && newColumn < gameBoard.GetLength(1) && 
            gameBoard[newRow, newColumn] != board.wall)
        {
            gameBoard[playerStartRow, playerStartColumn] = "⬜️";
            playerStartRow = newRow;
            playerStartColumn = newColumn;
            gameBoard[playerStartRow, playerStartColumn] = icon;
        }
    }

}
}
