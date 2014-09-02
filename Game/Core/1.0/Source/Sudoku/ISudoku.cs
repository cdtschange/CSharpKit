using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdtsGame.Core.Sudoku
{
    /// <summary>
    /// 数独接口
    /// </summary>
    public interface ISudoku
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Reset();
        /// <summary>
        /// 获取关联单元格索引
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回关联单元格索引</returns>
        int[] GetRelativeCells(int index);
        /// <summary>
        /// 生成数独
        /// </summary>
        int[] GenerateSudoku();
        /// <summary>
        /// 获取单元格的候选数列表
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>候选数列表</returns>
        int[] GetCandidate(int index);
        /// <summary>
        /// 填充单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <param name="value">填充值</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        int[] FillCell(int index, int value);
        /// <summary>
        /// 清空单元格
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns>需要更新候选数的单元格索引列表</returns>
        int[] ClearCell(int index);
        /// <summary>
        /// 填充数独
        /// </summary>
        /// <param name="values">81个单元格的值</param>
        void FillSudoku(int[] values);
        /// <summary>
        /// 数独类型
        /// </summary>
        SudokuType Type { get; set; }

    }
}
