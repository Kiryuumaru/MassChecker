using System;

namespace MassChecker.Geometry
{
    internal class Rect
    {
        #region Properties

        internal int Left { get; private set; }
        internal int Top { get; private set; }
        internal int Right { get; private set; }
        internal int Bottom { get; private set; }

        internal int X { get { return Left; } }
        internal int Y { get { return Top; } }
        internal int Width { get { return Right - Left; } }
        internal int Height { get { return Bottom - Top; } }

        #endregion

        #region Constructor

        internal Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        #endregion

        #region Methods

        internal bool Contains(Point point)
        {
            return ((Left <= point.X) && (point.X <= Right) && (Top <= point.Y) && (point.Y <= Bottom));
        }

        #endregion
    }
}