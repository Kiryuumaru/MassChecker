using System;

namespace MassChecker.Geometry
{
    internal class Arbitary3DRect
    {
        #region Constructors

        internal Arbitary3DRect(Point tl, Point tr, Point bl, Point br)
        {
            TL = tl;
            TR = tr;
            BL = bl;
            BR = br;
            PerspectiveMidpoint = Extension.LineIntersection(tl, br, tr, bl);
        }

        #endregion

        #region Properties

        internal Point TL;
        internal Point TR;
        internal Point BL;
        internal Point BR;
        internal Point PerspectiveMidpoint;
        private System.Drawing.Point[] borders;

        #endregion

        #region Methods

        private double GetLeftLength()
        {
            return Extension.GetDistance(TL, BL);
        }
        private double GetTopLength()
        {
            return Extension.GetDistance(TL, TR);
        }
        private double GetRightLength()
        {
            return Extension.GetDistance(TR, BR);
        }
        private double GetBottomLength()
        {
            return Extension.GetDistance(BL, BR);
        }

        internal System.Drawing.Point[] GetBorders()
        {
            borders = new System.Drawing.Point[]
            {
                new System.Drawing.Point(BL.X, BL.Y),
                new System.Drawing.Point(TL.X, TL.Y),

                new System.Drawing.Point(TL.X, TL.Y),
                new System.Drawing.Point(TR.X, TR.Y),

                new System.Drawing.Point(TR.X, TR.Y),
                new System.Drawing.Point(BR.X, BR.Y),

                new System.Drawing.Point(BR.X, BR.Y),
                new System.Drawing.Point(BL.X, BL.Y)
            };
            return borders;
        }

        internal Arbitary3DRect GetInnerRect(double leftOffset, double topOffset, double rightOffset, double bottomOffset)
        {
            Line left;
            Line top;
            Line right;
            Line bottom;

            if (leftOffset == 0) left = new Line(TL, BL);
            else if (leftOffset == 1) left = new Line(TR, BR);
            else left = new Line(GetTopPoint(leftOffset), GetBottomPoint(leftOffset));

            if (topOffset == 0) top = new Line(TL, TR);
            else if (topOffset == 1) top = new Line(BL, BR);
            else top = new Line(GetLeftPoint(topOffset), GetRightPoint(topOffset));

            if (rightOffset == 0) right = new Line(TL, BL);
            else if (rightOffset == 1) right = new Line(TR, BR);
            else right = new Line(GetTopPoint(rightOffset), GetBottomPoint(rightOffset));

            if (bottomOffset == 0) bottom = new Line(TL, TR);
            else if (bottomOffset == 1) bottom = new Line(BL, BR);
            else bottom = new Line(GetLeftPoint(bottomOffset), GetRightPoint(bottomOffset));

            return new Arbitary3DRect(
                Extension.LineIntersection(top, left),
                Extension.LineIntersection(top, right),
                Extension.LineIntersection(bottom, left),
                Extension.LineIntersection(bottom, right));
        }

        internal bool IsInside(Point interest)
        {
            return Extension.IsInside(interest, BL, TL, TR, BR);
        }

        //left-right and top-bottom
        internal Point GetLeftPoint(double offset)
        {
            double perspectiveOffset = Math.Pow(offset, Math.Pow(GetBottomLength() / GetTopLength(), 0.5));
            return Extension.GetPoint(TL, BL, perspectiveOffset);
        }
        internal Point GetTopPoint(double offset)
        {
            double perspectiveOffset = Math.Pow(offset, Math.Pow(GetRightLength() / GetLeftLength(), 0.5));
            return Extension.GetPoint(TL, TR, perspectiveOffset);
        }
        internal Point GetRightPoint(double offset)
        {
            double perspectiveOffset = Math.Pow(offset, Math.Pow(GetBottomLength() / GetTopLength(), 0.5));
            return Extension.GetPoint(TR, BR, perspectiveOffset);
        }
        internal Point GetBottomPoint(double offset)
        {
            double perspectiveOffset = Math.Pow(offset, Math.Pow(GetRightLength() / GetLeftLength(), 0.5));
            return Extension.GetPoint(BL, BR, perspectiveOffset);
        }

        #endregion
    }
}