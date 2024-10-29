namespace P_P{
public class BaseCharacter
{
    public string? name;
    public string? ability;
    public int await_time;
    public BaseCharacter(string? name , string? ability , int await_time)
    {
        this.name = name;
        this.ability = ability;
        this.await_time = await_time;
    }
    public void Move(int x, int y, string[,] matrix , string icon)
    {
        if (x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1))
        {
            matrix[x, y] = icon;
        }
        else
        {
            Console.WriteLine("Invalid move. Staying in the current position.");
        }
    }

}
}
