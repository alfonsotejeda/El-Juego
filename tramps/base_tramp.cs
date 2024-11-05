namespace P_P
{
    public abstract class BaseTrap
    {
        public string icon { get; protected set; }
        
        protected BaseTrap(string icon)
        {
            this.icon = icon;
        }

        public abstract void ApplyEffect(ref int movementsLeft);
    }
}
