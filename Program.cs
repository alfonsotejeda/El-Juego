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
            
            List<BaseCharacter> characters = InitializeCharacters(rows, columns);
            List<BaseTramp> tramps = InitializeTraps(rows, columns, gameBoard);
            
            foreach (BaseCharacter character in characters)
            {
                character.PlaceCharacter(gameBoard, character);
            }
            
            try
            {
                while (true)
                {
                    foreach (BaseCharacter character in characters)
                    {
                        printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
                        character.TakeTurn(gameBoard, character, tramps, characters);
                    }
                
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        if (AnsiConsole.Confirm("¿Deseas volver al menú principal?"))
                        {
                            printingMethods.layout["Bottom"].Update(new Panel("¿Deseas volver al menú principal?").Expand());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                printingMethods.layout["Bottom"].Update(new Panel($"Error en el juego: {ex.Message}").Expand());
                printingMethods.layout["Bottom"].Update(new Panel("Presiona cualquier tecla para volver al menú...").Expand());
                Console.ReadKey(true);
            }
        }

        static List<BaseCharacter> InitializeCharacters(int rows, int columns)
        {
            int player1Row = 1;
            int player1Column = 1;
            int player1movementCapacity = 5;
            
            int player2StartRow = rows - 2;
            int player2StartColumn = columns - 2;
            int player2movementCapacity = 5;
            
            int player3StartRow = rows - 2;
            int player3StartColumn = 1;
            int player3movementCapacity = 5;

            int player4StartRow = 1;
            int player4StartColumn = columns - 2;
            int player4movementCapacity = 5;

            List<BaseCharacter> characters = new List<BaseCharacter>
            {
                new BlueSquareCharacter("🟦", "defense", ref player1movementCapacity, ref player1Row, ref player1Column),
                new YellowSquareCharacter("🟨", "jumpOveraWall", ref player4movementCapacity, ref player4StartRow, ref player4StartColumn),
                new GreenSquareCharacter("🟩", "removeOneRandomTramp", ref player3movementCapacity, ref player3StartRow, ref player3StartColumn),
                new RedSquareCharacter("🟥", "attack", ref player2movementCapacity, ref player2StartRow, ref player2StartColumn)
            };

            return characters;
        }

        static List<BaseTramp> InitializeTraps(int rows, int columns, Shell[,] gameBoard)
        {
            List<BaseTramp> tramps = new List<BaseTramp>
            {
                new GoToOriginTramp("goToOrigin"),
                new ReduceLiveTramp("reduceLive"),
                new ClosePathTramp("closePath")
            };

            foreach (BaseTramp tramp in tramps)
            {
                tramp.CreateRandomTraps(gameBoard, tramp, 1, rows / 2, 1, columns / 2, 4);
                tramp.CreateRandomTraps(gameBoard, tramp, 1, rows / 2, columns / 2, columns, 4);
                tramp.CreateRandomTraps(gameBoard, tramp, rows / 2, rows, 1, columns / 2, 4);
                tramp.CreateRandomTraps(gameBoard, tramp, rows / 2, rows, columns / 2, columns, 4);
            }

            return tramps;
        }
    }
}