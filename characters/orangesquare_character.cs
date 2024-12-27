using P_P.board;
using P_P.tramps;
using System.Collections.Generic;
using Spectre.Console;
using P_P.PrintingMethods;
namespace P_P.characters
{
    public class OrangeSquareCharacter : BaseCharacter
    {
        public OrangeSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn, ref int countdown)
            : base(icon, ability, movementCapacity, playerRow,  playerColumn,  countdown)
        {
        }

        public override void UseAbility(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            MazeGenerator mazeGenerator = new MazeGenerator();
            int characterToChangeMazeIndex = DisplayCharactersToChange(characters, character, gameBoard, tramps);
            BaseCharacter characterToChangeMaze = characters[characterToChangeMazeIndex];

            if (IsInFirstQuadrant(characterToChangeMaze, gameBoard))
            {
                GenerateMazeInQuadrant(0, gameBoard.GetLength(0) / 2, 0, gameBoard.GetLength(1) / 2, gameBoard, character, tramps);
                printingMethods.layout["Bottom"].Update(new Panel($"Has cambiado el laberinto del personaje {characterToChangeMaze.Icon}").Expand());
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            }
            else if (IsInSecondQuadrant(characterToChangeMaze, gameBoard))
            {
                GenerateMazeInQuadrant(0, gameBoard.GetLength(0) / 2, gameBoard.GetLength(1) / 2, gameBoard.GetLength(1), gameBoard, character, tramps);
                printingMethods.layout["Bottom"].Update(new Panel($"Has cambiado el laberinto del personaje {characterToChangeMaze.Icon}").Expand());
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            }
            else if (IsInThirdQuadrant(characterToChangeMaze, gameBoard))
            {
                GenerateMazeInQuadrant(gameBoard.GetLength(0) / 2, gameBoard.GetLength(0), 0, gameBoard.GetLength(1) / 2, gameBoard, character, tramps);
                printingMethods.layout["Bottom"].Update(new Panel($"Has cambiado el laberinto del personaje {characterToChangeMaze.Icon}").Expand());
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            }
            else if (IsInFourthQuadrant(characterToChangeMaze, gameBoard))
            {
                GenerateMazeInQuadrant(gameBoard.GetLength(0) / 2, gameBoard.GetLength(0), gameBoard.GetLength(1) / 2, gameBoard.GetLength(1), gameBoard, character, tramps);
                printingMethods.layout["Bottom"].Update(new Panel($"Has cambiado el laberinto del personaje {characterToChangeMaze.Icon}").Expand());
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            }
        }
        public override int DisplayCharactersToChange(List<BaseCharacter> characters, BaseCharacter character, Shell[,] gameBoard, List<BaseTramp> tramps)
        {
            var posibleChangeCharacters = CreateCharacterOptions(characters, character);
            int selectedIndex = 0;

            AnsiConsole.Live(printingMethods.layout).Start(ctx =>
            {
                bool selectionMade = false;

                while (!selectionMade)
                {
                    UpdateMenuContent(posibleChangeCharacters, selectedIndex);
                    ctx.Refresh();

                    var key = Console.ReadKey(true).Key;
                    selectedIndex = UpdateSelectedIndex(key, selectedIndex, posibleChangeCharacters.Count, ref selectionMade);
                }

                var selectedCharacter = characters[selectedIndex];
                printingMethods.layout["Bottom"].Update(new Panel($"Has cambiado el laberinto del personaje {selectedCharacter.Icon}").Expand());
                ctx.Refresh();

                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            });

            return ParseSelectedCharacterIndex(posibleChangeCharacters[selectedIndex]);
        }

        private List<string> CreateCharacterOptions(List<BaseCharacter> characters, BaseCharacter character)
        {
            var posibleChangeCharacters = new List<string>();
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] != character)
                {
                    posibleChangeCharacters.Add($"Personaje {i} : {characters[i].Icon}");
                }
            }
            return posibleChangeCharacters;
        }
        private void UpdateMenuContent(List<string> posibleChangeCharacters, int selectedIndex)
        {
            var menuContent = new Panel(
                $"Elige al jugador al que le quieres cambiar el laberinto:\n\n" +
                string.Join("\n", posibleChangeCharacters.Select((option, index) =>
                    index == selectedIndex
                        ? $"[green]> {option}[/]" // Opción seleccionada
                        : $"  {option}"          // Opciones no seleccionadas
                ))
            ).Expand();

            printingMethods.layout["Bottom"].Update(menuContent);
        }
        private int UpdateSelectedIndex(ConsoleKey key, int selectedIndex, int optionsCount, ref bool selectionMade)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + optionsCount) % optionsCount;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % optionsCount;
                    break;
                case ConsoleKey.Enter:
                    selectionMade = true;
                    break;
            }
            return selectedIndex;
        }
        private int ParseSelectedCharacterIndex(string selectedCharacter)
        {
            return int.Parse(selectedCharacter.Split(' ')[1]);
        }
        private bool IsInFirstQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn <= gameboard.GetLength(1) / 2 && character.PlayerRow <= gameboard.GetLength(0) / 2;
        }

        private bool IsInSecondQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn >= gameboard.GetLength(1) / 2 && character.PlayerRow <= gameboard.GetLength(0) / 2;
        }

        private bool IsInThirdQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn <= gameboard.GetLength(1) / 2 && character.PlayerRow >= gameboard.GetLength(0) / 2;
        }

        private bool IsInFourthQuadrant(BaseCharacter character, Shell[,] gameboard)
        {
            return character.PlayerColumn >= gameboard.GetLength(1) / 2 && character.PlayerRow >= gameboard.GetLength(0) / 2;
        }
        private void GenerateMazeInQuadrant(int rowStart, int rowEnd, int columnStart, int columnEnd, Shell[,] gameBoard, BaseCharacter character , List<BaseTramp> tramps)
        {
            MazeGenerator mazeGenerator = new MazeGenerator();
            mazeGenerator.GenerateMaze(rowStart, rowEnd, columnStart, columnEnd, gameBoard);
            character.PlaceCharacter(gameBoard, character);
            foreach (BaseTramp tramp in tramps)
            {
                tramp.CreateRandomTraps(gameBoard, tramp, rowStart, rowEnd, columnStart, columnEnd, 4);
            }
        }
    }
}
