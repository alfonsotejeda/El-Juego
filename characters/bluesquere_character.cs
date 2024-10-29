namespace P_P
{
    public class BlueSquareCharacter : BaseCharacter
    {
        public string icon;
        public BlueSquareCharacter(string? icon, string? ability, int await_time) : base(icon, ability, await_time)
        {
            this.icon = icon;
        }
    }
}