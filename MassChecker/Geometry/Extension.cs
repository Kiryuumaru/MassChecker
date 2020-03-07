using System;
using System.Collections.Generic;

namespace MassChecker.Geometry
{
    internal static class Extension
    {
        internal static bool IsInside(Point interest, params Point[] polygon)
        {
            bool isInside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].Y > interest.Y) != (polygon[j].Y > interest.Y)) &&
                (interest.X < (polygon[j].X - polygon[i].X) * (interest.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }

        internal static bool IsInside(Point interest, Arbitary3DRect arbitary3DRect)
        {
            return IsInside(interest, arbitary3DRect.BL, arbitary3DRect.TL, arbitary3DRect.TR, arbitary3DRect.BR);
        }

        internal static int GetPointX(Point point1, Point point2, double yOffSet)
        {
            return point1.X + (int)((point2.X - point1.X) * yOffSet);
        }

        internal static int GetPointY(Point point1, Point point2, double xOffSet)
        {
            return point1.Y + (int)((point2.Y - point1.Y) * xOffSet);
        }

        internal static int GetPointX(Line line, double yOffSet)
        {
            return line.A.X + (int)((line.B.X - line.A.X) * yOffSet);
        }

        internal static int GetPointY(Line line, double xOffSet)
        {
            return line.A.Y + (int)((line.B.Y - line.A.Y) * xOffSet);
        }

        internal static bool IsParallel(Point A, Point B, Point C, Point D)
        {
            int a1 = B.Y - A.Y;
            int b1 = A.X - B.X;
            int a2 = D.Y - C.Y;
            int b2 = C.X - D.X;

            double determinant = a1 * b2 - a2 * b1;

            return (determinant == 0);
        }

        internal static bool IsParallel(Line ab, Line cd)
        {
            return IsParallel(ab.A, ab.B, cd.A, cd.B);
        }

        internal static Point LineIntersection(Point A, Point B, Point C, Point D)
        {
            // Line AB represented as a1x + b1y = c1  
            int a1 = B.Y - A.Y;
            int b1 = A.X - B.X;
            int c1 = a1 * (A.X) + b1 * (A.Y);

            // Line CD represented as a2x + b2y = c2  
            int a2 = D.Y - C.Y;
            int b2 = C.X - D.X;
            int c2 = a2 * (C.X) + b2 * (C.Y);

            double determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                throw new Exception("Lines are in parallel");
            }
            else
            {
                int x = (int)((b2 * c1 - b1 * c2) / determinant);
                int y = (int)((a1 * c2 - a2 * c1) / determinant);
                return new Point(x, y);
            }
        }

        internal static Point LineIntersection(Line ab, Line cd)
        {
            return LineIntersection(ab.A, ab.B, cd.A, cd.B);
        }

        internal static Point GetPoint(Point a, Point b, double offSet)
        {
            return new Point(GetPointX(a, b, offSet), GetPointY(a, b, offSet));
        }

        internal static Point GetPoint(Line line, double offSet)
        {
            return new Point(GetPointX(line, offSet), GetPointY(line, offSet));
        }

        internal static float[] PointsToArray(params Point[] points)
        {
            List<float> ret = new List<float>();
            foreach (Point p in points)
            {
                ret.Add(p.X);
                ret.Add(p.Y);
            }
            return ret.ToArray();
        }

        internal static float[] LinesToArray(params Line[] lines)
        {
            List<float> ret = new List<float>();
            foreach (Line l in lines)
            {
                ret.Add(l.AX);
                ret.Add(l.AY);
                ret.Add(l.BX);
                ret.Add(l.BY);
            }
            return ret.ToArray();
        }

        internal static double GetDistance(Point pointA, Point pointB)
        {
            int a = pointB.X - pointA.X;
            int b = pointB.Y - pointA.Y;

            return Math.Sqrt(a * a + b * b);
        }

        internal static double PolygonArea(params Point[] polygon)
        {
            int i, j;
            double area = 0;

            for (i = 0; i < polygon.Length; i++)
            {
                j = (i + 1) % polygon.Length;

                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }

            area /= 2;
            return (area < 0 ? -area : area);
        }
    }
}