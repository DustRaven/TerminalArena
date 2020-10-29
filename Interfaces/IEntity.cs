namespace TerminalArena.Interfaces
{
    /// <summary>
    /// Interface für die Kämpferklasse.
    /// Eigenschaften und Methoden, die für alle Kämpfer identisch sein sollen.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gesundheit des Kämpfers
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Schadenswert des Kämpfers
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Verteidigungswert des Kämpfers
        /// </summary>
        int Defense { get; set; }

        /// <summary>
        /// Der Name des Kämpfers
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Eine Nachricht über Ereignisse
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Greift ein Ziel an. Der Schaden und der Verteidigungswert des Gegners werden berechnet.
        /// </summary>
        /// <param name="enemy">Der anzugreifende Gegner</param>
        void Attack(IEntity enemy);

        /// <summary>
        /// Verteidigung gegen einen Angriff. Hier wird entschieden, ob und wie viel Schaden verursacht wird.
        /// </summary>
        /// <param name="damage">Der Schaden, der verursacht werden soll</param>
        void Defend(int damage);
    }
}
