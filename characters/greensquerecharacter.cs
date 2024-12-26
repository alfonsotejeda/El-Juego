using P_P.board;
using P_P.tramps;
using P_P.PrintingMethods;
using Spectre.Console;
namespace P_P.characters
{
    public class GreenSquareCharacter : BaseCharacter
    {
        public string icon;
        public GreenSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            bool quitTrampController = false;
            PrintingMethods.PrintingMethods printingMethods = new PrintingMethods.PrintingMethods();
            if (IsInFirstQuadrant(character, gameboard))
            {
                quitTrampController = RemoveTrapInQuadrant(gameboard, 0, gameboard.GetLength(0) / 2, 0, gameboard.GetLength(1) / 2);
            }
            else if (IsInSecondQuadrant(character, gameboard))
            {
                quitTrampController = RemoveTrapInQuadrant(gameboard, 0, gameboard.GetLength(0) / 2, gameboard.GetLength(1) / 2, gameboard.GetLength(1));
            }
            else if (IsInThirdQuadrant(character, gameboard))
            {
                quitTrampController = RemoveTrapInQuadrant(gameboard, gameboard.GetLength(0) / 2, gameboard.GetLength(0), 0, gameboard.GetLength(1) / 2);
            }
            else if (IsInFourthQuadrant(character, gameboard))
            {
                quitTrampController = RemoveTrapInQuadrant(gameboard, gameboard.GetLength(0) / 2, gameboard.GetLength(0), gameboard.GetLength(1) / 2, gameboard.GetLength(1));
            }
            else
            {
                printingMethods.layout["Bottom"].Update(new Panel("No hay trampas cerca").Expand());
            }
            printingMethods.layout["Bottom"].Update(new Panel("Presiona cualquier tecla para continuar...").Expand());
            Console.ReadKey();
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

        private bool RemoveTrapInQuadrant(Shell[,] gameboard, int startRow, int endRow, int startColumn, int endColumn)
        {
            for (int row = startRow; row < endRow; row++)
            {
                for (int column = startColumn; column < endColumn; column++)
                {
                    if (gameboard[row, column].GetType() == typeof(path) && gameboard[row, column].HasObject && gameboard[row, column].ObjectType == "tramp")
                    {
                        printingMethods.layout["Bottom"].Update(new Panel($"Has quitado la trampa: {gameboard[row, column].ObjectId} de la posición {row} , {column}").Expand());
                        gameboard[row, column].HasObject = false;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}