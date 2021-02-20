using System;
using System.Collections.Generic;
using NewBallGame.Controllers;
using NewBallGame.Data;
using NewBallGame.Models;

namespace NewBallGame.View {
    public struct MenuItem {
        public string[] Text;
        public int Left;
        public int Top;
        public ConsoleColor FgColor;
        public MenuButton Button;

        public MenuItem(string[] text, int left, int top, int button) {
            Text = text;
            Left = left;
            Top = top;
            FgColor = ConsoleColor.White;
            Button = (MenuButton)button;
        }
    }

    public class Menu : IMenu {
        public const ConsoleColor SELECT_COLOR = ConsoleColor.DarkGray;
        public const int MAX_ITEMS = 6;

        private MenuItem[] _startMenuItems;
        private MenuItem[] _pauseMenuItems;
        private MenuItem[] _levelsListMenuItems;
        private MenuItem[] _gamemodeMenuItems;

        private MenuItem[] _currentItems;
        private List<MenuItem[]> _menuPages;

        private int _itemIdx = 1;
        private int _pageIdx;

        public int SelectedItemIdx {
            get => _itemIdx;
            set {
                if (value > 0 && value < _currentItems.Length) {
                    ChangeBgColor(ConsoleColor.Black);
                    _itemIdx = value;
                    ChangeBgColor();
                }
                else if (_menuPages != null) {
                    if (value <= 0 && _pageIdx > 0) {
                        _currentItems = _menuPages[--_pageIdx];
                        _itemIdx = MAX_ITEMS;
                    }
                    else if (value >= _currentItems.Length && _pageIdx < _menuPages.Count - 1) {
                        _currentItems = _menuPages[++_pageIdx];
                        _itemIdx = 1;
                    }
                    Show();
                }
            }
        }

        public MenuMode CurrentListMode { get; set; }
        public IPlayableController MainController { get; set; }
        private KeyController KeyController { get; set; }

        public Menu(IPlayableController mainController, MenuMode mode) {
            InitializeItems();

            MainController = mainController;
            Run(mode);
            KeyController = new MenuKeyController(this);
        }

        public void ExecuteAction(ConsoleKey key) {
            switch (key) {
                case ConsoleKey.DownArrow:
                    SelectedItemIdx++;
                    break;
                case ConsoleKey.UpArrow:
                    SelectedItemIdx--;
                    break;
                case ConsoleKey.Enter:
                    RunAction();
                    break;
                case ConsoleKey.Escape:
                    Run(MenuMode.Start);
                    break;
            }
        }

        private void RunAction() {
            MenuItem current = _currentItems[SelectedItemIdx];
            switch (current.Button) {
                case MenuButton.Edit:
                    RunLevelsMenu(MenuButton.EditorLevel);
                    break;
                case MenuButton.Exit:
                    Environment.Exit(0);
                    break;
                case MenuButton.Play:
                    Run(MenuMode.Gamemode);
                    break;
                case MenuButton.Resume:
                    MainController.ResumeCurrent();
                    break;
                case MenuButton.Menu:
                    MainController.DisposeCurrent();
                    Run(MenuMode.Start);
                    break;
                case MenuButton.Restart:
                    MainController.RestartCurrent();
                    break;
                case MenuButton.Level:
                    switch (CurrentListMode) {
                        case MenuMode.LvlsList:
                            MainController.Start(new Game(MAX_ITEMS * _pageIdx + _itemIdx, 0, 5, MainController));
                            break;
                        case MenuMode.EditorLvlsList:
                            MainController.Start(new Editor(MAX_ITEMS * _pageIdx + _itemIdx, MainController));
                            break;
                        case MenuMode.AILvlsList:
                            MainController.Start(new AIGame(MAX_ITEMS * _pageIdx + _itemIdx, 0, 5, MainController));
                            break;
                    }
                    break;
                case MenuButton.New:
                    MainController.Start(new Editor(MainController));
                    break;
                case MenuButton.NormalMode:
                    RunLevelsMenu(MenuButton.Level);
                    break;
                case MenuButton.AIMode:
                    RunLevelsMenu(MenuButton.AIMode);
                    break;
            }
        }

        public void Show() {
            Console.Clear();
            for (int i = 0; i < _currentItems.Length; i++) {
                for (int j = 0; j < _currentItems[i].Text.Length; j++) {
                    Console.ForegroundColor = _currentItems[i].FgColor;
                    Console.SetCursorPosition(_currentItems[i].Left, _currentItems[i].Top + j);
                    Console.Write(_currentItems[i].Text[j]);
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            SelectedItemIdx = _itemIdx;
        }

        private void ChangeBgColor(ConsoleColor color = SELECT_COLOR) {
            MenuItem i = _currentItems[SelectedItemIdx];
            for (int j = 0; j < _currentItems[SelectedItemIdx].Text.Length; j++) {
                Console.SetCursorPosition(i.Left, i.Top + j);
                Console.ForegroundColor = i.FgColor;
                Console.BackgroundColor = color;
                Console.Write(i.Text[j]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Run(MenuMode mode) {
            switch (mode) {
                case MenuMode.Start:
                    RunStartMenu();
                    break;
                case MenuMode.Pause:
                    RunPauseMenu();
                    break;
                case MenuMode.Gamemode:
                    RunGamemodeMenu();
                    break;
            }
            Show();
        }

        private void RunPauseMenu() {
            _currentItems = _pauseMenuItems;
            _currentItems[0].FgColor = ConsoleColor.Green;
        }

        private void RunStartMenu() {
            _currentItems = _startMenuItems;
            _currentItems[0].FgColor = ConsoleColor.Green;
        }

        private void RunGamemodeMenu() {
            _currentItems = _gamemodeMenuItems;
            _currentItems[0].FgColor = ConsoleColor.Green;
        }

        private void RunLevelsMenu(MenuButton buttonType) {
            _levelsListMenuItems = DataReader.GetMenuItems(MenuMode.LvlsList);
            int l = _levelsListMenuItems.Length;
            if (buttonType != MenuButton.EditorLevel) {
                CurrentListMode = buttonType == MenuButton.AIMode ? MenuMode.AILvlsList : MenuMode.LvlsList;
                l--;
            }
            else
                CurrentListMode = MenuMode.EditorLvlsList;

            _currentItems = new MenuItem[Math.Min(l, MAX_ITEMS) + 1];
            _currentItems[0] = _startMenuItems[0];
            Array.Copy(_levelsListMenuItems, 0, _currentItems, 1, Math.Min(l, MAX_ITEMS));

            if (l > MAX_ITEMS) {
                _menuPages = new List<MenuItem[]> {
                    _currentItems
                };

                for (int i = 0; i < l / MAX_ITEMS; i++) {
                    int length = (i + 2) * MAX_ITEMS < l ? MAX_ITEMS : (l - (MAX_ITEMS * (i + 1))) % MAX_ITEMS;
                    MenuItem[] items = new MenuItem[++length];
                    items[0] = _startMenuItems[0];
                    for (int j = 0; j < items.Length - 1; j++) {
                        items[j + 1] = _levelsListMenuItems[((i + 1) * MAX_ITEMS) + j];
                    }
                    _menuPages.Add(items);
                }
            }

            Show();
        }

        private void InitializeItems() {
            _startMenuItems = DataReader.GetMenuItems(MenuMode.Start);
            _pauseMenuItems = DataReader.GetMenuItems(MenuMode.Pause);
            _gamemodeMenuItems = DataReader.GetMenuItems(MenuMode.Gamemode);
        }
    }
}
