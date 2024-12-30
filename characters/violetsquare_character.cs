using P_P.board;
using P_P.tramps;
using System.Collections.Generic;

namespace P_P.characters
{
    public class VioletSquareCharacter : BaseCharacter
    {
        public VioletSquareCharacter(string name, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn, ref int countdown, ref int visibility)
            : base(name, ability, movementCapacity, playerColumn, playerRow, countdown, visibility)
        {
        }

        public override void UseAbility(Shell[,] gameBoard, BaseCharacter character, List<BaseTramp> tramps, List<BaseCharacter> characters)
        {
            character.MovementCapacity += 2;
        }
    }
}
