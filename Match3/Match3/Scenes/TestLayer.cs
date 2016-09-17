using CocosSharp;
using Match3.Information;

namespace Match3.Scenes
{
    class TestLayer : CCLayer
    {
        public CCLabel testLabel;
        public TestLayer()
        {
            //  Label to display the targetScore the user has to meet to beat the level
            testLabel = new CCLabel("Test String Label", "Arial", 50, CCLabelFormat.SystemFont);
            testLabel.Color = CCColor3B.White;
            testLabel.AnchorPoint = CCPoint.AnchorMiddle;
            testLabel.Position = new CCPoint(768/2, 1024/2);
            AddChild(testLabel);
        }

    }
}

