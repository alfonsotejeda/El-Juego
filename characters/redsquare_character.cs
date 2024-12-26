using P_P.board;
using P_P.tramps;
using Spectre.Console;

namespace P_P.characters
{
    public class RedSquareCharacter : BaseCharacter
    {
        public string icon;
        public RedSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard, BaseCharacter character , List<BaseTramp> tramps,List<BaseCharacter> characters)
        {
            Console.WriteLine("Introduce el personaje que quieres Atacar");
            int characterToAttack = DisplayCharactersToChange(characters , character , gameboard , tramps);
            
            characters[characterToAttack].Live -= 10;
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
                printingMethods.layout["Bottom"].Update(new Panel($"Has atacado al personaje {selectedCharacter.Icon}").Expand());
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
                $"Elige al jugador que quieres atacar:\n\n" +
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
    }
}