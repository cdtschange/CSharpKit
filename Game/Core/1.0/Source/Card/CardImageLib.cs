using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
#if SILVERLIGHT
using System.Windows.Media.Imaging;
using CdtsGame.Core.Silverlight;
#endif

namespace CdtsGame.Core.Card
{

    /// <summary>
    /// 扑克牌图案生成器
    /// </summary>
    public static class CardImageLib
    {
#if SILVERLIGHT

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        static CardImageLib()
        {
            imageDic = new Dictionary<CardValue, BitmapImage>();

            Object[] values = Utility.GetValues(typeof(CardValue));

            foreach (Object value in values)
            {
                CardValue cv = (CardValue)value;
                Assembly ass = Assembly.GetExecutingAssembly();
                String imagePath = "/Solitaire;component/Images/" + cv.ToString() + ".png";
                BitmapImage bi = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                imageDic.Add(cv, bi);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 图案列表
        /// </summary>
        private static Dictionary<CardValue, BitmapImage> imageDic;

        #endregion

        #region Methods

        /// <summary>
        /// 获取扑克图案
        /// </summary>
        /// <param name="value">牌面值</param>
        /// <returns>返回扑克图案</returns>
        public static BitmapImage GetCardBitmap(CardValue value)
        {
            return imageDic[value];
        }

        #endregion
#endif
    }
}
