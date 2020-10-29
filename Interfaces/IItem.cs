namespace TerminalArena.Interfaces
{
    /// <summary>
    /// Beschreibt einen Gegenstand.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Der Name des Gegenstandes
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Der Wert des Gegenstandes
        /// </summary>
        public int Value { get; set; }
    }
}
