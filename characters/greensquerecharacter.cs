namespace P_P.characters
{
    public class GreenSquareCharacter : BaseCharacter
    {
        public new string icon;
        public GreenSquareCharacter(string icon, string ability, ref int movementCapacity, ref int playerRow, ref int playerColumn)
            : base(icon, ability, movementCapacity, playerColumn, playerRow)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }
    }
}