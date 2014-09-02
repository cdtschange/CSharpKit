using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace UI.Model
{
    public class GameModel
    {
        public GameModel()
        {
            LevelDict = new Dictionary<int, GameLevel>();
            LevelDict.Add(1, new GameLevel(30, 15));
            LevelDict.Add(2, new GameLevel(40, 25));
            LevelDict.Add(3, new GameLevel(50, 30));
            LevelDict.Add(4, new GameLevel(60, 45));
            Grade = 1;
            RecordTxt = "";
            ShortRememberTxt = "";
            ComputerGuess();
        }

        private int grade;

        public int Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                if (LevelDict.ContainsKey(value))
                {
                    SetGameLevel(LevelDict[value]);
                }
            }
        }
        public int TotalInning { get; set; }
        public int NowInning { get; set; }
        public string InningInfo
        {
            get { return NowInning + "/" + TotalInning; }
        }
        public double Score { get; set; }
        public string ScoreInfo
        {
            get { return Math.Round(Score, 1).ToString(); }
        }
        public double PassScore { get; set; }
        public string PassScoreInfo
        {
            get { return Math.Round(PassScore, 1).ToString(); }
        }
        public bool IsPass
        {
            get { return Score >= PassScore; }
        }
        public bool IsEnd
        {
            get
            {
                return NowInning >= TotalInning;
            }
        }
        public int TotalGuessNumber { get; set; }
        public int IGuessNumber { get; set; }
        public int CGuessNumber { get; set; }
        public double IGuessWinPercent
        {
            get
            {
                if (TotalGuessNumber == 0) return 0;
                return IGuessNumber * 1.0 / TotalGuessNumber;
            }
        }
        public double CGuessWinPercent
        {
            get
            {
                if (TotalGuessNumber == 0) return 0;
                return CGuessNumber * 1.0 / TotalGuessNumber;
            }
        }
        public string IGuessWinPercentText
        {
            get { return (int)(IGuessWinPercent * 100) + "%"; }
        }
        public string CGuessWinPercentText
        {
            get { return (int)(CGuessWinPercent * 100) + "%"; }
        }
        public int NowResult { get; set; }

        public GuessType IGuessType { get; set; }
        public GuessType CGuessType { get; set; }
        public string RecordTxt { get; set; }
        public string ShortRememberTxt { get; set; }

        public int RunningWin { get; set; }
        public int RunningLose { get; set; }

        public Dictionary<int, GameLevel> LevelDict { get; set; }


        public void ComputerGuess(GuessType type)
        {
            CGuessType = type;
        }
        public void ComputerGuess(int type)
        {
            GuessType t = GuessType.Default;
            if (type == 1)
            {
                t = GuessType.Paper;
            }
            else if (type == 2)
            {
                t = GuessType.Scissors;
            }
            else if (type == 3)
            {
                t = GuessType.Rock;
            }
            ComputerGuess(t);
        }
        public void ComputerGuess()
        {
            MatchCollection m1 = Regex.Matches(RecordTxt, "(" + ShortRememberTxt + "1)");//bu
            MatchCollection m2 = Regex.Matches(RecordTxt, "(" + ShortRememberTxt + "2)");//jiandao
            MatchCollection m3 = Regex.Matches(RecordTxt, "(" + ShortRememberTxt + "3)");//shitou
            if (m1.Count >= m2.Count && m1.Count >= m3.Count && m1.Count != 0)
            {//jiandao
                ComputerGuess(GetWinGuessType(GuessType.Paper));
            }
            else if (m2.Count >= m3.Count && m2.Count != 0)
            {//shitou
                ComputerGuess(GetWinGuessType(GuessType.Scissors));
            }
            else if (m3.Count != 0)
            {//bu
                ComputerGuess(GetWinGuessType(GuessType.Rock));
            }
            else
            {
                ComputerGuess(new Random((int)DateTime.Now.Ticks).Next(1, 4));
            }
        }
        public void IGuess(GuessType type)
        {
            IGuessType = type;
            RecordGuess();
            int result = GuessTypeCompare(IGuessType, CGuessType);
            UpdateGameInfo(result);
            ComputerGuess();
        }

        private void UpdateGameInfo(int result)
        {
            NowResult = result;
            NowInning++;
            TotalGuessNumber++;
            if (result > 0)
            {
                IGuessNumber++;
                RunningWin++;
                RunningLose = 0;
                Score += 1;
                if (RunningWin > 1)
                {
                    Score += 0.5;
                }
                if (IGuessWinPercent > CGuessWinPercent)
                {
                    Score += 0.5;
                }
            }
            else if (result < 0)
            {
                CGuessNumber++;
                RunningWin = 0;
                RunningLose++;
            }
            else
            {
                RunningWin = 0;
                RunningLose = 0;
                Score += 0.5;
            }
        }

        private void RecordGuess()
        {
            RecordTxt += ((int)IGuessType).ToString();
            if (ShortRememberTxt.Length >= 5)
            {
                ShortRememberTxt = ShortRememberTxt.Substring(1);
            }
            ShortRememberTxt += ((int)IGuessType).ToString();
        }

        public int GuessTypeCompare(GuessType type1, GuessType type2)
        {
            int flag = 0;
            if (type1 == type2)
            {
                flag = 0;
            }
            else
            {
                switch (type1)
                {
                    case GuessType.Paper:
                        if (type2 == GuessType.Rock)
                            flag = 1;
                        else
                            flag = -1;
                        break;
                    case GuessType.Scissors:
                        if (type2 == GuessType.Paper)
                            flag = 1;
                        else
                            flag = -1;
                        break;
                    case GuessType.Rock:
                        if (type2 == GuessType.Scissors)
                            flag = 1;
                        else
                            flag = -1;
                        break;
                    default:
                        break;
                }
            }
            return flag;
        }
        public GuessType GetWinGuessType(GuessType type)
        {
            GuessType result = GuessType.Default;
            switch (type)
            {
                case GuessType.Paper:
                    result = GuessType.Scissors;
                    break;
                case GuessType.Scissors:
                    result = GuessType.Rock;
                    break;
                case GuessType.Rock:
                    result = GuessType.Paper;
                    break;
                default:
                    break;
            }
            return result;
        }

        public void SetGameLevel(GameLevel level)
        {
            NowInning = 0;
            TotalInning = level.TotalInning;
            Score = 0;
            PassScore = level.PassScore;
            RunningWin = 0;
            RunningLose = 0;
        }
    }

    public enum GuessType
    {
        Default, Paper, Scissors, Rock
    }

    public class GameLevel
    {
        public GameLevel(int inning, double passScore)
        {
            TotalInning = inning;
            PassScore = passScore;
        }
        public int TotalInning { get; set; }
        public double PassScore { get; set; }
    }
}
