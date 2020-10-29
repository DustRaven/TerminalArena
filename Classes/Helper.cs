using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Hilfsklasse. Primär für die Namensgenerierung der Kämpfer.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Gibt einen zufäligen Namen aus der names.json-Datei zurück.
        /// </summary>
        /// <returns>string</returns>
        public static string GetRandomName()
        {
            StreamReader file = File.OpenText(@"names.json");

            List<string> names = JsonConvert.DeserializeObject<List<string>>(file.ReadToEnd());
            return names[GetRandomNumber(0, names.Count)];
        }

        /// <summary>
        /// Generiert eine zufällige Zahl zwischen lower und upper.
        /// </summary>
        /// <param name="lower">Niedrigste Zahl</param>
        /// <param name="upper">Höchste Zahl</param>
        /// <returns>int</returns>
        private static int GetRandomNumber(int lower, int upper)
        {
            Random random = new Random();
            return random.Next(lower, upper);
        }
    }
}
