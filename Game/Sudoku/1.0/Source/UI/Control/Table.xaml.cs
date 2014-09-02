using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CdtsGame.Core.Sudoku;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace CdtsGame.UISudoku.Control
{
    public partial class Table : UserControl
    {
        private Facade sudokuFacade;
        private int[] problem;
        private int[] solution;
        private Level gameLevel = Level.Normal;
        private DispatcherTimer timer;
        private DateTime time;

        public Table()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            newGame.Click += new RoutedEventHandler(newGame_Click);
            levelEasyBtn.Click += new RoutedEventHandler(level_Checked);
            levelNormalBtn.Click += new RoutedEventHandler(level_Checked);
            levelHardBtn.Click += new RoutedEventHandler(level_Checked);
            levelDevilBtn.Click += new RoutedEventHandler(level_Checked);
            Cells.ForEach(c => c.Table = this);
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            showStory.Completed += new EventHandler((s, e) =>
            {
                time = DateTime.Now;
                tempDateTime = DateTime.Now;
                timer.Start();
            });
            NewGame();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if ((pp.RotationY % 360) >= -270 && (pp.RotationY % 360) <= -90)
            {
                backGrid.Opacity = 1;
            }
            else
            {
                backGrid.Opacity = 0;
            }
        }

        DateTime tempDateTime;

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan d = DateTime.Now - time;
            TimeSpan dt = DateTime.Now - tempDateTime;

            if (dt.TotalSeconds > 10)
            {
                Score--;
                tempDateTime = DateTime.Now;
            }
            timeTxt.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)d.TotalHours, ((int)d.TotalMinutes) % 60, ((int)d.TotalSeconds) % 60);
        }

        void newGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        public void NewGame()
        {
            sudokuFacade = new Facade(new Sudoku());
            problem = sudokuFacade.GenerateSudoku(gameLevel);
            solution = sudokuFacade.GetSolution();

            for (int i = 0; i < 81; i++)
            {
                Cell cell = Cells[i];
                cell.Reset();
                if (problem[i] != 0)
                {
                    cell.Show();
                    cell.ReadOnly = true;
                    cell.CellValue = problem[i].ToString();
                }
                else
                {
                    cell.ReadOnly = false;
                    cell.CellValue = "";
                    cell.Candidates = sudokuFacade.GetCandidate(i);
                }
            }
            timer.Stop();
            Score = 1000;
            int nums = 0;
            switch (gameLevel)
            {
                case Level.Easy:
                    nums = sudokuFacade.EasyNumber;
                    break;
                case Level.Normal:
                    nums = sudokuFacade.NormalNumber;
                    break;
                case Level.Hard:
                    nums = sudokuFacade.HardNumber;
                    break;
                case Level.Devil:
                    nums = sudokuFacade.DevilNumber;
                    break;
                default:
                    break;
            }
            successTxt.Text = "Congratulations!";
            successTxt.Opacity = 0;
            CellsLeft = 81 - nums;
            timeTxt.Text = "00:00:00";
            showStory.Stop();
            showStory.Begin();
        }
        void level_Checked(object sender, RoutedEventArgs e)
        {
            string name = (sender as ToggleButton).Name;
            if (name.Contains("Easy"))
            {
                ChooseLevel(Level.Easy);

                levelNormalBtn.IsChecked = false;
                levelHardBtn.IsChecked = false;
                levelDevilBtn.IsChecked = false;
            }
            else if (name.Contains("Normal"))
            {
                ChooseLevel(Level.Normal);
                levelEasyBtn.IsChecked = false;
                levelHardBtn.IsChecked = false;
                levelDevilBtn.IsChecked = false;
            }
            else if (name.Contains("Hard"))
            {
                ChooseLevel(Level.Hard);
                levelEasyBtn.IsChecked = false;
                levelNormalBtn.IsChecked = false;
                levelDevilBtn.IsChecked = false;
            }
            else if (name.Contains("Devil"))
            {
                ChooseLevel(Level.Devil);
                levelEasyBtn.IsChecked = false;
                levelNormalBtn.IsChecked = false;
                levelHardBtn.IsChecked = false;
            }
        }
        public void ChooseLevel(Level level)
        {
            gameLevel = level;
            NewGame();
        }
        public void SelectAValue(Cell cell, string value)
        {
            int index = int.Parse(cell.Name.Substring(1)) - 1;
            int oldValue = 0;
            if (!string.IsNullOrEmpty(cell.CellValue))
            {
                oldValue = int.Parse(cell.CellValue);
            }
            cell.CellValue = value;
            int[] affects = new int[1];
            if (oldValue != 0)
            {
                affects = sudokuFacade.ClearCell(index);

            }
            if (!string.IsNullOrEmpty(value))
            {
                int[] temp = sudokuFacade.FillCell(index, int.Parse(value));
                if (temp != null)
                {
                    affects = affects.Union(temp).ToArray();
                }
            }
            cell.Candidates = sudokuFacade.GetCandidate(index);

            if (affects != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    for (int i = 0; i < affects.Length; i++)
                    {
                        if (!Cells[affects[i]].ReadOnly)
                        {
                            Cells[affects[i]].IsAnimation = true;
                            Cells[affects[i]].Candidates = sudokuFacade.GetCandidate(affects[i]);
                            Cells[affects[i]].IsAnimation = false;
                        }
                    }
                }
                else if (oldValue != 0)
                {
                    for (int i = 0; i < affects.Length; i++)
                    {
                        if (!Cells[affects[i]].ReadOnly)
                        {
                            Cells[affects[i]].IsAnimation = true;
                            Cells[affects[i]].Candidates = sudokuFacade.GetCandidate(affects[i]);
                            Cells[affects[i]].IsAnimation = false;
                        }
                    }
                }
            }

            if (oldValue == 0 && !string.IsNullOrEmpty(value))
            {
                CellsLeft--;
                Score--;
            }
            else if (oldValue != 0 && string.IsNullOrEmpty(value))
            {
                CellsLeft++;
                Score--;
            }
            else if (oldValue != 0 && oldValue.ToString() != value)
            {
                Score--;
            }
        }

        private int cellsLeft;

        public int CellsLeft
        {
            get { return cellsLeft; }
            set
            {
                cellsLeft = value;
                cellleftTxt.Text = value.ToString();
                if (value == 0)
                {
                    Cells.ForEach(c => c.ReadOnly = true);
                    successTxt.Text += "\r\nYour Score is:\r\n" + Score;
                    successStory.Begin();
                    timer.Stop();
                }
            }
        }

        private int score = 1000;

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                scoreTxt.Text = value.ToString();
                scoreTxtRed.Text = scoreTxt.Text;
                scoreStory.Begin();
            }
        }

        private List<Cell> cells;
        public List<Cell> Cells
        {
            get
            {
                if (cells == null)
                {
                    cells = new List<Cell>();
                    for (int i = 1; i <= 81; i++)
                    {
                        cells.Add(this.FindName("c" + i) as Cell);
                    }
                }
                return cells;
            }
        }
    }
}
