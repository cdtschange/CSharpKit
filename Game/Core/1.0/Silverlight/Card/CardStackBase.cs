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
using CdtsGame.Core.Card;
using System.Collections.Generic;

namespace CdtsGame.Core.Silverlight.Card
{
    /// <summary>
    /// 牌堆基类
    /// </summary>
    public class CardStackBase : UserControl, ICardStackBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pl">牌列表</param>
        /// <param name="dir">初始化牌堆插牌方向</param>
        public CardStackBase(IEnumerable<ICard> pl, CardStackDir dir)
        {
            this.cardList = new List<ICard>();
            this.mainGrid = new Grid() { Tag = 0.0 };
            this.Content = this.mainGrid;

            if (pl != null)
            {
                List<ICard> tmpList = new List<ICard>(pl);
                tmpList.ForEach(c => PushCard(c, dir));
            }
        }
        /// <summary>
        /// 构造函数（默认下方插入初始化牌）
        /// </summary>
        /// <param name="pl">牌列表</param>
        public CardStackBase(IEnumerable<ICard> pl)
            : this(pl, CardStackDir.Bottom)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public CardStackBase()
            : this(null)
        {
        }

        #endregion

        #region Properties

        protected List<ICard> cardList;
        /// <summary>
        /// 扑克列表
        /// </summary>
        public List<ICard> CardList
        {
            get { return this.cardList; }
        }

        private Grid mainGrid;
        /// <summary>
        /// 主画布
        /// </summary>
        public Grid MainGrid
        {
            get { return this.mainGrid; }
            set { this.mainGrid = value; }
        }

        /// <summary>
        /// 第一张牌
        /// </summary>
        public ICard TopCard
        {
            get
            {
                if (this.cardList.Count > 0)
                    return this.cardList[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// 最底下的一张牌
        /// </summary>
        public ICard BottomCard
        {
            get
            {
                if (this.cardList.Count > 0)
                    return this.cardList[this.cardList.Count - 1];
                else
                    return null;
            }
        }

        /// <summary>
        /// 牌堆里牌的数量
        /// </summary>
        public int CardCount
        {
            get
            {
                return this.cardList.Count;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 插入一张牌
        /// </summary>
        /// <param name="p">插入的牌</param>
        /// <param name="dir">插入方向</param>
        public virtual void PushCard(ICard p, CardStackDir dir)
        {
            switch (dir)
            {
                case CardStackDir.Top:
                    this.cardList.Insert(0, p);
                    mainGrid.Children.Add((Card)p);
                    break;
                case CardStackDir.Bottom:
                    this.cardList.Add(p);
                    mainGrid.Children.Add((Card)p);
                    break;
                case CardStackDir.Random:
                    Random r = new Random(DateTime.Now.Millisecond);
                    int s = r.Next(0, this.cardList.Count);
                    this.cardList.Insert(s, p);
                    mainGrid.Children.Add((Card)p);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 向牌堆中加入一组牌（有完成事件）
        /// </summary>
        /// <param name="pl">加入的一组牌</param>
        /// <param name="dir">加入方向</param>
        public virtual void AddCard(IEnumerable<ICard> pl, CardStackDir dir)
        {
            foreach (ICard p in pl)
                PushCard(p, dir);

            OnAddCardCompleted();
        }
        /// <summary>
        /// 向牌堆中加入一张牌（有完成事件）
        /// </summary>
        /// <param name="p">加入的一张牌</param>
        /// <param name="dir">加入方向</param>
        public virtual void AddCard(ICard p, CardStackDir dir)
        {
            AddCard(new ICard[] { p }, dir);
        }

        /// <summary>
        /// 抽出一张牌
        /// </summary>
        /// <param name="dir">抽出方向</param>
        public virtual ICard PopCard(CardStackDir dir)
        {
            Card p = null;
            if (cardList.Count > 0)
            {
                switch (dir)
                {
                    case CardStackDir.Top:
                        p = (Card)cardList[0];
                        cardList.Remove(p);
                        mainGrid.Children.Remove(p);
                        p.IsSelected = false;
                        break;
                    case CardStackDir.Bottom:
                        p = (Card)cardList[cardList.Count - 1];
                        cardList.Remove(p);
                        mainGrid.Children.Remove(p);
                        p.IsSelected = false;
                        break;
                    case CardStackDir.Random:
                        Random r = new Random(DateTime.Now.Millisecond);
                        int s = r.Next(0, this.cardList.Count - 1);
                        p = (Card)cardList[s];
                        cardList.Remove(p);
                        mainGrid.Children.Remove(p);
                        p.IsSelected = false;
                        break;
                    default:
                        break;
                }
            }
            return p;
        }

        /// <summary>
        /// 从牌堆里的指定牌开始分离出一组牌（有完成事件）
        /// </summary>
        /// <param name="p">指定牌</param>
        /// <returns>分离出的牌</returns>
        /// <param name="dir">分离方向</param>
        /// <param name="includeSplit">是否在分离出的牌中包含指定牌</param>
        public virtual List<ICard> SplitCard(ICard p, CardStackDir dir, bool includeSplit)
        {
            List<ICard> pl = new List<ICard>();
            if (this.cardList.Contains(p))
            {
                ICard tmpCard = null;
                switch (dir)
                {
                    case CardStackDir.Top:
                        tmpCard = this.TopCard;
                        if (includeSplit)
                        {
                            do
                            {
                                tmpCard = this.PopCard(CardStackDir.Top);
                                pl.Add(tmpCard);
                            }
                            while (tmpCard != p);
                        }
                        else
                        {
                            while (tmpCard != p)
                            {
                                tmpCard = this.PopCard(CardStackDir.Top);
                                pl.Add(tmpCard);
                            }
                        }
                        break;
                    case CardStackDir.Bottom:
                        tmpCard = this.BottomCard;
                        if (includeSplit)
                        {
                            do
                            {
                                tmpCard = this.PopCard(CardStackDir.Bottom);
                                pl.Insert(0, tmpCard);
                            }
                            while (tmpCard != p);
                        }
                        else
                        {
                            while (tmpCard != p)
                            {
                                tmpCard = this.PopCard(CardStackDir.Bottom);
                                pl.Insert(0, tmpCard);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            OnSplitCardCompleted();
            return pl;
        }

        /// <summary>
        /// 将牌堆的所有牌释放，并以列表形式返回
        /// </summary>
        /// <returns>所有牌</returns>
        public virtual List<ICard> ReleaseToList()
        {
            List<ICard> pl = this.SplitCard(this.TopCard, CardStackDir.Bottom, true);
            return pl;
        }

        /// <summary>
        /// 加入牌成功
        /// </summary>
        protected virtual void OnAddCardCompleted()
        {
            if (AddCardCompleted != null)
                AddCardCompleted(this, null);
        }
        /// <summary>
        /// 分离牌成功
        /// </summary>
        protected virtual void OnSplitCardCompleted()
        {
            if (SplitCardCompleted != null)
                SplitCardCompleted(this, null);
        }

        public event EventHandler AddCardCompleted;
        public event EventHandler SplitCardCompleted;

        #endregion
    }
}
