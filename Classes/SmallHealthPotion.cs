namespace TerminalArena.Classes
{
    /// <summary>
    /// Ein kleiner Heiltrank, der 5 LP wiederherstellen kann.
    /// Abgeleitet von der Heiltrank-Klasse.
    /// </summary>
    /// <seealso cref="HealthPotion"/>
    public class SmallHealthPotion : HealthPotion
    {
        /// <summary>
        /// Erstellt einen Heiltrank mit der Stärke 5
        /// </summary>
        public SmallHealthPotion() : base(5)
        {
            Name = "Kleiner Heiltrank";
            Value = 5;
        }
    }
}