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
using System.Collections.Generic;
using CdtsGame.Core.Card;
using CdtsGame.Core.Silverlight;

namespace CdtsGame.Core.Silverlight.Card.Solitaire
{
    /// <summary>
    /// 纸牌牌堆基类
    /// </summary>
    public abstract class SolitaireStackBase : CardStackBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pl">牌列表</param>
        /// <param name="p">每张牌堆叠位移</param>
        public SolitaireStackBase(IEnumerable<ICard> pl, Point p)
        {
            this.CardPadding = p;
            this.cardList = new List<ICard>();
            this.MainGrid = new Grid() { Tag = 0.0 };
            this.Content = this.MainGrid;

            if (pl != null)
            {
                List<ICard> tmpList = new List<ICard>(pl);
                foreach (ICard pk in tmpList)
                    PushCardInBottom(pk);
            }
        }

        public SolitaireStackBase(Point p)
            : this(null, p)
        {
        }

        public SolitaireStackBase(IEnumerable<ICard> cl)
            : this(cl, new Point(0, 20))
        {
        }

        public SolitaireStackBase()
            : this(null, new Point(0, 20))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 鼠标按住的牌
        /// </summary>
        protected ICard UnderMouseCard { get; set; }
        /// <summary>
        /// 牌之间的间隔
        /// </summary>
        public Point CardPadding { get; set; }

        protected Point sumFix = new Point();
        /// <summary>
        /// 新牌初始位置
        /// </summary>
        public Point SumFix
        {
            get { return this.sumFix; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 插入一张牌
        /// </summary>
        /// <param name="p">插入的牌</param>
        /// <param name="dir">插入方向</param>
        public override void PushCard(ICard p, CardStackDir dir)
        {
            Card c = p as Card;
            c.Margin = new Thickness(sumFix.X, sumFix.Y, 0, 0);
            sumFix = sumFix.Add(CardPadding);
            base.PushCard(p, dir);
        }

        /// <summary>
        /// 抽出一张牌
        /// </summary>
        /// <param name="dir">抽出方向</param>
        public override ICard PopCard(CardStackDir dir)
        {
            Card c = base.PopCard(dir) as Card;
            c.Margin = new Thickness();
            sumFix = sumFix.Sub(CardPadding);
            this.UnderMouseCard = this.UnderMouseCard == c ? null : this.UnderMouseCard;
            return c;
        }

        /// <summary>
        /// 向牌堆底部插入一张牌
        /// </summary>
        /// <param name="p">插入的牌</param>
        public void PushCardInBottom(ICard p)
        {
            this.PushCard(p, CardStackDir.Bottom);
        }

        /// <summary>
        /// 从牌堆底部抽出一张牌
        /// </summary>
        /// <returns>抽出的牌</returns>
        public ICard PopCardFromBottom()
        {
            if (cardList.Count > 0)
            {
                ICard c = this.PopCard(CardStackDir.Bottom);
                return c;
            }
            else
                return null;
        }

        /// <summary>
        /// 从牌堆里的指定牌开始从底部分离出一组牌
        /// </summary>
        /// <param name="p">指定牌</param>
        /// <returns>分离出的牌</returns>
        protected List<ICard> SplitCardFromBottom(ICard p)
        {
            return base.SplitCard(p, CardStackDir.Bottom, true);
        }

        /// <summary>
        /// 向牌堆中加入一组牌
        /// </summary>
        /// <param name="pl">加入的一组牌</param>
        protected void AddCardInBottom(IEnumerable<ICard> pl)
        {
            base.AddCard(pl, CardStackDir.Bottom);
        }

        /// <summary>
        /// 向牌堆中加入一张牌
        /// </summary>
        /// <param name="c">加入的一张牌</param>
        protected void AddCardInBottom(ICard c)
        {
            base.AddCard(c, CardStackDir.Bottom);
        }

        /// <summary>
        /// 当鼠标指向的牌改变时
        /// </summary>
        protected virtual void OnUnderMouseCardChange(ICard oldCard, ICard newCard)
        {

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            ICard pk = null;
            bool tmp = false;

            for (int i = 0; i < cardList.Count; i++)
            {
                //找出鼠标指向的最下面一张牌
                Rect r = new Rect(0, 0, (cardList[i] as Card).ActualWidth, (cardList[i] as Card).ActualHeight);
                Point p = e.GetPosition(cardList[i] as Card);
                if (r.Contains(p))
                {
                    tmp = true;
                    pk = cardList[i];
                }
                else
                {
                    if (tmp)
                        break;
                }
            }

            if (UnderMouseCard != pk)
            {
                OnUnderMouseCardChange(UnderMouseCard, pk);
                UnderMouseCard = pk;
            }
        }

        #endregion

    }

    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum GameModeType
    {
        /// <summary>
        /// 翻一张牌
        /// </summary>
        OneCard,
        /// <summary>
        /// 翻三张牌
        /// </summary>
        ThreeCard
    }
}
