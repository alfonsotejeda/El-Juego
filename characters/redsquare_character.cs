using P_P.board;
using P_P.tramps;
using Spectre.Console;

namespace P_P.characters
{
    public class RedSquareCharacter : BaseCharacter
    {
        public new string icon;
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
           // Crear las opciones de personajes
            var posibleChangeCharacters = new List<string>();
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] != character)
                {
                    posibleChangeCharacters.Add($"Personaje {i} : {characters[i].Icon}");
                }
            }

            int selectedIndex = 0; // Índice del personaje seleccionado

            // Usar AnsiConsole.Live para manejar las actualizaciones dinámicas
            AnsiConsole.Live(printingMethods.layout).Start(ctx =>
            {
                bool selectionMade = false;

                while (!selectionMade)
                {
                    // Actualizar el layout["Bottom"] con las opciones del menú
                    var menuContent = new Panel(
                        $"Elige al jugador que quieres atacar:\n\n" +
                        string.Join("\n", posibleChangeCharacters.Select((option, index) =>
                            index == selectedIndex
                                ? $"[green]> {option}[/]" // Opción seleccionada
                                : $"  {option}"          // Opciones no seleccionadas
                        ))
                    ).Expand();

                    printingMethods.layout["Bottom"].Update(menuContent);
                    ctx.Refresh();

                    // Capturar la entrada del usuario
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + posibleChangeCharacters.Count) % posibleChangeCharacters.Count;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % posibleChangeCharacters.Count;
                            break;
                        case ConsoleKey.Enter:
                            selectionMade = true;
                            break;
                    }
                }

                // Acción tras seleccionar un personaje
                var selectedCharacter = characters[selectedIndex];
                printingMethods.layout["Bottom"].Update(
                    new Panel($"Has atacado al personaje {selectedCharacter.Icon}").Expand()
                );
                ctx.Refresh();

                // Volver a imprimir el juego con la selección hecha
                printingMethods.PrintGameSpectre(gameBoard, character, characters, tramps);
            });
            string selectedCharacter = posibleChangeCharacters[selectedIndex];
            int selectedCharacterIndex = int.Parse(selectedCharacter.Split(' ')[1]);
            return selectedCharacterIndex;
        }
    }
} 