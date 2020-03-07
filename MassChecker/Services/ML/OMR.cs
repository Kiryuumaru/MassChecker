using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using MassChecker.Anchors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MassChecker.Models;
using MassChecker.Diagnostics;

namespace MassChecker.Services
{
    public static partial class ML
    {
        public static class OMR
        {
            private static Rectangle roi = new Rectangle(215, 190, 810, 1125);
            private static int blockSize = 51;
            private static double param1 = 15;
            private static int erodeI = 3;
            private static int dilateI = 3;

            private static Anchor anchor;
            private static AnchorDiagnostics anchorDiag;
            private static Capture grabber;
            private static Image<Bgr, byte> currentFrame = null;
            private static Image<Bgr, byte> crop = null;
            private static Image<Gray, byte> gray = null;
            private static Image<Gray, byte> thres = null;
            private static Image<Gray, byte> erode = null;
            private static Image<Gray, byte> dilate = null;

            private static ImageBox imageBoxFrameGrabber;

            public static Action Logger;

            public static void ProcessFile(ImageBox frameGrabber, string filename)
            {
                anchor = new Anchor(roi.Width, roi.Height, PartialDB.GetAssessment());
                anchorDiag = new AnchorDiagnostics(anchor);
                imageBoxFrameGrabber = frameGrabber;
                grabber = new Capture(filename);

                currentFrame = grabber.QueryFrame();
                crop = currentFrame.Copy(roi);
                //result = crop.Resize(baseWidth, baseHeight, INTER.CV_INTER_AREA);
                gray = crop.Convert<Gray, byte>();

                thres = new Image<Gray, byte>(new Size(gray.Width, gray.Height));
                IntPtr srcPtr = gray.Ptr;
                IntPtr dstPtr = thres.Ptr;

                CvInvoke.cvAdaptiveThreshold(srcPtr, dstPtr, 255, ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, THRESH.CV_THRESH_BINARY_INV, blockSize, param1);

                erode = thres.Erode(erodeI);
                dilate = erode.Dilate(dilateI);

                List<Geometry.Shade> filtered1 = new List<Geometry.Shade>();
                using (MemStorage storage = new MemStorage())
                {
                    Contour<Point> contours = dilate.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, RETR_TYPE.CV_RETR_TREE, storage);
                    while (contours != null)
                    {
                        Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);
                        if (currentContour.BoundingRectangle.Width > 10 &&
                            currentContour.BoundingRectangle.Width < 50 &&
                            currentContour.BoundingRectangle.Height > 10 &&
                            currentContour.BoundingRectangle.Height < 50)
                        {
                            filtered1.Add(new Geometry.Shade(
                                currentContour.BoundingRectangle.X,
                                currentContour.BoundingRectangle.Y,
                                currentContour.BoundingRectangle.Width,
                                currentContour.BoundingRectangle.Height));
                        }
                        contours = contours.HNext;
                    }
                }
                anchor.Process(filtered1);
                anchorDiag.DrawNormal(crop);
                imageBoxFrameGrabber.Image = crop;
            }
        }
    }
}
