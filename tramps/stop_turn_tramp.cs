namespace P_P
{
    public class StopTurnTrap : BaseTrap
    {
        public StopTurnTrap() : base("⚡") { }

        public override void ApplyEffect(ref int movementsLeft)
        {
            movementsLeft = 0;
        }
    }
}
