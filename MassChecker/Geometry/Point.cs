using System;

namespace MassChecker.Geometry
{
    internal class Point
    {
        #region Constructor

        internal Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        internal int X { get; private set; }
        internal int Y { get; private set; }

        #endregion
    }
}