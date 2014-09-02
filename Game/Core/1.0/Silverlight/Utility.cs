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
using System.Linq;
using System.Reflection;

namespace CdtsGame.Core.Silverlight
{
    public static class Utility
    {
        public static Point Add(this Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point Sub(this Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Color ToColor(this string colorName)
        {
            if (colorName.StartsWith("#"))
                colorName = colorName.Replace("#", string.Empty);
            int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
            return new Color()
            {
                A = Convert.ToByte((v >> 24) & 255),
                R = Convert.ToByte((v >> 16) & 255),
                G = Convert.ToByte((v >> 8) & 255),
                B = Convert.ToByte((v >> 0) & 255)
            };
        }
        /// <summary>
        /// 获取枚举类型的所有枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举值列表</returns>
        public static object[] GetValues(Type enumType)
        {
            if (enumType.IsEnum == false)
            {
                throw new ArgumentException("Type " + enumType.Name + " is not an enum!");
            }

            List<Object> values = new List<object>();

            var fields = from n in enumType.GetFields() where n.IsLiteral select n;

            foreach (FieldInfo fi in fields)
                values.Add(fi.GetValue(enumType));

            return values.ToArray();

        }
    }

    public static class StoryUtility
    {
        public static Timeline DoubleAnimation(double begin, double duration, double from, double to, DependencyObject target, string prop)
        {
            DoubleAnimation an = new DoubleAnimation();
            an.BeginTime = TimeSpan.FromMilliseconds(begin);
            an.Duration = TimeSpan.FromMilliseconds(duration);
            an.From = from;
            an.To = to;
            Storyboard.SetTarget(an, target);
            Storyboard.SetTargetProperty(an, new PropertyPath(prop));
            return an;
        }
    }
}
