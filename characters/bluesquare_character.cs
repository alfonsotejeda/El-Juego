using P_P.board;
using P_P.tramps;

namespace P_P.characters
{
    public class BlueSquareCharacter : BaseCharacter
    {
        public BlueSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn, ref int countdown, ref int visibility)
            : base(icon, ability, movementCapacity, playerColumn, playerRow, countdown, visibility)
        {
            this.Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard ,BaseCharacter character , List<BaseTramp> tramps , List<BaseCharacter> characters)
        {
            this.Live += 10;
        }
    }
}