using P_P.board;
using P_P.tramps;

namespace P_P.characters
{
    public class YellowSquareCharacter : BaseCharacter
    {
        public YellowSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            var directions = new (int, int)[]
            {
                (0, 1),  // Derecha
                (0, -1), // Izquierda
                (1, 0),  // Abajo
                (-1, 0)  // Arriba
            };
            foreach (var (dx, dy) in directions)
            {
                int newRow = character.PlayerRow + dx;
                int newColumn = character.PlayerColumn + dy;

                if (newRow >= 0 && newRow < gameboard.GetLength(0) && newColumn >= 0 && newColumn < gameboard.GetLength(1))
                {
                    if (gameboard[newRow, newColumn].GetType() == typeof(wall))
                    {
                        int newRow2 = newRow + dx;
                        int newColumn2 = newColumn + dy;
                        if (newRow2 >= 0 && newRow2 < gameboard.GetLength(0) && newColumn2 >= 0 && newColumn2 < gameboard.GetLength(1))
                        {
                            if (gameboard[newRow2, newColumn2].GetType() == typeof(path))
                            {
                                gameboard[character.PlayerRow, character.PlayerColumn].HasCharacter = false;
                                gameboard[character.PlayerRow, character.PlayerColumn].CharacterIcon = null;
                                gameboard[character.PlayerRow, character.PlayerColumn] = new path("⬜️");

                                character.PlayerRow = newRow2;
                                character.PlayerColumn = newColumn2;
                                gameboard[newRow2, newColumn2].HasCharacter = true;
                                gameboard[newRow2, newColumn2].CharacterIcon = character.Icon;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No hay pared cerca");
                }
            }
        }
    }
}