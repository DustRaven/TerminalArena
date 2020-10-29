using System;
using System.Collections.Generic;
using System.Linq;

namespace TerminalArena.Classes
{
    /// <summary>
    /// Klasse für das User Interface. Hier sind alle Methoden enthalten, die das Interface darstellen.
    /// </summary>
    public class UserInterface
    {
        private const char TopLeft = '╔';
        private const char TopRight = '╗';
        private const char BottomLeft = '╚';
        private const char BottomRight = '╝';
        private const char Horizontal = '═';
        private const char Vertical = '║';
        private const char VerticalLeft = '╠';
        private const char VerticalRight = '╣';
        private const char HorizontalTop = '╦';
        private const char HorizontalBottom = '╩';
        private const char Cross = '╬';

        private const int TitleHeight = 2;
        private const int Padding = 1;
        private readonly Coordinate[] _contentArea;
        private readonly int _contentHeight;
        private readonly int _contentWidth;

        private readonly Coordinate[] _actionArea;
        private readonly int _actionHeight;
        private readonly int _actionWidth;

        /// <summary>
        /// Löscht die Inhalte der Konsole, legt globale Eigenschaften fest und zeichnet Rahmen und Titel.
        /// </summary>
        public UserInterface(string title)
        {
            Console.Clear();
            SetupConsole();
            DrawOuterBorder();

            _contentHeight =
                (Console.WindowHeight / 3) * 2 - TitleHeight - Padding -
                2; // Fensterhöhe / 3 * 2 - Titel + Trenner - Abstand oben & unten - Rahmen oben & unten
            _contentWidth =
                Console.WindowWidth - 2 - 2 * Padding; // Fensterbreite - Rahmen links & rechts - Abstand links & rechts

            _contentArea = new[]
            {
                new Coordinate(1 + Padding, TitleHeight + Padding + 1), // Inhaltsfläche links oben
                new Coordinate(_contentWidth, _contentHeight) // Inhaltsfläche rechts unten
            };


            _actionHeight = Console.WindowHeight - _contentHeight - Padding * 2 - 2 - TitleHeight;
            _actionWidth = _contentWidth;

            _actionArea = new[]
            {
                new Coordinate(1 + Padding, Console.WindowHeight - _actionHeight + Padding),
                new Coordinate(Console.WindowWidth - 1 - Padding, Console.WindowHeight - 1 - Padding)
            };

            DrawTitle(title);
        }

        private void SetupConsole()
        {
            Console.CursorVisible = false;
        }

        private void DrawOuterBorder()
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write(Horizontal);
                Console.SetCursorPosition(x, Console.WindowHeight - 1);
                Console.Write(Horizontal);
            }

            for (int y = 0; y < Console.WindowHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(Vertical);
                Console.SetCursorPosition(Console.WindowWidth - 1, y);
                Console.Write(Vertical);
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(TopLeft);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(BottomLeft);
            Console.SetCursorPosition(Console.WindowWidth - 1, 0);
            Console.Write(TopRight);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 1);
            Console.Write(BottomRight);
            Console.SetCursorPosition(1, 1);
        }

        /// <summary>
        /// Schreibt den Titel des Fensters.
        /// </summary>
        /// <param name="title">Der Titel</param>
        public void DrawTitle(string title)
        {
            ClearTitleArea();
            Coordinate position = new Coordinate(_contentArea[0].X, _contentArea[0].Y);

            CenterString(title, new Coordinate(1, 1));

            Console.SetCursorPosition(0, _contentArea[0].Y - 1 - Padding);

            string separator = VerticalLeft + new string(Horizontal, Console.WindowWidth - 2) + VerticalRight;
            Console.Write(separator);

            position.Reset();
        }

        private void CenterString(string text, Coordinate pos)
        {
            int x = (_contentWidth - text.Length) / 2;
            Console.SetCursorPosition(x, pos.Y);
            Console.Write(text);
        }

        /// <summary>
        /// Zeigt ein Menü mit den gegebenen Optionen an.
        /// Gibt den Index der gewählten Option zurück.
        /// </summary>
        /// <param name="title">Der Titel des Menüs</param>
        /// <param name="options">Eine Liste von Optionen</param>
        /// <returns>Index der gewählten Option</returns>
        public int PrintMenu(string title, params string[] options)
        {
            ClearContentArea();
            ClearTitleArea();

            DrawTitle(title);

            int currentSelection = 0;
            ConsoleKey key;
            do
            {
                Coordinate position =
                    new Coordinate(Console.WindowWidth - _contentWidth, Console.WindowHeight - _contentHeight);

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    CenterString(options[i], position);
                    position.NextLine();
                    Console.ResetColor();
                }


                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.DownArrow:
                    {
                        if (currentSelection + 1 > options.Length - 1)
                        {
                            currentSelection = 0;
                        }
                        else
                        {
                            currentSelection += 1;
                        }

                        break;
                    }

                    case ConsoleKey.UpArrow:
                    {
                        if (currentSelection - 1 < 0)
                        {
                            currentSelection = options.Length - 1;
                        }
                        else
                        {
                            currentSelection -= 1;
                        }

                        break;
                    }
                }
            } while (key != ConsoleKey.Enter);

            return currentSelection;
        }

        /// <summary>
        /// Gibt eine Liste von Aktionen als Menü aus.
        /// </summary>
        /// <param name="actions">Eine Liste von Aktionen</param>
        /// <returns>Die gewählte Aktion</returns>
        public string PrintActions(List<Action> actions)
        {
            Console.SetCursorPosition(_actionArea[0].X, _actionArea[0].Y);
            ConsoleKey key;
            int currentSelection = 0;

            int widestAction = actions.Select(action => action.Length).Prepend(0).Max() + 2;
            int actionsPerLine = _actionWidth / widestAction;

            do
            {
                ClearActionArea();

                for (int i = 0; i < actions.Count; i++)
                {
                    Console.SetCursorPosition(_actionArea[0].X + i % actionsPerLine * widestAction,
                        _actionArea[0].Y + i / actionsPerLine);

                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    Console.Write(actions[i].Name);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (currentSelection >= actionsPerLine)
                        {
                            currentSelection -= actionsPerLine;
                        }

                        break;
                    }

                    case ConsoleKey.DownArrow:
                    {
                        if (currentSelection + actionsPerLine < actions.Count)
                        {
                            currentSelection += actionsPerLine;
                        }

                        break;
                    }
                    case ConsoleKey.LeftArrow:
                    {
                        if (currentSelection % actionsPerLine > 0)
                        {
                            currentSelection--;
                        }

                        break;
                    }

                    case ConsoleKey.RightArrow:
                    {
                        if (currentSelection % actionsPerLine < actionsPerLine - 1)
                        {
                            currentSelection++;
                        }

                        break;
                    }
                }
            } while (key != ConsoleKey.Enter);

            return actions[currentSelection].What;
        }

        /// <summary>
        /// Zeichnet den Rand des Aktionsfeldes
        /// </summary>
        public void DrawActionBorder()
        {
            Coordinate currentPosition = new Coordinate();
            int x = 0;
            int y = (Console.WindowHeight / 3) * 2;

            Coordinate left = new Coordinate(x, y);
            Coordinate right = new Coordinate(Console.WindowWidth - 1, y);

            for (int i = x; i < Console.WindowWidth - 1; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write(Horizontal);
            }

            Console.SetCursorPosition(left.X, left.Y);
            Console.Write(VerticalLeft);

            Console.SetCursorPosition(right.X, right.Y);
            Console.Write(VerticalRight);

            currentPosition.Reset();
        }

        /// <summary>
        /// Löscht den Inhalt des Aktionsbereiches
        /// </summary>
        public void ClearActionArea()
        {
            Coordinate currentPosition = new Coordinate();
            Coordinate position = new Coordinate(_actionArea[0].X, _actionArea[0].Y);
            for (int i = position.Y; i < _actionArea[1].Y; i++)
            {
                Console.SetCursorPosition(position.X, i);
                Console.Write(new string(' ', _contentWidth));
                position.NextLine();
            }

            Console.SetCursorPosition(_actionArea[0].X, _actionArea[0].Y);
            currentPosition.Reset();
        }

        /// <summary>
        /// Zeigt eine Information über das Programm an.
        /// </summary>
        public void PrintInfo()
        {
            Coordinate currentPosition = new Coordinate();
            ClearContentArea();
            CenterString("Terminal Arena", currentPosition);
            currentPosition.NextLine();
            CenterString("v. 1", currentPosition);
            currentPosition.NextLine();
            CenterString("Press enter to return to the menu", currentPosition);
            do
            {
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Löscht den Inhaltsteil des Fensters (alles unterhalb des Titels und innerhalb der Rahmen).
        /// </summary>
        public void ClearContentArea()
        {
            Coordinate position = new Coordinate(_contentArea[0].X, _contentArea[0].Y);

            for (int i = 0 + 1; i < _contentArea[1].Y; i++)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(new string(' ', _contentWidth));
                position.NextLine();
            }

            Console.SetCursorPosition(_contentArea[0].X, _contentArea[0].Y);
        }

        private void ClearTitleArea()
        {
            Coordinate pos = new Coordinate(1, 1);

            string clearString = new string(' ', Console.WindowWidth - 2);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(clearString);
        }

        /// <summary>
        /// Gibt eine Zeichenfolge im Inhaltsbereich aus, ohne in die nächste Zeile zu springen (Wie Console.Write).
        /// </summary>
        /// <param name="message">Zeichenfolge</param>
        public void Print(string message)
        {
            Coordinate pos = new Coordinate();

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(message);
        }

        /// <summary>
        /// Gibt eine Zeichenfolge im Inhaltsbereich aus und springt in die nächste Zeile (wie Console.WriteLine).
        /// Ohne Zeichenfolge wird der  Cursor nur in die nächste Zeile gesetzt.
        /// </summary>
        /// <param name="message">Zeichenfolge</param>
        public void PrintLine(string message = null)
        {
            Coordinate pos = new Coordinate();
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(message);
            Console.SetCursorPosition(pos.X, pos.Y + 1);
        }
    }
}