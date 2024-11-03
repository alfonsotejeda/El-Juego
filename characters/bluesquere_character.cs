namespace P_P
{
    public class BlueSquareCharacter : BaseCharacter
    {
        public string icon;
        public BlueSquareCharacter(string? icon, string? ability, int await_time, ref int player_start_row,ref int player_start_column ) : base(icon, ability, await_time , player_start_row, player_start_column)
        {
            this.icon = icon;
        }
    }
}