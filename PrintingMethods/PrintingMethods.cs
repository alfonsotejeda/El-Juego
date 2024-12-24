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
                if (gameBoard[i, j].GetType() == typeof(wall))
                {
                    PrintPixel(canvas , i , j , Color.Black);
                }
                else if (gameBoard[i, j].GetType() == typeof(path))
                {
                    PrintPixel(canvas , i , j , Color.White);
                }
                if (gameBoard[i, j].HasObject)
                {
                    switch (gameBoard[i, j].ObjectType)
                    {
                        case "tramp":
                            PrintPixel(canvas , i , j , Color.DarkRed);
                            break;
                    }
                }
                if (gameBoard[i, j].HasCharacter)
                {
                    switch (gameBoard[i, j].CharacterIcon)
                    {
                        case "🟦":
                            PrintPixel(canvas , i , j , Color.Blue);
                            break;
                        case "🟥":
                            PrintPixel(canvas , i , j , Color.Red);
                            break;
                        case "🟩":
                            PrintPixel(canvas , i , j , Color.Green);
                            break;
                        case "🟨":
                            PrintPixel(canvas , i , j , Color.Yellow);
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
        layout["Left"].Size(95);
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
        private void PrintPixel(Canvas canvas , int i , int j ,Color color)
        {
            canvas.SetPixel(j * 2, i * 2, color);
            canvas.SetPixel(j * 2 + 1, i * 2, color);
            canvas.SetPixel(j * 2, i * 2 + 1, color);
            canvas.SetPixel(j * 2 + 1, i * 2 + 1, color );
        }
        
}