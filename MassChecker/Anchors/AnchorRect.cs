using MassChecker.Geometry;

namespace MassChecker.Anchors
{
    internal class AnchorRect
    {
        #region Constructor

        internal AnchorRect(int baseWidth, int baseHeight, double aspectRatio, double rectOverMainRatio, double headerHeightRatio, int leftOffset, int topOffset, int rightOffset, int bottomOffset)
        {
            Width = baseWidth;
            Height = baseHeight;
            AspectRatio = aspectRatio;
            RectOverMainRatio = rectOverMainRatio;
            HeaderHeightRatio = headerHeightRatio;
            LeftOffset = leftOffset;
            TopOffset = topOffset;
            RightOffset = rightOffset;
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
                LeftOffset,
                TopOffset,
                Width - RightOffset,
                Height - BottomOffset);
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
                MainAnchor.Bottom - RectAnchorSide,
                MainAnchor.Left + RectAnchorSide,
                MainAnchor.Bottom);
            AnchorBR = new Rect(
                MainAnchor.Right - RectAnchorSide,
                MainAnchor.Bottom - RectAnchorSide,
                MainAnchor.Right,
                MainAnchor.Bottom);
        }

        #endregion

        #region Properties

        internal int Width;
        internal int Height;
        internal double AspectRatio;
        internal double RectOverMainRatio;
        internal double HeaderHeightRatio;
        internal int LeftOffset;
        internal int TopOffset;
        internal int RightOffset;
        internal int BottomOffset;

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