namespace P_P
{
    public class Board
    {
        private int columns;
        private int rows;
        public string[,] gameBoard;
        public Board(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.gameBoard = new string[rows, columns];
        }
        public string[,] create_board()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.gameBoard[i,j] = "🔲";
                }
            }
            return this.gameBoard;
            }
        public void print_board(string [,] gameboard)
        {
            Console.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(gameBoard[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
