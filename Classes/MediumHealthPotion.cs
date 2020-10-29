namespace TerminalArena.Classes
{
    /// <summary>
    /// Ein mittlerer Heiltrank, der 20 LP wiederherstellen kann.
    /// Abgeleitet von der Heiltrank-Klasse.
    /// </summary>
    /// <seealso cref="HealthPotion"/>
    public class MediumHealthPotion : HealthPotion
    {
        /// <summary>
        /// Erstellt einen Heiltrank mit der Stärke 20
        /// </summary>
        public MediumHealthPotion() : base(20)
        {
            Name = "Mittlerer Heiltrank";
            Value = 12;
        }
    }
}