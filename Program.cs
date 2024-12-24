using System.Dynamic;
using P_P.board;
using P_P.characters;
using P_P.menu;
using Spectre.Console;
using P_P.PrintingMethods;
using P_P.tramps;

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
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            int rows = 21;
            int columns = 21;
            
            Board board = new Board(columns, rows);
            Shell[,] gameBoard = board.CreateBoard();
            
            
            
            int player1Row = 1;
            int player1Column = 1;
            int player1movementCapacity = 5;
            BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("🟦", "defense", ref player1movementCapacity, ref player1Row, ref player1Column);
            
            int player2StartRow = rows - 2;
            int player2StartColumn = columns - 2;
            int player2movementCapacity = 5;
            RedSquareCharacter redSquareCharacter = new RedSquareCharacter("🟥", "attack", ref player2movementCapacity, ref player2StartRow, ref player2StartColumn);
            
            int player3StartRow = rows - 2;
            int player3StartColumn = 1;
            int player3movementCapacity = 5;
            GreenSquareCharacter greenSquareCharacter = new GreenSquareCharacter("🟩", "attack", ref player3movementCapacity, ref player3StartRow, ref player3StartColumn);

            int player4StartRow = 1;
            int player4StartColumn = columns - 2;
            int player4movementCapacity = 5;
            YellowSquareCharacter yellowSquareCharacter = new YellowSquareCharacter("🟨", "attack", ref player4movementCapacity, ref player4StartRow, ref player4StartColumn);

            blueSquareCharacter.PlaceCharacter(gameBoard , blueSquareCharacter);
            redSquareCharacter.PlaceCharacter(gameBoard  , redSquareCharacter);
            greenSquareCharacter.PlaceCharacter(gameBoard , greenSquareCharacter);
            yellowSquareCharacter.PlaceCharacter(gameBoard , yellowSquareCharacter);
            
            
            List<BaseTramp> tramps = new List<BaseTramp>();
            tramps.Add(new GoToOriginTramp("goToOrigin"));
            tramps.Add(new ReduceLiveTramp("reduceLive"));
            tramps.Add(new ClosePathTramp("closePath"));
            foreach (BaseTramp tramp in tramps)
            {
                tramp.CreateRandomTraps(gameBoard, tramp, 1, rows/2, 1, columns/2 , 4);
                tramp.CreateRandomTraps(gameBoard, tramp, 1, rows/2, columns/2, columns,4);
                tramp.CreateRandomTraps(gameBoard, tramp, rows/2, rows, 1, columns/2,4);
                tramp.CreateRandomTraps(gameBoard, tramp, rows/2, rows, columns/2, columns,4);
            }
            
            try
            
            {
                while (true)
                {
                   printingMethods.PrintGameSpectre(gameBoard , blueSquareCharacter); 
                   blueSquareCharacter.TakeTurn(gameBoard , blueSquareCharacter, tramps);
                   yellowSquareCharacter.TakeTurn(gameBoard , yellowSquareCharacter,tramps);
                   greenSquareCharacter.TakeTurn(gameBoard , greenSquareCharacter,tramps);
                   redSquareCharacter.TakeTurn(gameBoard , redSquareCharacter,tramps);
                   
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