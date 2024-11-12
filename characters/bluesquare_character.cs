namespace P_P
{
    public class BlueSquareCharacter : BaseCharacter
    {
        public new string icon;
        public BlueSquareCharacter(string icon, string ability, int awaitTime, ref int playerStartRow, ref int playerStartColumn)
            : base(icon, ability, awaitTime, playerStartColumn, playerStartRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }
    }
} 