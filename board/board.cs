namespace P_P
{
    public class Board
    {
        private int columns;
        private int rows;
        public string[,] gameBoard;

        public string wall = "🟫";
        public Board(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
            this.gameBoard = new string[rows, columns];
        }
        public string[,] create_board()
        {
            Maze_Generator maze_Generator = new Maze_Generator();
            maze_Generator.Generate_Empty_Maze(this.gameBoard , wall);

            maze_Generator.Generate_Maze(0,rows/2,0,columns/2,wall,"⬜️",this.gameBoard);
            maze_Generator.Generate_Maze(rows-1,rows/2,0,columns/2,wall,"⬜️",this.gameBoard);
            maze_Generator.Generate_Maze(rows/2,rows-1,columns/2,columns-1,wall,"⬜️",this.gameBoard);
            maze_Generator.Generate_Maze(0,rows/2,columns/2,columns-1,wall,"⬜️",this.gameBoard);
            this.gameBoard[15,15] = "🏆";
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
