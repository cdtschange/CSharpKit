using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdtsGame.Core.Sudoku
{
    /// <summary>
    /// 难度级别
    /// </summary>
    public enum Level
    {
        Easy, Normal, Hard, Devil
    }

    /// <summary>
    /// 外观
    /// </summary>
    public class Facade
    {
        public Facade(ISudoku sudoku)
        {
            EasyNumber = 38;
            NormalNumber = 32;
            HardNumber = 26;
            DevilNumber = 20;

            this.sudoku = sudoku;
        }
        public int EasyNumber { get; set; }
        public int NormalNumber { get; set; }
        public int HardNumber { get; set; }
        public int DevilNumber { get; set; }

        private ISudoku sudoku;
        private int[] solution;

        /// <summary>
        /// 挖空数独
        /// </summary>
        /// <param name="level">难度级别</param>
        /// <returns>返回剩下的不为空的单元格索引</returns>
        private int[] DigCells(Level level)
        {
            int[] fills = null;
            int num = 0;
            switch (level)
            {
                case Level.Easy:
                    num = EasyNumber;
                    break;
                case Level.Normal:
                    num = NormalNumber;
                    break;
                case Level.Hard:
                    num = HardNumber;
                    break;
                case Level.Devil:
                    num = DevilNumber;
                    break;
                default:
                    break;
            }
            fills = new int[num];
            Random r = new Random();
            int index = 0, m = 81 / num + 1;
            for (int i = 0; i < 81; i += m)
            {
                fills[index++] = r.Next(i, i + m);
            }
            while (index < num)
            {
                int t = r.Next(0, 81);
                if (!fills.Contains(t))
                    fills[index++] = t;
            }
            return fills;
        }
        /// <summary>
        /// 重置数独
        /// </summary>
        public void ResetSudoku()
        {
            sudoku.Reset();
        }
        /// <summary>
        /// 生成数独
        /// </summary>
        /// <param name="level">难度</param>
        /// <returns>返回需要求解的数独</returns>
        public int[] GenerateSudoku(Level level)
        {
            sudoku.Type = SudokuType.RandomSudoku;
            solution = sudoku.GenerateSudoku();
            int[] fills = DigCells(level);
            int[] problem = new int[81];
            for (int i = 0; i < problem.Length; i++)
            {
                if (fills.Contains(i))
                {
                    problem[i] = solution[i];
                }
            }
            sudoku.FillSudoku(problem);
            return problem;
        }
        /// <summary>
        /// 填充数独单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <param name="value">值</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        public int[] FillCell(int index, int value)
        {
            return sudoku.FillCell(index, value);
        }
        /// <summary>
        /// 清空数独单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        public int[] ClearCell(int index)
        {
            return sudoku.ClearCell(index);
        }
        /// <summary>
        /// 获取数独单元格的候选数列表
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>候选数列表</returns>
        public int[] GetCandidate(int index)
        {
            return sudoku.GetCandidate(index);
        }
        /// <summary>
        /// 获取数独关联单元格索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回关联单元格索引</returns>
        public int[] GetRelativeCells(int index)
        {
            return sudoku.GetRelativeCells(index);
        }
        /// <summary>
        /// 获取数独解
        /// </summary>
        /// <returns>返回数独解</returns>
        public int[] GetSolution()
        {
            return solution;
        }
        /// <summary>
        /// 显示数独
        /// </summary>
        /// <param name="sudoku">数独数组</param>
        public void Show(int[] table)
        {
            if (table.Length != 81)
            {
                throw new InvalidOperationException("数独数组长度不是81！");
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(string.Format("{0,2}", table[i * 9 + j]));
                }
                Console.WriteLine();
            }
        }

    }
}
