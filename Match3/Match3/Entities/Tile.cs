using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3;

namespace Match3.Entities
{
    public class Tile : CCSprite
    {
        public Tile()
        {
            Texture = new CCTexture2D("tile");
            float tileDimensions = (ScreenInfo.preferredWidth - (Configuration.gridWidthSpacing * 2)) / Configuration.gridColumns;
            ContentSize = new CCSize(tileDimensions, tileDimensions);
            AnchorPoint = CCPoint.AnchorMiddle;

            //  Adding a debug label that will display the material's row and column numbers, to see if the material visually match with it's array locations
            CCLabel debugLabel = new CCLabel("tile", "Arial", 22, CCLabelFormat.SystemFont);
            //debugLabel.Text = "[" + row + ", " + column + "]";
            debugLabel.Color = CCColor3B.White;
            debugLabel.PositionX = ContentSize.Width / 2.0f;
            debugLabel.PositionY = ContentSize.Height / 2.0f;
            debugLabel.AnchorPoint = CCPoint.AnchorMiddle;

            AddChild(debugLabel);

        }
    }
}
