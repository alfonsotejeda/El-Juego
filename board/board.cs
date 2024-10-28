namespace P_P
{
    public class Board
    {
        private int columns;
        private int rows;
        private int[,] gameBoard;
        public Board(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.gameBoard = new int[rows, columns];
        }
        public void print_board()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("🔲" + " ");
                }
                Console.WriteLine();
            }
        }
    }
}