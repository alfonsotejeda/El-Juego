using System.Dynamic;
using Spectre.Console;

namespace P_P
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (true)
            {
                string option = menu.ShowMenu();
                
                if (option == "1")
                {
                    RunGame();
                }
                else if (option == "2")
                {
                    System.Console.WriteLine("En contrucción");
                }
                else if (option == "3")
                {
                    System.Console.WriteLine("En contrucción");
                }
            }
        }

        static void RunGame()
        {
            int rows = 33;
            int columns = 33;
            string[] tramps = new string[5];
            
            Board board = new Board(columns, rows);
            Shell[,] gameBoard = board.CreateBoard();
            
            int player1StartRow = 1;
            int player1StartColumn = 1;
            BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦", "defense", 5, ref player1StartRow, ref player1StartColumn);
            
            int player2StartRow = 31;
            int player2StartColumn = 31;
            RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack", 5, ref player2StartRow, ref player2StartColumn);
            
            gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].HasCharacter = true;
            gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].CharacterIcon = blueSquareCharacter.icon;
            gameBoard[blueSquareCharacter.playerStartRow, blueSquareCharacter.playerStartColumn].IsPath = false;

            gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].HasCharacter = true;
            gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].CharacterIcon = redSquareCharacter.icon;
            gameBoard[redSquareCharacter.playerStartRow, redSquareCharacter.playerStartColumn].IsPath = false;

            ClosePathTramp closePathTramp = new ClosePathTramp("🟩", 50, "c");
            closePathTramp.CreateRandomTraps(gameBoard,0 ,rows / 2, 0, columns / 2);
            tramps[0] = closePathTramp.trampId;
            
            
            
            try
            
            {
                while (true)
                {
                    board.PrintBoardSpectre(gameBoard);
                    blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn, gameBoard, board);

                    if (closePathTramp.CheckTrap(blueSquareCharacter, gameBoard))
                    {
                        Console.WriteLine("The player is trapped!");
                        ConsoleKeyInfo key = Console.ReadKey();
                    }
                    
                    // Check for escape key to return to menu
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        if (AnsiConsole.Confirm("¿Deseas volver al menú principal?"))
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error en el juego: {ex.Message}[/]");
                AnsiConsole.MarkupLine("[yellow]Presiona cualquier tecla para volver al menú...[/]");
                Console.ReadKey(true);
            }
        }
    }
}