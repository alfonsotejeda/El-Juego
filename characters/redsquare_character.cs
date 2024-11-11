namespace P_P
{
    public class RedSquareCharacter : BaseCharacter
    {
        public new string icon;
        public RedSquareCharacter(string icon, string ability, int awaitTime, ref int playerStartRow, ref int playerStartColumn)
            : base(icon, ability, awaitTime, playerStartColumn, playerStartRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }
    }
} 