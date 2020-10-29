using TerminalArena.Interfaces;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Abstrakte Klasse für Heiltränke.
    /// </summary>
    public abstract class HealthPotion : IItem
    {
        /// <summary>
        /// Der Name des Heiltranks
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Der Wert des Heiltranks
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Die Anzahl an Lebenspunkten, die der Heiltrank wiederherstellt.
        /// </summary>
        public int HpBonus { get; set; }

        /// <summary>
        /// Erstellt einen Heiltrank mit der angegebenen Stärke.
        /// </summary>
        /// <param name="hpBonus">Die Stärke des Tranks</param>
        protected HealthPotion(int hpBonus)
        {
        }

        /// <summary>
        /// Erstellt einen Heiltrank mit der Stärke 10.
        /// </summary>
        protected HealthPotion()
        {
        }
    }
}
