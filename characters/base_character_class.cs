using P_P.board;

namespace P_P.characters{
public class BaseCharacter
{
    public string Icon;
    public string Ability;
    public int MovementCapacity;
    public int PlayerColumn;
    public int PlayerRow;
    public int Life = 100;
    public BaseCharacter(string name, string ability, int movementCapacity, int playerColumn, int playerRow)
    {
        this.Icon = name ?? throw new ArgumentNullException(nameof(name));
        this.Ability = ability ?? throw new ArgumentNullException(nameof(ability));
        this.MovementCapacity = movementCapacity;
        this.PlayerColumn = playerColumn;
        this.PlayerRow = playerRow;
    }
    public void Move(ref int playerRow, ref int playerColumn, ref int movementCapacity ,Shell[,] gameBoard)
    {
        ConsoleKeyInfo key = Console.ReadKey();
        int newRow = playerRow;
        int newColumn = playerColumn;

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
            gameBoard[newRow, newColumn].GetType() != typeof(wall) && 
            !gameBoard[newRow, newColumn].HasCharacter)
        {
            // Limpiar posición anterior
            gameBoard[playerRow, playerColumn].HasCharacter = false;
            gameBoard[playerRow, playerColumn].CharacterIcon = null;
            gameBoard[playerRow, playerColumn] = new path("⬜️");

            // Actualizar nueva posición
            playerRow = newRow;
            playerColumn = newColumn;
            gameBoard[playerRow, playerColumn].HasCharacter = true;
            gameBoard[playerRow, playerColumn].CharacterIcon = this.Icon;
            // gameBoard[playerRow, playerColumn].IsPath = false;
            // Actualizar el tablero
            movementCapacity--;
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            printingMethods.PrintBoardSpectre(gameBoard);
        }
    }
    public void PlaceCharacter(Shell[,] gameBoard , BaseCharacter character)
    {
            gameBoard[character.PlayerRow, character.PlayerColumn].HasCharacter = true;
            gameBoard[character.PlayerRow, character.PlayerColumn].CharacterIcon = character.Icon;
    }

    public void TakeTurn(Shell[,] gameBoard, BaseCharacter character)
    {
        while (character.MovementCapacity != 0)
        {
            character.Move(ref character.PlayerRow, ref character.PlayerColumn, ref character.MovementCapacity,gameBoard);    
        }
        MovementCapacity = 5;
        Console.WriteLine("Next Turn");
        Console.ReadKey();
        PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
        printingMethods.PrintBoardSpectre(gameBoard);
    }

}
}
