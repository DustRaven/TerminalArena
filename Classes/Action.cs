
namespace TerminalArena.Classes
{
    /// <summary>
    /// Eine ausführbare Aktion.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Der Name der Aktion. Dieser wird im Interface angezeigt
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Die Länge des Aktionsnamens
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// Eine Zeichenfolge, die die Aktion identifiziert
        /// </summary>
        public string What { get; }

        /// <summary>
        /// Konstruktor einer Aktion
        /// </summary>
        /// <param name="name">Der Name der Aktion</param>
        /// <param name="what">Der Identifikator</param>
        public Action(string name, string what)
        {
            Name = name;
            Length = name.Length;
            What = what;
        }
    }
}
