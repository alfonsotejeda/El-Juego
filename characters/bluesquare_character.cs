using P_P.board;
namespace P_P.characters
{
    public class BlueSquareCharacter : BaseCharacter
    {
        public new string icon;
        public BlueSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public override void UseAbility(Shell[,] gameboard ,BaseCharacter character)
        {
            this.Live += 10;
        }
    }
} 