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
using CdtsGame.Core.Silverlight.Card.Solitaire;
using CdtsGame.Core.Card;

namespace Solitaire
{
    public partial class SolitaireSourceStack : SolitaireStackBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cl">牌列表</param>
        /// <param name="gm">游戏模式</param>
        public SolitaireSourceStack(IEnumerable<ICard> cl, GameModeType gm)
            : base(cl, new Point(10, 10))
        {
            InitializeComponent();
            this.GameMode = gm;
            this.MinHeight = 123;
            this.MinWidth = 80;
            this.Content = MainGrid;
        }

        public SolitaireSourceStack()
            : this(null, GameModeType.ThreeCard)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 游戏模式
        /// </summary>
        public GameModeType GameMode { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// 改变游戏模式
        /// </summary>
        /// <param name="gm">游戏模式</param>
        public void ChangeGameMode(GameModeType gm)
        {
            this.GameMode = gm;
        }

        /// <summary>
        /// 从牌堆中取牌
        /// </summary>
        /// <returns>取出的牌</returns>
        public List<ICard> SplitSourceCard()
        {
            List<ICard> cl = null;

            switch (GameMode)
            {
                case GameModeType.OneCard:
                    cl = this.SplitCardFromBottom(this.BottomCard);
                    break;
                case GameModeType.ThreeCard:
                    if (this.CardCount >= 3)
                        cl = this.SplitCardFromBottom(this.cardList[this.CardCount - 3]);
                    else if (this.CardCount == 2)
                        cl = this.SplitCardFromBottom(this.TopCard);
                    else if (this.CardCount == 1)
                        cl = this.SplitCardFromBottom(this.BottomCard);
                    break;
                default:
                    break;
            }

            return cl;
        }

        public override void PushCard(ICard p, CardStackDir dir)
        {
            base.PushCard(p, dir);
            if (sumFix.X > CardPadding.X * 2)
            {
                sumFix = new Point(0, 0);
            }
        }
        public override ICard PopCard(CardStackDir dir)
        {
            var c = base.PopCard(dir);
            if (sumFix.X < 0)
            {
                sumFix = new Point(CardPadding.X * 2, CardPadding.Y * 2);
            }
            return c;
        }

        /// <summary>
        /// 重新填装牌堆
        /// </summary>
        /// <param name="cl">牌列表</param>
        public void RefillCardList(IEnumerable<ICard> cl)
        {
            if (this.CardCount != 0)
                throw new Exception("牌堆不为空，不能重新填装");
            this.AddCardInBottom(cl);
        }

        protected override void OnUnderMouseCardChange(ICard oldCard, ICard newCard)
        {
            if (oldCard != null && oldCard == this.BottomCard)
            {
                oldCard.IsSelected = false;
            }
            if (newCard != null && newCard == this.BottomCard)
            {
                newCard.IsSelected = true;
            }
        }

        #endregion

    }
}
