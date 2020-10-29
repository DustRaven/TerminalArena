using System;

namespace TerminalArena.Helpers
{
    /// <summary>
    /// Repräsentiert einen Würfel mit konfigurierbarer Seitenanzahl.
    /// </summary>
    public class Dice
    {
        private readonly Random _random;
        private readonly int _sides;
        
        /// <summary>
        /// Erstellt einen Würfel mit der gewünschten Seitenzahl.
        /// </summary>
        /// <param name="sides">Seiten des Würfels</param>
        public Dice(int sides)
        {
            _sides = sides;
            _random = new Random();
        }

        /// <summary>
        /// Erstellt einen Würfel mit 6 Seiten.
        /// </summary>
        public Dice()
        {
            _sides = 6;
            _random = new Random();
        }

        /// <summary>
        /// Würfelt mit dem Würfel.
        /// </summary>
        /// <returns>int</returns>
        public int Roll()
        {
            return _random.Next(1, _sides + 1);
        }

        /// <summary>
        /// Gibt die Anzahl der Seiten zurück.
        /// </summary>
        /// <returns>int</returns>
        public int GetSides()
        {
            return _sides;
        }

        /// <summary>
        /// Gibt eine Beschreibung des "Dice"-Objekts als Zeichenfolge zurück.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Würfele mit einem {_sides}-seitigen Würfel";
        }
    }
}
