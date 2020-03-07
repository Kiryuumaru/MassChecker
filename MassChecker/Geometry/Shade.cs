using System;

namespace MassChecker.Geometry
{
    internal class Shade
    {
        #region Constructor

        internal Shade(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BoundingRect = new Rect(Left, Top, Right, Bottom);
            Tl = new Point(Left, Top);
            Tr = new Point(Right, Top);
            Bl = new Point(Left, Bottom);
            Br = new Point(Right, Bottom);
            Center = new Point(BoundingRect.X + (BoundingRect.Width / 2), BoundingRect.Y + (BoundingRect.Height / 2));
        }

        #endregion

        #region Properties

        internal int X { get; private set; }
        internal int Y { get; private set; }
        internal int Width { get; private set; }
        internal int Height { get; private set; }

        internal int Left { get { return X; } }
        internal int Top { get { return Y; } }
        internal int Right { get { return X + Width; } }
        internal int Bottom { get { return Y + Height; } }

        internal Rect BoundingRect { get; set; }
        internal Point Tl { get; set; }
        internal Point Tr { get; set; }
        internal Point Bl { get; }
        internal Point Br { get; set; }
        internal Point Center { get; set; }

        #endregion
    }
}