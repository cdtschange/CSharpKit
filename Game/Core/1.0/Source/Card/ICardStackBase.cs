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
    /// 牌堆方向
    /// </summary>
    public enum CardStackDir
    {
        Top, Bottom, Random
    }

    public interface ICardStackBase
    {
        #region Properties

        /// <summary>
        /// 扑克列表
        /// </summary>
        List<ICard> CardList { get; }
#if SILVERLIGHT
        /// <summary>
        /// 主画布
        /// </summary>
        Grid MainGrid { get; set; }
#endif
        /// <summary>
        /// 第一张牌
        /// </summary>
        ICard TopCard { get; }
        /// <summary>
        /// 最底下的一张牌
        /// </summary>
        ICard BottomCard { get; }
        /// <summary>
        /// 牌堆里牌的数量
        /// </summary>
        int CardCount { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 插入一张牌
        /// </summary>
        /// <param name="p">插入的牌</param>
        /// <param name="dir">插入方向</param>
        void PushCard(ICard p, CardStackDir dir);
        /// <summary>
        /// 向牌堆中插入一组牌（有完成事件）
        /// </summary>
        /// <param name="pl">加入的一组牌</param>
        /// <param name="dir">加入方向</param>
        void AddCard(IEnumerable<ICard> pl, CardStackDir dir);
        /// <summary>
        /// 向牌堆中插入一张牌（有完成事件）
        /// </summary>
        /// <param name="p">加入的一张牌</param>
        /// <param name="dir">加入方向</param>
        void AddCard(ICard p, CardStackDir dir);
        /// <summary>
        /// 抽出一张牌
        /// </summary>
        /// <param name="dir">抽出方向</param>
        ICard PopCard(CardStackDir dir);
        /// <summary>
        /// 从牌堆里的指定牌开始分离出一组牌（有完成事件）
        /// </summary>
        /// <param name="p">指定牌</param>
        /// <returns>分离出的牌</returns>
        /// <param name="dir">分离方向</param>
        /// <param name="includeP">是否在分离出的牌中包含指定牌</param>
        List<ICard> SplitCard(ICard p, CardStackDir dir, bool includeSplit);
        /// <summary>
        /// 将牌堆的所有牌释放，并以列表形式返回
        /// </summary>
        /// <returns>所有牌</returns>
        List<ICard> ReleaseToList();

        #endregion
    }
}
