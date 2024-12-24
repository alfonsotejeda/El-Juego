using P_P.board;
using Spectre.Console;
using P_P.characters;
namespace P_P.PrintingMethods;

public class PrintingMethods
{
    public void PrintGameSpectre(Shell[,] gameBoard , BaseCharacter baseCharacter)
    {
        AnsiConsole.Clear();
            
        // Definir el tamaño del lienzo
        int canvasWidth = gameBoard.GetLength(1)*2;
        int canvasHeight = gameBoard.GetLength(0)*2;
        Canvas canvas = new Canvas(canvasWidth, canvasHeight);

        // Imprimir cada celda como un "píxel" en el lienzo
        for (int i = 0; i < canvasHeight/2; i++)
        {
            for (int j = 0; j < canvasWidth/2; j++)
            {
                    // if (gameBoard[i, j].IsTrophy)
                    // {
                    //     canvas.SetPixel(j , i ,Color.Chartreuse3);
                    //     canvas.SetPixel(j+1 , i ,Color.Chartreuse3);
                    //     canvas.SetPixel(j , i+1 ,Color.Chartreuse3);
                    //     canvas.SetPixel(j+1 , i+1 ,Color.Chartreuse3);
                    // }
                if (gameBoard[i, j].GetType() == typeof(wall))
                {
                    canvas.SetPixel(j * 2, i * 2, Color.Black);
                    canvas.SetPixel(j * 2 + 1, i * 2, Color.Black);
                    canvas.SetPixel(j * 2, i * 2 + 1, Color.Black);
                    canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.Black);
                }
                else if (gameBoard[i, j].GetType() == typeof(path))
                {
                    canvas.SetPixel(j * 2, i * 2, Color.White);
                    canvas.SetPixel(j * 2 + 1, i * 2, Color.White);
                    canvas.SetPixel(j * 2, i * 2 + 1, Color.White);
                    canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.White);
                }
                if (gameBoard[i, j].HasObject)
                {
                    switch (gameBoard[i, j].ObjectType)
                    {
                        case "tramp":
                            canvas.SetPixel(j * 2, i * 2, Color.DarkRed);
                            canvas.SetPixel(j * 2 + 1, i * 2, Color.DarkRed);
                            canvas.SetPixel(j * 2, i * 2 + 1, Color.DarkRed);
                            canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.DarkRed);
                            break;
                    }
                }
                if (gameBoard[i, j].HasCharacter)
                {
                    switch (gameBoard[i, j].CharacterIcon)
                    {
                        case "🟦":
                            canvas.SetPixel(j * 2, i * 2, Color.Blue);
                            canvas.SetPixel(j * 2 + 1, i * 2, Color.Blue);
                            canvas.SetPixel(j * 2, i * 2 + 1, Color.Blue);
                            canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.Blue);
                            break;
                        case "🟥":
                            canvas.SetPixel(j * 2, i * 2, Color.Red);
                            canvas.SetPixel(j * 2 + 1, i * 2, Color.Red);
                            canvas.SetPixel(j * 2, i * 2 + 1, Color.Red);
                            canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.Red);
                            break;
                        case "🟩":
                            canvas.SetPixel(j * 2, i * 2, Color.Green);
                            canvas.SetPixel(j * 2 + 1, i * 2, Color.Green);
                            canvas.SetPixel(j * 2, i * 2 + 1, Color.Green);
                            canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.Green);
                            break;
                        case "🟨":
                            canvas.SetPixel(j * 2, i * 2, Color.Yellow);
                            canvas.SetPixel(j * 2 + 1, i * 2, Color.Yellow);
                            canvas.SetPixel(j * 2, i * 2 + 1, Color.Yellow);
                            canvas.SetPixel(j * 2 + 1, i * 2 + 1, Color.Yellow);
                            break;
                    }
                }
            }
        }
        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left"),
                new Layout("Right")
                    .SplitRows(
                        new Layout("Top"),
                        new Layout("Bottom")));
        // layout["Left"].Size(45);
        // layout["Top"].Size(10);
        // layout["Bottom"].Size(10);
        layout["Left"].Update(canvas);
        layout["Top"].Update(new BarChart()
            .Width(60)
            .Label(baseCharacter.Icon)
            .CenterLabel()
            .AddItem("PlayerLive", baseCharacter.Live, Color.Green)
            .AddItem("PlayerMovements", baseCharacter.MovementCapacity, Color.Blue)
            .AddItem("PlayerCountDonw", 4, Color.Red));
        layout["Bottom"].Update(new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.White)
            .AddColumn("Player")
            .AddColumn("Ability")
            .AddColumn("Movements")
        );
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