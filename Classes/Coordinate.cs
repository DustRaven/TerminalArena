using System;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Klasse, um eine Koordinate in der Konsole darzustellen.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Position von Links
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Position von Oben
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Erstellt eine X/Y-Koordinate mit den angegebenen Werten.
        /// </summary>
        /// <param name="x">X (oder left)</param>
        /// <param name="y">Y (oder top)</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Erstellt eine Koordinate aus der momentanen Cursorposition.
        /// </summary>
        public Coordinate()
        {
            X = Console.CursorLeft;
            Y = Console.CursorTop;
        }

        /// <summary>
        /// Setzt den Cursor auf die X/Y-Koordinate zurück.
        /// </summary>
        public void Reset()
        {
            Console.SetCursorPosition(X, Y);
        }

        /// <summary>
        /// Setzt die Koordinate eine Zeile herunter.
        /// </summary>
        public void NextLine()
        {
            Y += 1;
        }
    }
}