namespace P_P
{
    /// <summary>
    /// Representa al personaje del jugador rojo
    /// Hereda de BaseCharacter y se especializa en ataque
    /// </summary>
    public class RedSquareCharacter : BaseCharacter
    {
        /// <summary>
        /// Constructor del personaje rojo
        /// </summary>
        public RedSquareCharacter(string icon, string ability, int await_time, 
            ref int player_start_row, ref int player_start_column) 
            : base("🔶", ability, await_time, player_start_row, player_start_column)
        {
            // No necesitamos sobrescribir el icono aquí
        }
    }
}