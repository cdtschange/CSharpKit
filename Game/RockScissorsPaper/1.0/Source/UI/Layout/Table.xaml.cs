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
using UI.Model;
using CdtsGame.Core.Silverlight;

namespace UI.Layout
{
    public partial class Table : UserControl
    {
        GameModel gameModel;
        public string ComputerName
        {
            get
            {
                double p = gameModel.CGuessWinPercent;
                if (p < 0.1)
                {
                    ChangeFace(1);
                    return "傻傻小毛";
                }
                else if (p < 0.2)
                {
                    ChangeFace(2);
                    return "白痴小毛";
                }
                else if (p < 0.3)
                {
                    ChangeFace(3);
                    return "笨蛋小毛";
                }
                else if (p < 0.4)
                {
                    ChangeFace(4);
                    return "呆呆小毛";
                }
                else if (p < 0.5)
                {
                    ChangeFace(5);
                    return "普通小毛";
                }
                else if (p < 0.6)
                {
                    ChangeFace(6);
                    return "聪明小毛";
                }
                else if (p < 0.7)
                {
                    ChangeFace(7);
                    return "神奇小毛";
                }
                else if (p < 0.8)
                {
                    ChangeFace(8);
                    return "天才小毛";
                }
                else if (p < 0.9)
                {
                    ChangeFace(9);
                    return "神通小毛";
                }
                else
                {
                    ChangeFace(10);
                    return "无敌小毛";
                }
            }
        }

        private void ChangeFace(int i)
        {
            face1Img.Visibility = i == 1 ? Visibility.Visible : Visibility.Collapsed;
            face2Img.Visibility = i == 2 ? Visibility.Visible : Visibility.Collapsed;
            face3Img.Visibility = i == 3 ? Visibility.Visible : Visibility.Collapsed;
            face4Img.Visibility = i == 4 ? Visibility.Visible : Visibility.Collapsed;
            face5Img.Visibility = i == 5 ? Visibility.Visible : Visibility.Collapsed;
            face6Img.Visibility = i == 6 ? Visibility.Visible : Visibility.Collapsed;
            face7Img.Visibility = i == 7 ? Visibility.Visible : Visibility.Collapsed;
            face8Img.Visibility = i == 8 ? Visibility.Visible : Visibility.Collapsed;
            face9Img.Visibility = i == 9 ? Visibility.Visible : Visibility.Collapsed;
            face10Img.Visibility = i == 10 ? Visibility.Visible : Visibility.Collapsed;
        }
        public string ResultText
        {
            get
            {
                if (gameModel.RunningWin > 0)
                {
                    return "你赢了！";
                }
                else if (gameModel.RunningLose > 0)
                {
                    return "你输了！";
                }
                else
                {
                    return "平局！";
                }
            }
        }
        public int Result
        {
            get
            {
                if (gameModel.RunningWin > 0)
                {
                    return 1;
                }
                else if (gameModel.RunningLose > 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string RunWinText
        {
            get
            {
                if (gameModel.RunningWin > 1)
                {
                    return gameModel.RunningWin + "连胜";
                }
                else if (gameModel.RunningLose > 1)
                {
                    return gameModel.RunningLose + "连败";
                }
                else
                {
                    return "";
                }
            }
        }

        public ImageSource FaceImageResult
        {
            get
            {
                if (gameModel.RunningWin > 2)
                {
                    return facep3Img.Source;
                }
                else if (gameModel.RunningWin > 1)
                {
                    return facep2Img.Source;
                }
                else if (gameModel.RunningWin > 0)
                {
                    return facep1Img.Source;
                }
                else if (gameModel.RunningLose > 2)
                {
                    return facen3Img.Source;
                }
                else if (gameModel.RunningLose > 1)
                {
                    return facen2Img.Source;
                }
                else if (gameModel.RunningLose > 0)
                {
                    return facen1Img.Source;
                }
                else
                {
                    return facedImg.Source;
                }
            }
        }
        public string RecordTxt
        {
            get
            {
                return gameModel.RecordTxt;
            }
            set
            {
                gameModel.RecordTxt = value;
            }
        }
        private int gradeLevel = 1;
        public string GradeLevel
        {
            get { return gradeLevel.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    gradeLevel = int.Parse(value);
                }

                if (gradeLevel > 0)
                {
                    grad1Img.Visibility = Visibility.Visible;
                }
                if (gradeLevel > 1)
                {
                    grad2Img.Visibility = Visibility.Visible;
                }
                if (gradeLevel > 2)
                {
                    grad3Img.Visibility = Visibility.Visible;
                }
                if (gradeLevel > 3)
                {
                    grad4Img.Visibility = Visibility.Visible;
                }
                if (gradeLevel > 4)
                {
                    cupImg.Visibility = Visibility.Visible;
                }
            }
        }

        int cRockNumber = 5;

        public int CRockNumber
        {
            get { return cRockNumber; }
            set { cRockNumber = value; crockNumberTxt.Text = value + "/5"; }
        }
        int cScissorNumber = 5;

        public int CScissorNumber
        {
            get { return cScissorNumber; }
            set { cScissorNumber = value; cscissorNumberTxt.Text = value + "/5"; }
        }
        int cPaperNumber = 5;

        public int CPaperNumber
        {
            get { return cPaperNumber; }
            set { cPaperNumber = value; cpaperNumberTxt.Text = value + "/5"; }
        }
        int iRockNumber = 5;

        public int IRockNumber
        {
            get { return iRockNumber; }
            set { iRockNumber = value; irockNumberTxt.Text = value + "/5"; }
        }
        int iScissorNumber = 5;

        public int IScissorNumber
        {
            get { return iScissorNumber; }
            set { iScissorNumber = value; iscissorNumberTxt.Text = value + "/5"; }
        }
        int iPaperNumber = 5;

        public int IPaperNumber
        {
            get { return iPaperNumber; }
            set { iPaperNumber = value; ipaperNumberTxt.Text = value + "/5"; }
        }

        public bool IsBossEnd
        {
            get { return icardPanel.Children.Count == 0; }
        }
        public int BossResult
        {
            get { return int.Parse(iScoreLbl.Content.ToString()) - int.Parse(cScoreLbl.Content.ToString()); }
        }

        public Table()
        {
            InitializeComponent();
            gameModel = new GameModel();
            this.Loaded += new RoutedEventHandler(Table_Loaded);
        }

        void Table_Loaded(object sender, RoutedEventArgs e)
        {
            rockChooseImg.MouseEnter += new MouseEventHandler(rockChooseImg_MouseEnter);
            rockChooseImg.MouseLeave += new MouseEventHandler(rockChooseImg_MouseLeave);
            rockChooseImg.MouseLeftButtonDown += new MouseButtonEventHandler(rockChooseImg_MouseLeftButtonDown);
            scissorChooseImg.MouseEnter += new MouseEventHandler(scissorChooseImg_MouseEnter);
            scissorChooseImg.MouseLeave += new MouseEventHandler(scissorChooseImg_MouseLeave);
            scissorChooseImg.MouseLeftButtonDown += new MouseButtonEventHandler(scissorChooseImg_MouseLeftButtonDown);
            paperChooseImg.MouseEnter += new MouseEventHandler(paperChooseImg_MouseEnter);
            paperChooseImg.MouseLeave += new MouseEventHandler(paperChooseImg_MouseLeave);
            paperChooseImg.MouseLeftButtonDown += new MouseButtonEventHandler(paperChooseImg_MouseLeftButtonDown);
            chooseRockStory.Storyboard.Completed += new EventHandler(rockStoryboard_Completed);
            chooseScissorStory.Storyboard.Completed += new EventHandler(scissorStoryStoryboard_Completed);
            choosePaperStory.Storyboard.Completed += new EventHandler(paperStoryboard_Completed);
            grad1Img.MouseLeftButtonUp += new MouseButtonEventHandler(grad1Img_MouseLeftButtonUp);
            grad2Img.MouseLeftButtonUp += new MouseButtonEventHandler(grad2Img_MouseLeftButtonUp);
            grad3Img.MouseLeftButtonUp += new MouseButtonEventHandler(grad3Img_MouseLeftButtonUp);
            grad4Img.MouseLeftButtonUp += new MouseButtonEventHandler(grad4Img_MouseLeftButtonUp);
            cupImg.MouseLeftButtonUp += new MouseButtonEventHandler(cupImg_MouseLeftButtonUp);
            showResultStory.Storyboard.Completed += new EventHandler(Storyboard_Completed);
            cupImg_Copy.MouseLeftButtonUp += new MouseButtonEventHandler(cupImg_Copy_MouseLeftButtonUp);
            UpdateGameInfo();
        }

        void cupImg_Copy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            showNormalTableStory.Storyboard.Begin();
        }

        void Storyboard_Completed(object sender, EventArgs e)
        {
            tipTxt.Visibility = Visibility.Visible;
        }

        void grad1Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectGrade(1);
        }
        void grad2Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectGrade(2);
        }
        void grad3Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectGrade(3);
        }
        void grad4Img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectGrade(4);
        }
        void cupImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectGrade(5);
        }

        void rockStoryboard_Completed(object sender, EventArgs e)
        {
            CheckGame();
        }
        void scissorStoryStoryboard_Completed(object sender, EventArgs e)
        {
            CheckGame();
        }
        void paperStoryboard_Completed(object sender, EventArgs e)
        {
            CheckGame();
        }

        void rockChooseImg_MouseEnter(object sender, MouseEventArgs e)
        {
            rockOnStory.Storyboard.Begin();
        }
        void rockChooseImg_MouseLeave(object sender, MouseEventArgs e)
        {
            rockOnStory.Storyboard.Stop();
        }
        void rockChooseImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            iguessImg.Source = rockChooseImg.Source;
            ReadComputerGuessType();
            BeginGuess(GuessType.Rock);
            UpdateResultInfo();
            chooseRockStory.Storyboard.Begin();
        }

        void scissorChooseImg_MouseEnter(object sender, MouseEventArgs e)
        {
            scissorOnStory.Storyboard.Begin();
        }
        void scissorChooseImg_MouseLeave(object sender, MouseEventArgs e)
        {
            scissorOnStory.Storyboard.Stop();
        }
        void scissorChooseImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            iguessImg.Source = scissorChooseImg.Source;
            ReadComputerGuessType();
            BeginGuess(GuessType.Scissors);
            UpdateResultInfo();
            chooseScissorStory.Storyboard.Begin();
        }
        void paperChooseImg_MouseEnter(object sender, MouseEventArgs e)
        {
            paperOnStory.Storyboard.Begin();
        }
        void paperChooseImg_MouseLeave(object sender, MouseEventArgs e)
        {
            paperOnStory.Storyboard.Stop();
        }
        void paperChooseImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            iguessImg.Source = paperChooseImg.Source;
            ReadComputerGuessType();
            BeginGuess(GuessType.Paper);
            UpdateResultInfo();
            choosePaperStory.Storyboard.Begin();
        }

        private void UpdateResultInfo()
        {
            faceTxtTitle.Text = ResultText;
            faceTxtRunning.Text = RunWinText;
            switch (Result)
            {
                case 1:
                    faceTxtTitle.Foreground = new SolidColorBrush(Colors.Green);
                    faceTxtRunning.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case -1:
                    faceTxtTitle.Foreground = new SolidColorBrush("#FFCE0000".ToColor());
                    faceTxtRunning.Foreground = new SolidColorBrush("#FFCE0000".ToColor());
                    break;
                case 0:
                    faceTxtTitle.Foreground = new SolidColorBrush(Colors.Black);
                    faceTxtRunning.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                default:
                    break;
            }
            faceImg.Source = FaceImageResult;
        }

        private void ReadComputerGuessType()
        {
            switch (gameModel.CGuessType)
            {
                case GuessType.Paper:
                    cguessImg.Source = paperChooseImg.Source;
                    break;
                case GuessType.Scissors:
                    cguessImg.Source = scissorChooseImg.Source;
                    break;
                case GuessType.Rock:
                    cguessImg.Source = rockChooseImg.Source;
                    break;
                default:
                    break;
            }
        }

        private void BeginGuess(GuessType type)
        {
            gameModel.IGuess(type);
            UpdateGameInfo();
        }

        public void SelectGrade(int grade)
        {
            gameModel.Grade = grade;
            chooseRockStory.Storyboard.Stop();
            chooseScissorStory.Storyboard.Stop();
            choosePaperStory.Storyboard.Stop();
            if (grade == 5)
            {
                BossTableInitial();
                showBossTableStory.Storyboard.Begin();
            }
            else
            {
                UpdateGameInfo();
            }
        }


        private void UpdateGameInfo()
        {
            iguessWinPercentLbl.Content = gameModel.IGuessWinPercentText;
            cguessWinPercentLbl.Content = gameModel.CGuessWinPercentText;
            inningInfoLbl.Content = gameModel.InningInfo;
            scoreLbl.Content = gameModel.ScoreInfo;
            gradeLbl.Content = gameModel.Grade;
            passScoreLbl.Content = gameModel.PassScoreInfo;
            cName.Content = ComputerName;
        }

        private void CheckGame()
        {
            if (gameModel.IsPass)
            {
                if (gameModel.Grade < gameModel.LevelDict.Count)
                {
                    if (gameModel.Grade < int.Parse(GradeLevel))
                    {
                        MsgWindow mw = new MsgWindow("恭喜你过关了！", "点击确定进入下一关！", null);
                        mw.Show();
                        gameModel.Grade++;
                    }
                    else
                    {
                        MsgWindow mw = new MsgWindow("恭喜你过关了！", "此关你得到了" + gameModel.Grade + "级兵符，此兵符将使你能开启下一关！", (this.FindName("grad" + gameModel.Grade + "Img") as Image).Source);
                        mw.Show();
                        gameModel.Grade++;
                        GradeLevel = gameModel.Grade.ToString();
                    }
                    chooseRockStory.Storyboard.Stop();
                    chooseScissorStory.Storyboard.Stop();
                    choosePaperStory.Storyboard.Stop();
                    UpdateGameInfo();
                }
                else
                {
                    MsgWindow mw = new MsgWindow("恭喜你过关了！", "你拿到了所有的兵符，点击确定进入最终Boss关卡！", grad4Img.Source);
                    mw.Show();
                    gameModel.Grade++;
                    GradeLevel = gameModel.Grade.ToString();
                    chooseRockStory.Storyboard.Stop();
                    chooseScissorStory.Storyboard.Stop();
                    choosePaperStory.Storyboard.Stop();
                    BossTableInitial();
                    showBossTableStory.Storyboard.Begin();
                }
            }
            else if (gameModel.IsEnd)
            {
                MsgWindow mw = new MsgWindow("很抱歉你失败了！", "胜败乃兵家常事，请大侠从新来过！", faceCryImg.Source);
                mw.Show();
                gameModel.Grade = gameModel.Grade;
                chooseRockStory.Storyboard.Stop();
                chooseScissorStory.Storyboard.Stop();
                choosePaperStory.Storyboard.Stop();
                UpdateGameInfo();
            }
        }

        private void BossTableInitial()
        {
            CRockNumber = 5;
            CScissorNumber = 5;
            CPaperNumber = 5;
            IRockNumber = 5;
            IScissorNumber = 5;
            IPaperNumber = 5;
            iScoreLbl.Content = "0";
            cScoreLbl.Content = "0";
            iguessImg1.Source = null;
            showResultStory.Storyboard.Stop();
            Image chead = cheadImg;
            ccardPanel.Children.Clear();
            int i = 1;
            ccardPanelBackup.Children.ToList().ForEach(c =>
                {
                    Image im = new Image();
                    Image cm = c as Image;
                    im.Name = i++.ToString();
                    im.Height = cm.Height;
                    im.Margin = cm.Margin;
                    im.Source = cm.Source;
                    im.Cursor = cm.Cursor;
                    ccardPanel.Children.Add(im);
                });
            ccardPanel.Children.Add(chead);
            icardPanel.Children.Clear();
            icardPanelBackup.Children.ToList().ForEach(c =>
            {
                Image im = new Image();
                Image cm = c as Image;
                im.Name = cm.Name.Replace("Backup", "");
                im.Height = cm.Height;
                im.Margin = cm.Margin;
                im.Source = cm.Source;
                im.Cursor = cm.Cursor;
                im.MouseLeftButtonUp += new MouseButtonEventHandler(CardImage_MouseLeftButtonUp);
                icardPanel.Children.Add(im);
            });
        }

        private void CardImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tipTxt.Visibility = Visibility.Collapsed;
            Image im = sender as Image;
            iguessImg1.Source = im.Source;
            switch (gameModel.CGuessType)
            {
                case GuessType.Paper:
                    cguessImg1.Source = paperCardImg.Source;
                    if (CPaperNumber > 0)
                        CPaperNumber--;
                    else
                    {
                        if (CRockNumber > 0)
                        {
                            cguessImg1.Source = rockCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Rock);
                            CRockNumber--;
                        }
                        else
                        {
                            cguessImg1.Source = scissorCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Scissors);
                            CScissorNumber--;
                        }
                    }
                    break;
                case GuessType.Scissors:
                    cguessImg1.Source = scissorCardImg.Source;
                    if (CScissorNumber > 0)
                        CScissorNumber--;
                    else
                    {
                        if (CPaperNumber > 0)
                        {
                            cguessImg1.Source = paperCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Paper);
                            CPaperNumber--;
                        }
                        else
                        {
                            cguessImg1.Source = rockCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Rock);
                            CRockNumber--;
                        }
                    }
                    break;
                case GuessType.Rock:
                    cguessImg1.Source = rockCardImg.Source;
                    if (CRockNumber > 0)
                        CRockNumber--;
                    else
                    {
                        if (CScissorNumber > 0)
                        {
                            cguessImg1.Source = scissorCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Scissors);
                            CScissorNumber--;
                        }
                        else
                        {
                            cguessImg1.Source = paperCardImg.Source;
                            gameModel.ComputerGuess(GuessType.Paper);
                            CPaperNumber--;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (im.Name.StartsWith("r"))
            {
                IRockNumber--;
                BeginGuess(GuessType.Rock);
            }
            else if (im.Name.StartsWith("p"))
            {
                IPaperNumber--;
                BeginGuess(GuessType.Paper);
            }
            else if (im.Name.StartsWith("s"))
            {
                IScissorNumber--;
                BeginGuess(GuessType.Scissors);
            }
            icardPanel.Children.Remove(im);
            Image img = ccardPanel.Children[ccardPanel.Children.Count - 1] as Image;
            if (!img.Name.StartsWith("cheadImg"))
            {
                ccardPanel.Children.Remove(img);
            }
            else
            {
                ccardPanel.Children.RemoveAt(ccardPanel.Children.Count - 2);
            }
            UpdateBossResultInfo();
            showResultStory.Storyboard.Begin();
            CheckBossEnd();
        }
        private void UpdateBossResultInfo()
        {
            faceTxtTitle1.Text = ResultText;
            faceTxtRunning1.Text = RunWinText;
            switch (Result)
            {
                case 1:
                    faceTxtTitle1.Foreground = new SolidColorBrush(Colors.Green);
                    faceTxtRunning1.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case -1:
                    faceTxtTitle1.Foreground = new SolidColorBrush("#FFCE0000".ToColor());
                    faceTxtRunning1.Foreground = new SolidColorBrush("#FFCE0000".ToColor());
                    break;
                case 0:
                    faceTxtTitle1.Foreground = new SolidColorBrush(Colors.Black);
                    faceTxtRunning1.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                default:
                    break;
            }
            faceImg1.Source = FaceImageResult;

            if (Result > 0)
            {
                iScoreLbl.Content = (10 + int.Parse(iScoreLbl.Content.ToString())).ToString();
            }
            else if (Result < 0)
            {
                cScoreLbl.Content = (10 + int.Parse(cScoreLbl.Content.ToString())).ToString();
            }
            else
            {
                iScoreLbl.Content = (10 + int.Parse(iScoreLbl.Content.ToString())).ToString();
                cScoreLbl.Content = (10 + int.Parse(cScoreLbl.Content.ToString())).ToString();
            }
        }

        private void CheckBossEnd()
        {
            if (IsBossEnd)
            {
                if (BossResult > 0)
                {
                    MsgWindow mw = new MsgWindow("恭喜你打赢了Boss！", "你获得了最高荣誉奖杯，你已经成为石头剪子布之王！", cupImg.Source);
                    mw.Show();
                    return;
                }
                else if (BossResult < 0)
                {
                    MsgWindow mw = new MsgWindow("很抱歉你失败了！", "能走到这一步也不容易，希望你再接再厉！胜败乃兵家常事，请大侠从新来过！", faceCryImg.Source);
                    mw.Show();
                    mw.Closed += new EventHandler(mw_Closed);
                    return;
                }
                else
                {
                    MsgWindow mw = new MsgWindow("只差一点了！", "很可惜是平局，你还是没能战胜无所不能的Boss，希望你再接再厉！", faceCryImg.Source);
                    mw.Show();
                    mw.Closed += new EventHandler(mw_Closed);
                    return;
                }
            }
        }

        void mw_Closed(object sender, EventArgs e)
        {
            BossTableInitial();
        }
    }
}
