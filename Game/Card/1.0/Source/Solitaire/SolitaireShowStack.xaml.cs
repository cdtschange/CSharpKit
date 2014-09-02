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
using CdtsGame.Core.Silverlight.Card;

namespace Solitaire
{
    public partial class SolitaireShowStack : SolitaireStackBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cl">牌列表</param>
        /// <param name="gm">游戏模式</param>
        public SolitaireShowStack(IEnumerable<ICard> cl, GameModeType gm)
            : base(cl, new Point(15, 0))
        {
            InitializeComponent();
            this.GameMode = gm;
            this.Content = MainGrid;
        }

        public SolitaireShowStack()
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
            RefreshShow();
        }

        /// <summary>
        /// 根据游戏模式刷新显示
        /// </summary>
        private void RefreshShow()
        {
            if (this.GameMode == GameModeType.OneCard)
            {
                foreach (ICard c in this.cardList)
                {
                    Card cc = c as Card;
                    cc.Margin = new Thickness();
                }
            }
            else if (GameMode == GameModeType.ThreeCard)
            {
                for (int i = 0; i < this.cardList.Count; i++)
                {
                    Card cc = this.cardList[i] as Card;
                    cc.Margin = new Thickness(CardPadding.X * i, 0, 0, 0);
                }              
            }
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// 添加牌后刷新显示
        /// </summary>
        protected override void OnAddCardCompleted()
        {
            RefreshShow();
            base.OnAddCardCompleted();
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
