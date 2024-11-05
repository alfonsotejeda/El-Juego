namespace P_P
{
    public class TeleportationPortal
    {
        public string icon = "🌀";
        public (int row, int col) portal1;
        public (int row, int col) portal2;

        public TeleportationPortal((int row, int col) p1, (int row, int col) p2)
        {
            portal1 = p1;
            portal2 = p2;
        }

        public (int, int) Teleport(int currentRow, int currentCol)
        {
            if (currentRow == portal1.row && currentCol == portal1.col)
                return portal2;
            else if (currentRow == portal2.row && currentCol == portal2.col)
                return portal1;
            
            return (currentRow, currentCol);
        }
    }
}
