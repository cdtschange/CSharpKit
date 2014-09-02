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
using CdtsGame.Core.Card;

namespace CdtsGame.Core.Silverlight.Card
{
    /// <summary>
    /// 扑克牌
    /// </summary>
    public partial class Card : UserControl, ICard
    {
        #region Constructors
        public Card()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pv">牌面值</param>
        public Card(CardValue pv)
        {
            InitializeComponent();

            this.value = pv;
            this.Front.Source = CardImageLib.GetCardBitmap(pv);
            this.Back.Source = CardImageLib.GetCardBitmap(CardValue.Null);
        }

        #endregion

        #region Properties

        private CardValue value;
        /// <summary>
        /// 牌面值
        /// </summary>
        public CardValue Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// 是否是背面
        /// </summary>
        private Boolean isBack = true;

        public Boolean IsBack
        {
            get { return isBack; }
            set
            {
                isBack = value;
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public Boolean IsSelected { get; set; }

        /// <summary>
        /// 前景图案
        /// </summary>
        public Image ForeImage
        {
            get { return this.Front; }
            set { this.Front = value; }
        }

        /// <summary>
        /// 背景图案
        /// </summary>
        public Image BackImage
        {
            get { return this.Back; }
            set { this.Back = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 翻面
        /// </summary>
        public void TurnOver()
        {
            if (IsBack)
            {
                BackToFront.Storyboard.Begin();
            }
            else
            {
                FrontToBack.Storyboard.Begin();
            }
        }

        #endregion
    }
}
