using MassChecker.Geometry;

namespace MassChecker.Anchors
{
    internal class AnchorRect
    {
        #region Constructor

        internal AnchorRect(int baseWidth, int baseHeight, double aspectRatio, double paddingRatio, double rectOverMainRatio, double headerHeightRatio, int bottomOffset)
        {
            Width = baseWidth;
            Height = baseHeight;
            AspectRatio = aspectRatio;
            PaddingRatio = paddingRatio;
            RectOverMainRatio = rectOverMainRatio;
            HeaderHeightRatio = headerHeightRatio;
            BottomOffset = bottomOffset;
            InitAnchor();
        }

        internal void InitAnchor()
        {
            // center
            // MainAnchor = new Rect(
            //    Padding,
            //    (Height / 2) - ((int)(Width * AspectRatio) / 2),
            //    Width - Padding,
            //    (Height / 2) + ((int)(Width * AspectRatio) / 2));

            // top
            MainAnchor = new Rect(
                Padding,
                Padding,
                Width - Padding,
                Height - Padding);
            AnchorL = new Rect(
                MainAnchor.Left,
                MainAnchor.Top,
                MainAnchor.Left + RectAnchorSide,
                MainAnchor.Bottom);
            AnchorR = new Rect(
                MainAnchor.Right - RectAnchorSide,
                MainAnchor.Top,
                MainAnchor.Right,
                MainAnchor.Bottom);
            AnchorTL = new Rect(
                MainAnchor.Left,
                MainAnchor.Top,
                MainAnchor.Left + RectAnchorSide,
                MainAnchor.Top + (int)(RectAnchorSide + RectAnchorSide * 0.25));
            AnchorTR = new Rect(
                MainAnchor.Right - RectAnchorSide,
                MainAnchor.Top,
                MainAnchor.Right,
                MainAnchor.Top + (int)(RectAnchorSide + RectAnchorSide * 0.25));
            AnchorBL = new Rect(
                MainAnchor.Left,
                MainAnchor.Bottom - RectAnchorSide - BottomOffset,
                MainAnchor.Left + RectAnchorSide,
                MainAnchor.Bottom - BottomOffset);
            AnchorBR = new Rect(
                MainAnchor.Right - RectAnchorSide,
                MainAnchor.Bottom - RectAnchorSide - BottomOffset,
                MainAnchor.Right,
                MainAnchor.Bottom - BottomOffset);
        }

        #endregion

        #region Properties

        internal int Width;
        internal int Height;
        internal double AspectRatio;
        internal double PaddingRatio;
        internal double RectOverMainRatio;
        internal double HeaderHeightRatio;
        internal int BottomOffset;

        internal int Padding { get { return (int)(Width * PaddingRatio); } }
        internal int HeaderHeight { get { return (int)(MainAnchor.Height * HeaderHeightRatio); } }
        internal int RectAnchorSide { get { return (int)(MainAnchor.Width * RectOverMainRatio); } }

        #endregion

        #region RectAnchor

        internal Rect MainAnchor { get; private set; }
        internal Rect AnchorL { get; private set; }
        internal Rect AnchorR { get; private set; }
        internal Rect AnchorTL { get; private set; }
        internal Rect AnchorTR { get; private set; }
        internal Rect AnchorBL { get; private set; }
        internal Rect AnchorBR { get; private set; }

        #endregion
    }
}