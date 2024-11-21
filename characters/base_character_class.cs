namespace P_P{
public class BaseCharacter
{
    public string icon;
    public string ability;
    public int movementCapacity;
    public int playerStartColumn;
    public int playerStartRow;
    public string iconOfNextPositon;
    public BaseCharacter(string name, string ability, int movementCapacity, int playerStartColumn, int playerStartRow)
    {
        this.icon = name ?? throw new ArgumentNullException(nameof(name));
        this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
        this.movementCapacity = movementCapacity;
        this.playerStartColumn = playerStartColumn;
        this.playerStartRow = playerStartRow;
    }
    public void Move(ref int playerStartRow, ref int playerStartColumn, Shell[,] gameBoard, Board board)
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

        if (newRow >= 0 && newRow < gameBoard.GetLength(0) && 
            newColumn >= 0 && newColumn < gameBoard.GetLength(1) && 
            !gameBoard[newRow, newColumn].IsWall && 
            !gameBoard[newRow, newColumn].HasCharacter)
        {
            // Limpiar posición anterior
            gameBoard[playerStartRow, playerStartColumn].HasCharacter = false;
            gameBoard[playerStartRow, playerStartColumn].CharacterIcon = null;
            gameBoard[playerStartRow, playerStartColumn].IsPath = true;

            // Actualizar nueva posición
            playerStartRow = newRow;
            playerStartColumn = newColumn;
            gameBoard[playerStartRow, playerStartColumn].HasCharacter = true;
            gameBoard[playerStartRow, playerStartColumn].CharacterIcon = this.icon;
            gameBoard[playerStartRow, playerStartColumn].IsPath = false;

            // Actualizar el tablero
            board.PrintBoard(gameBoard);
        }
    }

}
}
