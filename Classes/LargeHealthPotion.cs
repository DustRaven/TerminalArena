namespace TerminalArena.Classes
{
    /// <summary>
    /// Ein mittlerer Heiltrank, der 50 LP wiederherstellen kann.
    /// Abgeleitet von der Heiltrank-Klasse.
    /// </summary>
    /// <seealso cref="HealthPotion"/>
    public class LargeHealthPotion : HealthPotion 
    {
        /// <summary>
        /// Erstellt einen Heiltrank mit der Stärke 50
        /// </summary>
        public LargeHealthPotion() : base(50)
        {
            Name = "Großer Heiltrank";
            Value = 30;
        }
    }
}
