using System.Collections.Generic;
using MassChecker.Geometry;
using MassChecker.Models;

namespace MassChecker.Anchors
{
    internal enum SetParserResult
    {
        SetA, SetB, SetC, Unshaded, Multishaded
    }

    internal class SetParser
    {
        #region Properties

        internal double LeftOffset;
        internal double TopOffset;
        internal double RightOffset;
        internal double BottomOffset;
        internal Shade ShadeSet;
        internal SetParserResult SetParserResult;
        internal AssessmentSet AssessmentSetResult;

        internal Arbitary3DRect MainRect;
        internal Arbitary3DRect SetARegion;
        internal Arbitary3DRect SetBRegion;
        internal Arbitary3DRect SetCRegion;

        #endregion

        #region Constructors

        internal SetParser(
            double leftOffset,
            double topOffset,
            double rightOffset,
            double bottomOffset)
        {
            LeftOffset = leftOffset;
            TopOffset = topOffset;
            RightOffset = rightOffset;
            BottomOffset = bottomOffset;
            borders = new List<System.Drawing.Point>();
        }

        #endregion

        #region Methods

        private readonly List<System.Drawing.Point> borders;
        public System.Drawing.Point[] GetBorders()
        {
            borders.Clear();
            borders.AddRange(SetARegion.GetBorders());
            borders.AddRange(SetBRegion.GetBorders());
            borders.AddRange(SetCRegion.GetBorders());
            return borders.ToArray();
        }

        private bool isSetAShaded;
        private bool isSetBShaded;
        private bool isSetCShaded;
        private int shadedSetCount;
        public void Process(PaperParser paperParser)
        {
            MainRect = paperParser.HeaderRect.GetInnerRect(LeftOffset, TopOffset, RightOffset, BottomOffset);

            SetARegion = MainRect.GetInnerRect(0, 0, 1.0 / 3.0, 1);
            SetBRegion = MainRect.GetInnerRect(1.0 / 3.0, 0, 2.0 / 3.0, 1);
            SetCRegion = MainRect.GetInnerRect(2.0 / 3.0, 0, 1, 1);

            isSetAShaded = false;
            isSetBShaded = false;
            isSetCShaded = false;
            ShadeSet = null;
            shadedSetCount = 0;
            foreach (Shade s in paperParser.HeaderShades)
            {
                if (SetARegion.IsInside(s.Center))
                {
                    isSetAShaded = true;
                    ShadeSet = s;
                }
                else if (SetBRegion.IsInside(s.Center))
                {
                    isSetBShaded = true;
                    ShadeSet = s;
                }
                else if (SetCRegion.IsInside(s.Center))
                {
                    isSetCShaded = true;
                    ShadeSet = s;
                }
            }

            shadedSetCount += isSetAShaded ? 1 : 0;
            shadedSetCount += isSetBShaded ? 1 : 0;
            shadedSetCount += isSetCShaded ? 1 : 0;

            if (shadedSetCount == 0)
            {
                SetParserResult = SetParserResult.Unshaded;
            }
            else if (shadedSetCount > 1)
            {
                SetParserResult = SetParserResult.Multishaded;
            }
            else if (isSetAShaded)
            {
                SetParserResult = SetParserResult.SetA;
                AssessmentSetResult = AssessmentSet.SetA;
            }
            else if (isSetBShaded)
            {
                SetParserResult = SetParserResult.SetB;
                AssessmentSetResult = AssessmentSet.SetB;
            }
            else if (isSetCShaded)
            {
                SetParserResult = SetParserResult.SetC;
                AssessmentSetResult = AssessmentSet.SetC;
            }
        }

        #endregion
    }
}