using System;
using System.Collections.Generic;

using MassChecker.Geometry;

namespace MassChecker.Anchors
{
    internal class PaperParser
    {
        #region Properties

        internal bool MainReady { get; set; }
        internal AnchorRect AnchorRect { get; private set; }
        internal Arbitary3DRect HeaderRect { get; private set; }
        internal Arbitary3DRect BodyRect { get; private set; }
        internal List<Shade> HeaderShades { get; private set; }
        internal List<Shade> BodyShades { get; private set; }

        internal double HeaderHeightMarginMultiplier;
        internal double CenterHeightMarginMultiplier;
        internal double AcceptableAreaMultiplier;
        internal double AspectRatioMargin;

        internal Shade TLPoint;
        internal Shade TRPoint;
        internal Shade HLPoint;
        internal Shade HRPoint;
        internal Shade CLPoint;
        internal Shade CRPoint;
        internal Shade BLPoint;
        internal Shade BRPoint;

        private readonly List<Shade> l;
        private readonly List<Shade> r;
        private readonly List<Shade> tl;
        private readonly List<Shade> tr;
        private readonly List<Shade> bl;
        private readonly List<Shade> br;

        private double headerHeightCalc;
        private double headerHeightMargin;
        private double headerHeightActual;
        private double headerHeightCurrentDiff;

        private double centerHeightCalc;
        private double centerHeightMargin;
        private double centerHeightActual;
        private double centerHeightCurrentDiff;

        private double acceptableArea;
        private double areaActual1;
        private double areaActual2;
        private double areaCurrent1;
        private double areaCurrent2;

        private double aspectRatio;

        private readonly List<System.Drawing.Point> borders;

        #endregion

        #region Constructor

        internal PaperParser(
            AnchorRect anchorRect,
            double headerHeightMarginMultiplier,
            double centerHeightMarginMultiplier,
            double acceptableAreaMultiplier,
            double aspectRatioMargin)
        {
            AnchorRect = anchorRect;
            HeaderHeightMarginMultiplier = headerHeightMarginMultiplier;
            CenterHeightMarginMultiplier = centerHeightMarginMultiplier;
            AcceptableAreaMultiplier = acceptableAreaMultiplier;
            AspectRatioMargin = aspectRatioMargin;

            acceptableArea = anchorRect.MainAnchor.Height * anchorRect.MainAnchor.Width * acceptableAreaMultiplier;

            borders = new List<System.Drawing.Point>();
            HeaderShades = new List<Shade>();
            BodyShades = new List<Shade>();

            l = new List<Shade>();
            r = new List<Shade>();
            tl = new List<Shade>();
            tr = new List<Shade>();
            bl = new List<Shade>();
            br = new List<Shade>();
        }

        #endregion

        #region Methods

        internal System.Drawing.Point[] GetBorders()
        {
            borders.Clear();
            borders.AddRange(new System.Drawing.Point[]
            {
                new System.Drawing.Point(HRPoint.Center.X, HRPoint.Center.Y),
                new System.Drawing.Point(HLPoint.Center.X, HLPoint.Center.Y),

                new System.Drawing.Point(HLPoint.Center.X, HLPoint.Center.Y),
                new System.Drawing.Point(TLPoint.Center.X, TLPoint.Center.Y),

                new System.Drawing.Point(TLPoint.Center.X, TLPoint.Center.Y),
                new System.Drawing.Point(TRPoint.Center.X, TRPoint.Center.Y),

                new System.Drawing.Point(TRPoint.Center.X, TRPoint.Center.Y),
                new System.Drawing.Point(HRPoint.Center.X, HRPoint.Center.Y),

                new System.Drawing.Point(HRPoint.Center.X, HRPoint.Center.Y),
                new System.Drawing.Point(CRPoint.Center.X, CRPoint.Center.Y),

                new System.Drawing.Point(CRPoint.Center.X, CRPoint.Center.Y),
                new System.Drawing.Point(BRPoint.Center.X, BRPoint.Center.Y),

                new System.Drawing.Point(BRPoint.Center.X, BRPoint.Center.Y),
                new System.Drawing.Point(BLPoint.Center.X, BLPoint.Center.Y),

                new System.Drawing.Point(BLPoint.Center.X, BLPoint.Center.Y),
                new System.Drawing.Point(CLPoint.Center.X, CLPoint.Center.Y),

                new System.Drawing.Point(CLPoint.Center.X, CLPoint.Center.Y),
                new System.Drawing.Point(HLPoint.Center.X, HLPoint.Center.Y)
            });

            return borders.ToArray();
        }

        internal void Process(List<Shade> shades)
        {
            l.Clear();
            r.Clear();
            tl.Clear();
            tr.Clear();
            bl.Clear();
            br.Clear();
            MainReady = false;

            foreach (Shade shade in shades)
            {
                if (AnchorRect.AnchorL.Contains(shade.Center)) l.Add(shade);
                if (AnchorRect.AnchorR.Contains(shade.Center)) r.Add(shade);
                if (AnchorRect.AnchorTL.Contains(shade.Center)) tl.Add(shade);
                if (AnchorRect.AnchorTR.Contains(shade.Center)) tr.Add(shade);
                if (AnchorRect.AnchorBL.Contains(shade.Center)) bl.Add(shade);
                if (AnchorRect.AnchorBR.Contains(shade.Center)) br.Add(shade);
            }

            if (tl.Count > 0 && tr.Count > 0 && bl.Count > 0 && br.Count > 0)
            {
                if (l.Count < 4 && r.Count < 4) return;

                HeaderShades.Clear();
                BodyShades.Clear();

                if (!AssertSideWithCL(l, tl, bl, out TLPoint, out HLPoint, out CLPoint, out BLPoint) ||
                    !AssertSideWithCL(r, tr, br, out TRPoint, out HRPoint, out CRPoint, out BRPoint)) return;

                aspectRatio = ((Extension.GetDistance(TLPoint.Center, BLPoint.Center) + Extension.GetDistance(TRPoint.Center, BRPoint.Center)) / 2) /
                              ((Extension.GetDistance(TLPoint.Center, TRPoint.Center) + Extension.GetDistance(BLPoint.Center, BRPoint.Center)) / 2);

                if (aspectRatio > AnchorRect.AspectRatio + AspectRatioMargin ||
                    aspectRatio < AnchorRect.AspectRatio - AspectRatioMargin) return;

                HeaderRect = new Arbitary3DRect(TLPoint.Center, TRPoint.Center, HLPoint.Center, HRPoint.Center);
                BodyRect = new Arbitary3DRect(HLPoint.Center, HRPoint.Center, BLPoint.Center, BRPoint.Center);

                foreach (Shade shade in shades)
                {
                    if (HeaderRect.IsInside(shade.Center)) HeaderShades.Add(shade);
                    if (BodyRect.IsInside(shade.Center)) BodyShades.Add(shade);
                }
                MainReady = true;
            }
        }

        bool assertSuccessWithCL;
        internal bool AssertSideWithCL(List<Shade> side, List<Shade> top, List<Shade> bottom, out Shade t, out Shade h, out Shade c, out Shade b)
        {
            t = null;
            h = null;
            c = null;
            b = null;
            assertSuccessWithCL = false;
            foreach (Shade st in top)
            {
                foreach (Shade sh in side)
                {
                    foreach (Shade sc in side)
                    {
                        foreach (Shade sb in bottom)
                        {
                            if (sh == st || sh == sb || sc == st || sc == sb ||
                                st.Y >= sh.Y || sh.Y >= sc.Y || sc.Y >= sb.Y) continue;

                            // Area filter
                            areaActual1 = Extension.PolygonArea(st.Center, sh.Center, sb.Center);
                            areaActual2 = Extension.PolygonArea(st.Center, sc.Center, sb.Center);
                            if (areaActual1 > acceptableArea || areaActual2 > acceptableArea) continue;

                            // Area chooser
                            if (assertSuccessWithCL)
                            {
                                areaCurrent1 = Extension.PolygonArea(t.Center, h.Center, b.Center);
                                areaCurrent2 = Extension.PolygonArea(t.Center, c.Center, b.Center);
                                if (areaActual1 + areaActual2 > areaCurrent1 + areaCurrent2) continue;
                            }

                            // Header filter
                            headerHeightCalc = Extension.GetDistance(st.Center, sb.Center) * AnchorRect.HeaderHeightRatio;
                            headerHeightMargin = headerHeightCalc * HeaderHeightMarginMultiplier;
                            headerHeightActual = Extension.GetDistance(st.Center, sh.Center);
                            if (headerHeightActual > headerHeightCalc + headerHeightMargin ||
                                headerHeightActual < headerHeightCalc - headerHeightMargin) continue;

                            // Header chooser
                            if (assertSuccessWithCL)
                            {
                                headerHeightCurrentDiff = Math.Abs(Extension.GetDistance(t.Center, h.Center) - headerHeightCalc);
                                if (Math.Abs(headerHeightActual - headerHeightCalc) > headerHeightCurrentDiff) continue;
                            }

                            // Center filter
                            centerHeightCalc = Extension.GetDistance(sb.Center, sh.Center) * 0.5;
                            centerHeightMargin = centerHeightCalc * CenterHeightMarginMultiplier;
                            centerHeightActual = Extension.GetDistance(sb.Center, sc.Center);
                            if (centerHeightActual > centerHeightCalc + centerHeightMargin ||
                                centerHeightActual < centerHeightCalc - centerHeightMargin) continue;

                            // Center chooser
                            if (assertSuccessWithCL)
                            {
                                centerHeightCurrentDiff = Math.Abs(Extension.GetDistance(b.Center, c.Center) - centerHeightCalc);
                                if (Math.Abs(centerHeightActual - centerHeightCalc) > centerHeightCurrentDiff) continue;
                            }

                            t = st;
                            h = sh;
                            c = sc;
                            b = sb;

                            assertSuccessWithCL = true;
                        }
                    }
                }
            }
            return assertSuccessWithCL;
        }

        #endregion
    }
}