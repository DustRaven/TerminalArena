using System;
using System.Collections.Generic;
using TerminalArena.Interfaces;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Repräsentiert das Inventar eines Kämpfers.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Eine Liste von Gegenständen.
        /// </summary>
        public List<IItem> Items { get; private set; }

        /// <summary>
        /// Die Kapazität des Inventars.
        /// </summary>
        public int Capacity => Items?.Count ?? 0;

        /// <summary>
        /// Initialisiert ein Inventar mit einer Größe von 10 (falls nicht anders angegeben)
        /// </summary>
        /// <param name="capacity">Die Kapazität des Inventars</param>
        public Inventory(int capacity = 10)
        {
            SetInventoryCapacity(capacity);
        }

        /// <summary>
        /// Prüft, ob im Inventar ein Gegenstand des gesuchten Typs existiert, und gibt das Ergebnis zurück.
        /// </summary>
        /// <param name="itemType">Der Typ des Gegenstands</param>
        /// <returns>Wahr oder Falsch</returns>
        public bool Contains(Type itemType)
        {
            foreach (IItem item in Items)
            {
                if (item.GetType() == itemType)
                {
                    return true;
                }
            }

            return false;
        }

        private void SetInventoryCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                Items = null;
            }
            else if (Items == null)
            {
                Items = new List<IItem>(capacity);
            }
        }
    }
}