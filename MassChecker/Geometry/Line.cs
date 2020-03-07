using System;

namespace MassChecker.Geometry
{
    internal class Line
    {
        #region Constructor

        internal Line(Point a, Point b)
        {
            A = a;
            B = b;
            AX = a.X;
            AY = a.Y;
            BX = b.X;
            BY = b.Y;
        }

        internal Line(int ax, int ay, int bx, int by)
        {
            AX = ax;
            AY = ay;
            BX = bx;
            BY = by;
        }

        #endregion

        #region Properties

        internal int AX { get; private set; }
        internal int AY { get; private set; }
        internal int BX { get; private set; }
        internal int BY { get; private set; }

        internal Point a;
        internal Point A
        {
            get
            {
                if (a == null) a = new Point(AX, AY);
                return a;
            }
            set
            {
                a = value;
            }
        }

        private Point b;
        internal Point B
        {
            get
            {
                if (b == null) b = new Point(BX, BY);
                return b;
            }
            set
            {
                b = value;
            }
        }

        #endregion

        #region Methods

        internal int GetPointX(double yOffSet)
        {
            return A.X + (int)((B.X - A.X) * yOffSet);
        }

        internal int GetPointY(double xOffSet)
        {
            return A.Y + (int)((B.Y - A.Y) * xOffSet);
        }

        internal Point GetPoint(double offset)
        {
            return new Point(GetPointX(offset), GetPointX(offset));
        }

        #endregion
    }
}