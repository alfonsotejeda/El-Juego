namespace P_P
{
public class Program
{
    static void Main(string[] args)
    {
        Board board = new Board(5 , 5);
        string [,] game_board = board.create_board();
        board.print_board(game_board);
        BlueSquareCharacter blueSquareCharacter = new BlueSquareCharacter("pedro" , "fly" , 5);
        blueSquareCharacter.Move(4 , 3 , game_board , "🟦");
        board.print_board(game_board);

    }
}
}
