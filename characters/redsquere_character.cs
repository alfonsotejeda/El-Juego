namespace P_P
{
    public class RedSquareCharacter : BaseCharacter
    {
        public string icon;
        public RedSquareCharacter(string? icon, string? ability, int await_time, ref int player_start_row,ref int player_start_column ) : base(icon, ability, await_time , player_start_row, player_start_column)
        {
            this.icon = icon;
        }
    }
}