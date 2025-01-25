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
            int rows = 29;
            int columns = 29;
            
            Board board = new Board(columns, rows);
            Shell[,] gameBoard = board.CreateBoard();
            
            List<BaseCharacter> characters = InitializeCharacters(rows, columns);
            List<BaseTramp> tramps = InitializeTraps(rows, columns, gameBoard);
            
    
            // Preguntar por la cantidad de jugadores
            int numberOfPlayers = AnsiConsole.Ask<int>("¿Cuántos jugadores van a jugar? (1-4)");

            // Validar la cantidad de jugadores
            if (numberOfPlayers < 1 || numberOfPlayers > 4)
            {
                AnsiConsole.MarkupLine("[red]Número de jugadores no válido. Debe ser entre 1 y 4.[/]");
                return;
            }

            List<BaseCharacter> selectedCharacters = SelectCharactersForPlayers(characters, numberOfPlayers);
            SetPositions(selectedCharacters, rows, columns);

            foreach (BaseCharacter character in selectedCharacters)
            {
                character.PlaceCharacter(gameBoard, character);
            }
            try
            {
                while (true)
                {
                    
                    foreach (BaseCharacter character in selectedCharacters)
                    {
                        printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
                        character.TakeTurn(gameBoard, character, tramps, selectedCharacters);
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
            int standardRow = 1;
            int standardColumn = 1;


            int movementCapacityBlue = 8;
            int movementCapacityYellow =10 ;
            int movementCapacityGreen = 7;
            int movementCapacityRed = 9;

    
            int countdownBlue = 2;
            int countdownYellow = 3;
            int countdownGreen = 3;
            int countdownRed = 2;

            int movementCapacityViolet = 9;
            int countdownViolet = 3;

            int movementCapacityOrange = 8;
            int countdownOrange = 3;

            int visibilityBlue = 5;
            int visibilityYellow = 3;
            int visibilityGreen = 4;
            int visibilityRed = 3;
            int visibilityViolet = 3;
            int visibilityOrange = 6;

            List<BaseCharacter> characters = new List<BaseCharacter>
            {
                new BlueSquareCharacter("🟦", "defense", ref movementCapacityBlue, ref standardRow, ref standardColumn, ref countdownBlue, ref visibilityBlue),
                new YellowSquareCharacter("🟨", "jumpOveraWall", ref movementCapacityYellow, ref standardRow, ref standardColumn, ref countdownYellow, ref visibilityYellow),
                new GreenSquareCharacter("🟩", "removeOneRandomTramp", ref movementCapacityGreen, ref standardRow, ref standardColumn, ref countdownGreen, ref visibilityGreen),
                new RedSquareCharacter("🟥", "attack", ref movementCapacityRed, ref standardRow, ref standardColumn, ref countdownRed, ref visibilityRed),
                new VioletSquareCharacter("🟪", "increaseMovement", ref movementCapacityViolet, ref standardRow, ref standardColumn, ref countdownViolet, ref visibilityViolet),
                new OrangeSquareCharacter("🟧", "changemaze", ref movementCapacityOrange, ref standardRow, ref standardColumn, ref countdownOrange, ref visibilityOrange)
            };

            return characters;
        }
        

        static List<BaseTramp> InitializeTraps(int rows, int columns, Shell[,] gameBoard)
        {
            List<BaseTramp> tramps = new List<BaseTramp>
            {
                new GoToOriginTramp("goToOrigin"),
                new ReduceLiveTramp("reduceLive"),
                //new ClosePathTramp("closePath")
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

        static List<BaseCharacter> SelectCharactersForPlayers(List<BaseCharacter> characters, int numberOfPlayers)
        {
            var characterOptions = characters.Select((character, index) => $"{index + 1}. {character.Icon} - {character.Ability}").ToList();
            var selectedCharacters = new List<BaseCharacter>();

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                AnsiConsole.MarkupLine($"[bold yellow]Jugador {i}, selecciona tu personaje:[/]");
                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Selecciona tu personaje:")
                        .PageSize(10)
                        .AddChoices(characterOptions));

                int selectedIndex = characterOptions.IndexOf(selection);
                var selectedCharacter = characters[selectedIndex];

                
                var newCharacter = (BaseCharacter?)Activator.CreateInstance(selectedCharacter.GetType(), selectedCharacter.Icon, selectedCharacter.Ability, selectedCharacter.MovementCapacity, selectedCharacter.PlayerRow, selectedCharacter.PlayerColumn, selectedCharacter.Countdown , selectedCharacter.Visibility);
                if (newCharacter != null)
                {
                    selectedCharacters.Add(newCharacter);
                }
                else
                {
                    throw new InvalidOperationException("Failed to create character instance.");
                }
            }

            return selectedCharacters;
        }

        static void SetPositions(List<BaseCharacter> selectedCharacters, int rows, int columns)
        {
            int player1Row = 1;
            int player1Column = 1;
            
            int player2StartRow = 1;
            int player2StartColumn = columns - 2;
            
            int player3StartRow = rows - 2;
            int player3StartColumn = 1;

            int player4StartRow = rows - 2;
            int player4StartColumn = columns - 2;
            for(int i = 0; i < selectedCharacters.Count; i++)
            {
                if (i == 0)
                {
                    selectedCharacters[i].PlayerRow = player1Row;
                    selectedCharacters[i].PlayerColumn = player1Column;
                }
                else if (i == 1)
                {
                    selectedCharacters[i].PlayerRow = player2StartRow;
                    selectedCharacters[i].PlayerColumn = player2StartColumn;
                }
                else if (i == 2)
                {
                    selectedCharacters[i].PlayerRow = player3StartRow;
                    selectedCharacters[i].PlayerColumn = player3StartColumn;
                }
                else if (i == 3)
                {
                    selectedCharacters[i].PlayerRow = player4StartRow;
                    selectedCharacters[i].PlayerColumn = player4StartColumn;
                }
            }
        }
    }
}