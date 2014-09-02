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
using CdtsGame.Core.Silverlight;

namespace CdtsGame.UISudoku.Control
{
    public partial class Cell : UserControl
    {
        public Cell()
        {
            InitializeComponent();
            ReadOnly = true;
            this.cellGrid.MouseLeftButtonDown += new MouseButtonEventHandler(Cell_MouseLeftButtonDown);
        }

        void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsCandidatePanel = true;
        }

        public void Reset()
        {
            IsCandidatePanel = false;
            error.Storyboard.Stop();
            right.Storyboard.Stop();
        }

        #region Properties

        private bool isCandidatePanel;

        public bool IsCandidatePanel
        {
            get { return isCandidatePanel; }
            set
            {
                if (isCandidatePanel == value)
                    return;
                isCandidatePanel = value;
                if (value)
                {
                    Table.Cells.ForEach(c =>
                    {
                        if (c != this)
                            c.IsCandidatePanel = false;
                    });
                    toCandidate.Begin();
                }
                else
                {
                    toCell.Begin();
                }
            }
        }

        private string cellValue;
        /// <summary>
        /// 单元格值
        /// </summary>
        public string CellValue
        {
            get { return cellValue; }
            set
            {
                cellValue = value;
                cv.Text = value;
            }
        }

        /// <summary>
        /// 单元格索引
        /// </summary>
        public int Index { get; set; }

        private int[] candidates;
        /// <summary>
        /// 候选数列表
        /// </summary>
        public int[] Candidates
        {
            get { return candidates; }
            set
            {
                if (IsAnimation && !ReadOnly)
                {
                    bool isError = false;
                    if (string.IsNullOrEmpty(CellValue))
                    {
                        if (candidates.Length != 0 && value.Length == 0)
                        {
                            isError = true;
                            this.IsHitTestVisible = false;
                            error.Storyboard.Begin();
                        }
                        else if (candidates.Length == 0 && value.Length != 0)
                        {
                            this.IsHitTestVisible = true;
                            right.Storyboard.Begin();
                        }
                    }
                    int s = 0, h = 0;
                    for (int i = 1; i <= 9; i++)
                    {
                        Grid g = (FindName("c" + i) as Grid);
                        if (candidates.Contains(i) && !value.Contains(i))
                        {
                            g.Cursor = Cursors.Arrow;
                            g.IsHitTestVisible = false;
                            h = i;
                        }
                        else if (!candidates.Contains(i) && value.Contains(i))
                        {
                            g.Cursor = Cursors.Hand;
                            g.IsHitTestVisible = true;
                            s = i;
                        }
                    }
                    if (isError)
                    {
                        if (h != 0)
                        {
                            (this.FindName("c" + h) as Grid).Opacity = 0;
                        }
                    }
                    else
                    {
                        if (s != 0)
                        {
                            ShowCandidate(s);
                        }
                        if (h != 0)
                        {
                            HideCandidate(h);
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        Grid g = (FindName("c" + i) as Grid);
                        if (value.Contains(i))
                        {
                            g.Cursor = Cursors.Hand;
                            g.IsHitTestVisible = true;
                            g.Opacity = 1;
                        }
                        else
                        {
                            g.Cursor = Cursors.Arrow;
                            g.IsHitTestVisible = false;
                            g.Opacity = 0;
                        }
                    }
                }
                candidates = value;
            }
        }

        public bool IsAnimation { get; set; }

        public bool IsError { get; set; }

        public void ShowCandidate(int value)
        {
            Storyboard story = new Storyboard();
            story.Duration = TimeSpan.FromSeconds(3);
            Timeline animation = StoryUtility.DoubleAnimation(500, 1000, 0, 1, this.FindName("c" + value) as DependencyObject, "Opacity");
            story.Children.Add(animation);
            toCandidate.Begin();
            candidateChange.Storyboard.Begin();
            story.Begin();
            story.Completed += new EventHandler((s, e) => toCell.Begin());
        }

        public void HideCandidate(int value)
        {
            Storyboard story = new Storyboard();
            story.Duration = TimeSpan.FromSeconds(3);
            Timeline animation = StoryUtility.DoubleAnimation(500, 1000, 1, 0, this.FindName("c" + value) as DependencyObject, "Opacity");
            story.Children.Add(animation);
            toCandidate.Begin();
            candidateChange.Storyboard.Begin();
            story.Begin();
            story.Completed += new EventHandler((s, e) => toCell.Begin());
        }

        public void Show()
        {
            show.Begin();
        }

        private bool readOnly;

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                IsHitTestVisible = !value;
                if (value)
                {
                    cv.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    cv.Foreground = new SolidColorBrush("#FF696969".ToColor());
                }
            }
        }

        public Table Table { get; set; }

        #endregion

        private void Candidate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (sender as Grid).Name.Substring(1);
            if (s == "n")
            {
                s = "";
            }
            (sender as Grid).Background = new SolidColorBrush(Colors.Transparent);
            Table.SelectAValue(this, s);
            IsCandidatePanel = false;
        }

        private void Candidate_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Colors.Yellow);
        }

        private void Candidate_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
