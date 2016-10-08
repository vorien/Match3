using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3
{
    public static class ScreenInfo
    {
        public const int preferredWidth = 768;
        public const int preferredHeight = 1024;
        public static float Width = 768;
        public static float Height = 1024;
        public static float fontLarge = 50;
        public static float fontMedium = 30;
        public static float fontSmall = 22;
        public static float Scale = 1;

        public static void setFontSizes()
        {
            float ratio = Height / preferredHeight;
            fontLarge = 60 * ratio;
            fontMedium = 50 * ratio;
            fontSmall = 30 * ratio;
            Scale = ratio;
        }
    }
}
