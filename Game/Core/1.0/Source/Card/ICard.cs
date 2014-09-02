using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if SILVERLIGHT
using System.Windows.Controls;
#endif

namespace CdtsGame.Core.Card
{
    /// <summary>
    /// 扑克牌
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// 牌面值
        /// </summary>
        CardValue Value { get; }
        /// <summary>
        /// 是否是背面
        /// </summary>
        Boolean IsBack { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        Boolean IsSelected { get; set; }

#if SILVERLIGHT
        /// <summary>
        /// 前景图案
        /// </summary>
        Image ForeImage { get; set; }
        /// <summary>
        /// 背景图案
        /// </summary>
        Image BackImage { get; set; }
#endif

        /// <summary>
        /// 翻面
        /// </summary>
        void TurnOver();
    }

    /// <summary>
    /// 牌面值
    /// </summary>
    /// <remarks>
    /// H(heart) 红桃
    /// S(spade) 黑桃
    /// D(Diamond) 方块
    /// C(club) 梅花
    /// JokerBlack 小王
    /// JokerRed 大王
    /// Null 表示背面
    /// </remarks>
    public enum CardValue : int
    {
        Null = 0,
        H1 = 101, H2, H3, H4, H5, H6, H7, H8, H9, H10, HJ, HQ, HK,
        S1 = 201, S2, S3, S4, S5, S6, S7, S8, S9, S10, SJ, SQ, SK,
        D1 = 301, D2, D3, D4, D5, D6, D7, D8, D9, D10, DJ, DQ, DK,
        C1 = 401, C2, C3, C4, C5, C6, C7, C8, C9, C10, CJ, CQ, CK,
        JokerBlack = 1000,
        JokerRed = 2000
    }
}
