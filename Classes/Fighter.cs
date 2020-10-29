using System;
using System.Collections.Generic;
using System.Drawing;
using Pastel;
using TerminalArena.Helpers;
using TerminalArena.Interfaces;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Die Implementierung des IEntity-Interfaces.
    /// Hier wird ein Kämpfer erstellt.
    /// </summary>
    public class Fighter : IEntity
    {
        #region Eigenschaften

        /// <summary>
        /// Die Gesundheit des Kämpfers
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Der Schaden, den der Kämpfer verursachen kann
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Die Verteidigung des Kämpfers
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Ein Würfel, der die Ergebnisse der Schadens- und Verteidigungsberechnungen zufälliger macht
        /// </summary>
        private Dice Dice { get; }

        /// <summary>
        /// Der Name des Kämpfers
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Eine Nachricht über ein Ereignis
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Die maximale Gesundheit des Kämpfers
        /// </summary>
        private int MaxHealth { get; }

        /// <summary>
        /// Eine Liste von Gegenständen
        /// </summary>
        private readonly Inventory _inventory;

        /// <summary>
        /// Eine Liste von Aktionen
        /// </summary>
        public List<Action> Actions { get; set; }

        #endregion

        #region Methoden

        /// <summary>
        /// Erstellt einen Kämpfer mit den angegebenen Attributen
        /// </summary>
        /// <param name="name">Der Name des Kämpfers</param>
        /// <param name="health">Die Gesundheit des Kämpfers</param>
        /// <param name="damage">Der Schaden des Kämpfers</param>
        /// <param name="defense">Die Verteidigung des Kämpfers</param>
        public Fighter(string name, int health, int damage, int defense)
        {
            Name = name;
            MaxHealth = health;
            Health = health;
            Damage = damage;
            Defense = defense;
            Dice = new Dice(6);
            _inventory = new Inventory();
            _inventory.Items.Add(new SmallHealthPotion());
            _inventory.Items.Add(new MediumHealthPotion());
            _inventory.Items.Add(new LargeHealthPotion());
            Actions = new List<Action>();
        }

        /// <summary>
        /// Greift den spezifizierten Gegner an
        /// </summary>
        /// <param name="enemy">Der anzugreifende Gegner</param>
        public virtual void Attack(IEntity enemy)
        {
            int calculatedDamage = Damage + Dice.Roll();
            Message = ($"{Name} verursacht {calculatedDamage} Schaden.");
            enemy.Defend(calculatedDamage);
        }

        /// <summary>
        /// Versucht, sich gegen einen Angriff zu verteidigen
        /// </summary>
        /// <param name="damage">Zu verursachender Schaden</param>
        public void Defend(int damage)
        {
            string message;
            int damageTaken = damage - Dice.Roll();
            if (damageTaken > 0)
            {
                Health -= damageTaken;
                message = $"{Name} hat {damageTaken} Punkte Schaden genommen";
                if (Health <= 0)
                {
                    Health = 0;
                    message += " und ist gestorben";
                }
            }
            else
            {
                message = $"{Name} hat den Schaden geblockt";
            }

            Message = message;
        }

        /// <summary>
        /// Gibt den Zustand des Kämpfers zurück
        /// </summary>
        /// <returns>Lebendig ja/nein</returns>
        public bool IsAlive()
        {
            return Health > 0;
        }

        /// <summary>
        /// Erstellt aus gegenwärtiger und maximaler Resource eine Leiste
        /// Dient der grafischen Anzeige der Resource
        /// </summary>
        /// <param name="current">Gegenwärtige Resource</param>
        /// <param name="maximum">Maximale Resource</param>
        /// <returns>Eine Zeichenfolge, die die Resource repräsentiert</returns>
        protected string BarGraphic(int current, int maximum)
        {
            string bar = "";
            int total = 20;
            double count = Math.Round(((double) current / maximum) * total);
            if (count == 0 && IsAlive())
            {
                count = 1;
            }

            for (int i = 0; i < count; i++)
            {
                bar += "█";
            }

            return bar.PadRight(total);
        }

        /// <summary>
        /// Öffentliche Methode zur Darstellung einer Gesundheitsleiste
        /// </summary>
        /// <returns>Eine Zeichenfolge, die die Gesundheit repräsentiert</returns>
        public string HealthBar()
        {
            return "[" + BarGraphic(Health, MaxHealth).Pastel(Color.Red).PastelBg(Color.DarkRed) +
                   $"] {Health} / {MaxHealth}";
        }

        /// <summary>
        /// Liefert die Repräsentation des Fighter-Objekts als String
        /// </summary>
        /// <returns>(string) Fighter-Objekt</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Führt die gewünschte Aktion aus und aktualisiert die Aktionen
        /// </summary>
        /// <param name="action">Die gewünschte Aktion</param>
        public void DoAction(string action)
        {
            switch (action)
            {
                case "potion":
                {
                    UsePotion();
                    break;
                }
                case "attack":
                    break;
                case "flee":
                {
                    Health = 0;
                    Message = $"{Name} hat aufgegeben.";
                    break;
                }
            }
        }

        private void UsePotion()
        {
            if (_inventory.Contains(typeof(HealthPotion)))
            {
                IItem item = _inventory.Items.Find(toFind => toFind.GetType() == typeof(HealthPotion));
                HealthPotion tempItem = (HealthPotion) item;
                if (tempItem != null)
                {
                    if (Health + tempItem.HpBonus > MaxHealth)
                    {
                        Health = MaxHealth;
                    }
                    else
                    {
                        Health += tempItem.HpBonus;
                    }

                    _inventory.Items.Remove(item);
                    Message = $"{Name} hat {item.Name} benutzt.";
                }
            }
            else
            {
                Message = $"{Name} hat versucht, einen Trank zu benutzen, hat aber keinen mehr.";
                Actions.Remove(Actions.Find(action => action.What == "potion"));
            }
        }

        #endregion
    }
}