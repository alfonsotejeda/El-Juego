namespace P_P{
public class BaseCharacter
{
    public string? icon;
    public string? ability;
    public int movement_capacity;
    public int player_start_column;
    public int player_start_row;
    public BaseCharacter(string? name , string? ability , int movement_capacity , int player_start_column , int player_start_row)
    {
        this.icon = name;
        this.ability = ability;
        this.movement_capacity = movement_capacity;
        this.player_start_column = player_start_column;
        this.player_start_row = player_start_row;
    }
    public void Move(ref int player_start_row , ref int player_start_column, string[,]game_board,Board board)
    {
            ConsoleKeyInfo key = Console.ReadKey();
            int new_row = player_start_row;
            int new_column = player_start_column;
            //parcheeeeeeeeee!!!!!!!!!!!!!!
            game_board[new_row,new_column] = "⬜️";

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    new_row --;
                    break;
                case ConsoleKey.DownArrow:
                    new_row ++;
                    break;
                case ConsoleKey.LeftArrow:
                    new_column --;
                    break;
                case ConsoleKey.RightArrow:
                    new_column ++;
                    break;
            }
            player_start_row = new_row;
            player_start_column = new_column;
            game_board[player_start_row, player_start_column] = icon;
    }

}
}
