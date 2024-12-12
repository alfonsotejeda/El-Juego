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
            BaseTramp[] tramps = new BaseTramp[5];
            
            Board board = new Board(columns, rows);
            Shell[,] gameBoard = board.CreateBoard();
            
            // int player1StartRow = 1;
            // int player1StartColumn = 1;
            // BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦", "defense", 5, ref player1StartRow, ref player1StartColumn);
            
            // int player2StartRow = 31;
            // int player2StartColumn = 31;
            // RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack", 5, ref player2StartRow, ref player2StartColumn);
            
            // blueSquareCharacter.PlaceCharacter(gameBoard , blueSquareCharacter);
            // redSquareCharacter.PlaceCharacter(gameBoard  , redSquareCharacter);

            
            
            
            
            // try
            
            // {
            //     while (true)
            //     {
            //         board.PrintBoardSpectre(gameBoard);
            //         blueSquareCharacter.Move(ref blueSquareCharacter.playerStartRow, ref blueSquareCharacter.playerStartColumn, gameBoard, board);
            //         // Check for escape key to return to menu
            //         if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            //         {
            //             if (AnsiConsole.Confirm("¿Deseas volver al menú principal?"))
            //                 break;
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     AnsiConsole.MarkupLine($"[red]Error en el juego: {ex.Message}[/]");
            //     AnsiConsole.MarkupLine("[yellow]Presiona cualquier tecla para volver al menú...[/]");
            //     Console.ReadKey(true);
            // }
        }
    }
}