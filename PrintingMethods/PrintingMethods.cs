using P_P.board;
using Spectre.Console;
namespace P_P.PrintingMethods;

public class PrintingMethods
{
        private Layout CreateLayout(Canvas canvas)
                     {
                             Layout layout = new Layout("Root")
                             .SplitColumns(
                                 new Layout("Left"),
                                 new Layout("Right")
                                     .SplitRows(
                                         new Layout("Top"),
                                         new Layout("Bottom")));
             
                         Panel mazePanel = new Panel(canvas);
                         Panel topPanel = new Panel("");
                         Panel bottomPanel = new Panel("");
                         // Update the left column
                         layout["Left"].Update(
                             mazePanel
                         );
                         layout["Top"].Update(
                             topPanel
                         );
                         layout["Bottom"].Update(
                             bottomPanel
                         );
                         return layout;
                     }
        public void PrintBoardSpectre(Shell[,] gameBoard)
        {
            AnsiConsole.Clear();
            
            // Definir el tamaño del lienzo
            int canvasWidth = gameBoard.GetLength(1);
            int canvasHeight = gameBoard.GetLength(0);
            Canvas canvas = new Canvas(canvasWidth, canvasHeight);

            // Imprimir cada celda como un "píxel" en el lienzo
            for (int i = 0; i < canvasHeight; i++)
            {
                for (int j = 0; j < canvasWidth; j++)
                {
                    if (gameBoard[i, j].IsTrophy)
                    {
                        canvas.SetPixel(j , i ,Color.Chartreuse3);
                    }
                    if (gameBoard[i, j].GetType() == typeof(wall))
                    {
                        canvas.SetPixel(j , i ,Color.Black);
                    }
                    if (gameBoard[i, j].GetType() == typeof(path))
                    {
                        canvas.SetPixel(j , i ,Color.White);
                    }
                    // if (gameBoard[i, j].IsTramp)
                    // {
                    //     canvas.SetPixel(j , i ,Color.Red);
                    // }
                    if (gameBoard[i, j].HasCharacter)
                    {
                        switch (gameBoard[i,j].CharacterIcon)
                        {
                            case "🟦":
                                canvas.SetPixel(j , i ,Color.Blue);
                                break;
                            case "🟥":
                                canvas.SetPixel(j , i ,Color.Red);
                                break;
                            case "🟩":
                                canvas.SetPixel(j , i ,Color.Green);
                                break;
                            case "🟨":
                                canvas.SetPixel(j , i ,Color.Yellow);
                                break;
                        }
                    }
                }
            }

            // Crear el layout
            Layout layout = CreateLayout(canvas);
            AnsiConsole.Write(layout);
            
        }
        
        public void PrintBoard(Shell[,] gameBoard)
        {
            // Console.Clear();
            // for (int i = 0; i < gameBoard.GetLength(0); i++)
            // {
            //     for (int j = 0; j < gameBoard.GetLength(1); j++)
            //     {
            //         if (gameBoard[i, j].HasCharacter)
            //         {
            //             Console.Write(gameBoard[i, j].CharacterIcon + " ");
            //         }
            //         else if (gameBoard[i, j].IsWall)
            //         {
            //             Console.Write(gameBoard[i, j].WallIcon + " ");
            //         }
            //         else if (gameBoard[i, j].IsPath)
            //         {
            //             Console.Write(gameBoard[i, j].PathIcon + " ");
            //         }
            //         else if (gameBoard[i, j].IsTrophy)
            //         {
            //             Console.Write("🏆");
            //         }
            //     }
            //     Console.WriteLine();
            // }
        }
        
}