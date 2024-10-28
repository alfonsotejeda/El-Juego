using System.Threading.Tasks.Dataflow;
using P_P;
public class Program
{
    static void Main(string[] args)
    {
        Board board = new Board(5 , 5);
        board.print_board();
    }
}
