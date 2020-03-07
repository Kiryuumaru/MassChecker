using System.Collections.Generic;
using System.Linq;
using MassChecker.Geometry;
using MassChecker.Models;

namespace MassChecker.Anchors
{
    internal class ShadeItemKey
    {
        #region Properties

        public Shade Shade { get; private set; }
        public int ItemNumber { get; private set; }
        public Key Key { get; private set; }
        public string KeyChar
        {
            get
            {
                switch (Key)
                {
                    case Key.A:
                        return "A";
                    case Key.B:
                        return "B";
                    case Key.C:
                        return "C";
                    case Key.D:
                        return "D";
                    case Key.E:
                        return "E";
                    default:
                        return "X";
                }
            }
        }

        #endregion

        #region Constructor

        public ShadeItemKey(Shade shade, int itemNumber, Key key)
        {
            Shade = shade;
            ItemNumber = itemNumber;
            Key = key;
        }

        #endregion
    }

    internal class ItemParser
    {
        #region Constructor

        internal ItemParser(double leftOffset, double topOffset, double rightOffset, double bottomOffset, int itemStart, int itemCount)
        {
            LeftOffset = leftOffset;
            TopOffset = topOffset;
            RightOffset = rightOffset;
            BottomOffset = bottomOffset;
            ItemStart = itemStart;
            ItemCount = itemCount;
            ItemRegion = new Arbitary3DRect[itemCount];
            ItemKeys = new List<ShadeItemKey>();
            borders = new List<System.Drawing.Point>();
        }

        #endregion

        #region Properties

        internal int ItemStart { get; private set; }
        internal int ItemCount { get; private set; }
        internal double LeftOffset { get; set; }
        internal double TopOffset { get; set; }
        internal double RightOffset { get; set; }
        internal double BottomOffset { get; set; }
        internal List<ShadeItemKey> ItemKeys { get; private set; }

        internal Arbitary3DRect MainRect;
        internal Arbitary3DRect ARegion;
        internal Arbitary3DRect BRegion;
        internal Arbitary3DRect CRegion;
        internal Arbitary3DRect DRegion;
        internal Arbitary3DRect ERegion;
        internal Arbitary3DRect[] ItemRegion;

        #endregion

        #region Methods

        private readonly List<System.Drawing.Point> borders;
        internal System.Drawing.Point[] GetBorders()
        {
            borders.Clear();
            borders.AddRange(ARegion.GetBorders());
            borders.AddRange(BRegion.GetBorders());
            borders.AddRange(CRegion.GetBorders());
            borders.AddRange(DRegion.GetBorders());
            borders.AddRange(ERegion.GetBorders());
            foreach (Arbitary3DRect l in ItemRegion.ToList().Reverse<Arbitary3DRect>())
            {
                borders.AddRange(l.GetBorders());
            }
            return borders.ToArray();
        }

        private int num;
        private Key key;
        internal void Process(PaperParser paperParser)
        {
            MainRect = paperParser.BodyRect.GetInnerRect(LeftOffset, TopOffset, RightOffset, BottomOffset);

            ARegion = MainRect.GetInnerRect(0, 0, 1.0 / 5.0, 1);
            BRegion = MainRect.GetInnerRect(1.0 / 5.0, 0, 2.0 / 5.0, 1);
            CRegion = MainRect.GetInnerRect(2.0 / 5.0, 0, 3.0 / 5.0, 1);
            DRegion = MainRect.GetInnerRect(3.0 / 5.0, 0, 4.0 / 5.0, 1);
            ERegion = MainRect.GetInnerRect(4.0 / 5.0, 0, 1, 1);

            for (int i = 0; i < ItemCount; i++)
            {
                ItemRegion[i] = MainRect.GetInnerRect(0, (double)i / ItemCount, 1, (double)(i + 1) / ItemCount);
            }

            num = 0;
            ItemKeys.Clear();
            foreach (Shade s in paperParser.BodyShades)
            {
                num = 0;
                for (int i = 0; i < ItemCount; i++)
                {
                    if (ItemRegion[i].IsInside(s.Center)) num = i + ItemStart;
                }

                if (num == 0) continue;

                if (ARegion.IsInside(s.Center)) key = Key.A;
                else if (BRegion.IsInside(s.Center)) key = Key.B;
                else if (CRegion.IsInside(s.Center)) key = Key.C;
                else if (DRegion.IsInside(s.Center)) key = Key.D;
                else if (ERegion.IsInside(s.Center)) key = Key.E;
                else continue;

                ItemKeys.Add(new ShadeItemKey(s, num, key));
            }
        }

        #endregion
    }
}