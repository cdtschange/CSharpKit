using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdtsGame.Core.Sudoku
{
    /// <summary>
    /// 数独
    /// </summary>
    public class Sudoku : ISudoku
    {
        #region Fields

        /// <summary>
        /// 候选数组
        /// </summary>
        /// <remarks>
        /// 候选数组：123456789
        /// 二进制位：000000000
        /// 有候选数，二进制位标识为0
        /// 如果二进制位标识为1，表示无此候选数
        /// </remarks>
        private int[] candidateArray = new int[81];
        /// <summary>
        /// 数独数组
        /// </summary>
        private int[] sudokuArray = new int[81];
        /// <summary>
        /// 普通游标
        /// </summary>
        private int normalIndex = 0;
        /// <summary>
        /// 记录堆栈
        /// </summary>
        private Stack<Dictionary<int, int>> recordStack = new Stack<Dictionary<int, int>>();
        /// <summary>
        /// 随机数产生器
        /// </summary>
        private Random ran = new Random(DateTime.Now.Millisecond);

        #endregion

        /// <summary>
        /// 构造器
        /// </summary>
        public Sudoku()
        {
            this.Type = SudokuType.RandomSudoku;
            Reset();
        }

        #region Methods
        /// <summary>
        /// 验证并更新相关单元格候选数
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        /// <returns>返回验证结果</returns>
        private bool UpdateRelativeCellsCandidate(int index, int value)
        {
            bool fail = false;
            Dictionary<int, int> dict = new Dictionary<int, int>(20);
            if (value == 0)
            { return false; }
            //当前单元格候选数移除填入的数，不做恢复
            candidateArray[index] |= (1 << (9 - value));
            dict.Add(index, candidateArray[index]);
            //获取关联单元格的数组值
            List<int> relatives = GetRelativeCells(index).ToList();
            relatives.ForEach(i =>
            {
                dict.Add(i, candidateArray[i]); // 备份候选数
            });
            recordStack.Push(dict); // 保存候选数记录

            sudokuArray[index] = value;

            relatives.ForEach(i =>
            {
                if (sudokuArray[index] != 0)
                {
                    candidateArray[i] |= (1 << (9 - sudokuArray[index]));
                    fail &= candidateArray[i] == 512;
                }
            });
            return !fail;
        }
        /// <summary>
        /// 填充单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>是否填充成功</returns>
        private bool FillCell(int index)
        {
            if (sudokuArray[index] != 0)
            { //如果数独此单元格已经填有数字，则返回
                return true;
            }
            while (candidateArray[index] < 511)
            { //从候选数组取值填入数独
                switch (this.Type)
                {
                    case SudokuType.RandomSudoku:
                        int r = ran.Next(0, 9);
                        bool forward = r % 2 == 1;
                        uint rdiv = 1u << r; int rnum = 9 - r;
                        uint origin = rdiv;
                        do
                        {
                            if ((candidateArray[index] & rdiv) == 0)
                            { //该位的候选数通过，填入候选数
                                if (UpdateRelativeCellsCandidate(index, rnum))
                                {
                                    return true;
                                }
                                else
                                { //恢复一次候选数记录
                                    Rollback(recordStack.Pop());
                                }
                            }
                            //尝试下一个候选数
                            if (forward)
                            {
                                rdiv = rdiv >> 1;
                                rnum++;
                            }
                            else
                            {
                                rdiv = rdiv << 1;
                                rnum--;
                            }
                            if (rdiv == 0)
                            {
                                rdiv = 1 << 8; rnum = 1;
                            }
                            else if (rdiv == 1 << 9)
                            {
                                rdiv = 1; rnum = 9;
                            }
                        }
                        while (rdiv != origin);
                        break;
                    case SudokuType.NormalSudoku:
                        uint div = 1 << 8; int num = 1;
                        while (div > 0)
                        {
                            if ((candidateArray[index] & div) == 0)
                            { //该位的候选数通过，填入候选数
                                if (UpdateRelativeCellsCandidate(index, num))
                                {
                                    return true;
                                }
                                else
                                { //恢复一次候选数记录
                                    Rollback(recordStack.Pop());
                                }
                            }
                            //尝试下一个候选数
                            div = div >> 1;
                            num++;
                        }
                        break;
                    default:
                        break;
                }
            }
            //所有候选数都无法满足条件，返回失败
            return false;
        }
        /// <summary>
        /// 回滚一次操作
        /// </summary>
        /// <param name="dict">恢复字典</param>
        /// <returns>返回下一个单元格的游标</returns>
        private int Rollback(Dictionary<int, int> dict)
        {
            bool first = true;
            foreach (KeyValuePair<int, int> v in dict)
            {
                if (first)
                {
                    sudokuArray[v.Key] = 0;
                    first = false;
                }
                else
                {
                    candidateArray[v.Key] = v.Value;
                }
            }
            for (int i = 0; i < 81; i++)
            {
                if (sudokuArray[i] == 0)
                {
                    return i;
                }
            }
            return 81;
        }
        /// <summary>
        /// 检查数独是否填充完毕
        /// </summary>
        /// <returns></returns>
        private int CheckSudoku()
        {
            for (int i = 0; i < 81; i++)
            {
                if (sudokuArray[i] == 0)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < 81; i++)
            {
                candidateArray[i] = 0;
                sudokuArray[i] = 0;
            }
            recordStack.Clear();
            normalIndex = 0;
        }
        /// <summary>
        /// 获取关联单元格索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回关联单元格索引</returns>
        public int[] GetRelativeCells(int index)
        {
            int[] relatives = new int[20];
            int k, l, count = 0;
            for (int i = 0; i < 9; i++) //关联行列单元格
            {
                k = i + (index / 9) * 9; l = i * 9 + index % 9;
                if (k != index)
                { relatives[count++] = k; }
                if (l != index)
                { relatives[count++] = l; }
            }
            //关联九宫格
            int fx = (index % 9) / 3 * 3, fy = (index / 9) / 3 * 3;
            int g = fx + fy * 9;
            if (g != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g += 7;
            if (g != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g + 11 != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g += 7;
            if (g + 18 != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g + 19 != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }
            g++;
            if (g + 20 != index && g / 9 != index / 9 && g % 9 != index % 9)
            { relatives[count++] = g; }

            return relatives;
        }
        /// <summary>
        /// 生成数独
        /// </summary>
        public int[] GenerateSudoku()
        {
            Reset();
            do
            {
                while (normalIndex < 81) //顺序填数
                {
                    if (sudokuArray[normalIndex] != 0) //如果已经有数，则跳过
                    {
                        normalIndex++;
                        continue;
                    }
                    if (FillCell(normalIndex)) //填充成功，继续填充下一个
                    {
                        normalIndex++;
                    }
                    else //填充失败，回滚
                    {
                        normalIndex = Rollback(recordStack.Pop());
                    }
                }
                //检查循环1次之后是否有没有填入的格子
                normalIndex = CheckSudoku();
                if (normalIndex == -1)
                    return sudokuArray;
            }
            while (true);
        }
        /// <summary>
        /// 获取单元格的候选数列表
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>候选数列表</returns>
        public int[] GetCandidate(int index)
        {
            List<int> list = new List<int>();
            int c = candidateArray[index];
            for (int i = 8; i >= 0; i--)
            {
                if ((c & (1 << i)) == 0)
                    list.Add(9 - i);
            }
            int[] result = new int[list.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = list[i];
            }
            return result;
        }
        /// <summary>
        /// 填充单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <param name="value">填充值</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        public int[] FillCell(int index, int value)
        {
            if (sudokuArray[index] != 0)
            { //如果数独此单元格已经填有数字，则返回
                return null;
            }
            //当前单元格候选数移除填入的数
            if (value == 0)
            { return null; }
            //当前单元格候选数移除填入的数
            candidateArray[index] = candidateArray[index] | (1 << (9 - value));
            //获取关联单元格的数组值
            List<int> relatives = GetRelativeCells(index).ToList();
            sudokuArray[index] = value;
            int temp;
            List<int> list = new List<int>();
            relatives.ForEach(i =>
            {
                if (sudokuArray[index] != 0)
                {
                    temp = candidateArray[i];
                    candidateArray[i] |= (1 << (9 - sudokuArray[index]));
                    if (temp != candidateArray[i])
                    {
                        list.Add(i);
                    }
                }
            });
            int[] result = new int[list.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = list[i];
            }
            return result;
        }
        /// <summary>
        /// 清空单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        public int[] ClearCell(int index)
        {
            if (sudokuArray[index] == 0)
            { //如果数独此单元格没有填数字，则返回
                return null;
            }
            int v = sudokuArray[index];
            sudokuArray[index] = 0;
            List<int> list = GetRelativeCells(index).ToList();
            List<int> removes = new List<int>();
            list.ForEach(l =>
            {
                int[] cans = GetRelativeCells(l);
                for (int i = 0; i < cans.Length; i++)
                {
                    if (sudokuArray[cans[i]] == v)
                    {
                        removes.Add(l);
                        break;
                    }
                }
            });
            int[] result = new int[list.Count - removes.Count + 1];
            int count = 1;
            result[0] = index;
            for (int i = 0; i < list.Count; i++)
            {
                if (!removes.Contains(list[i]))
                {
                    result[count++] = list[i];
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                candidateArray[result[i]] &= ~(1 << (9 - v));
            }
            return result;
        }
        /// <summary>
        /// 填充数独
        /// </summary>
        /// <param name="values">81个单元格的值</param>
        public void FillSudoku(int[] values)
        {
            Reset();
            if (values.Length != 81)
            {
                throw new InvalidOperationException("填充数独的值不是81个数！");
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != 0)
                {
                    FillCell(i, values[i]);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 数独类型
        /// </summary>
        public SudokuType Type { get; set; }

        #endregion
    }
}
