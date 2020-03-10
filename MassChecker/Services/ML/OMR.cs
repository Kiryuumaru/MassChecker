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

            public static void ProcessFile(AssessmentType type, ImageBox frameGrabber, string filename)
            {
                Rectangle roi = new Rectangle(180, 155, 810, 1125);
                int blockSize = 51;
                double param1 = 15;
                int erodeI = 3;
                int dilateI = 3;
                int minRectSide = 10;
                int maxRectSide = 50;
                switch (type)
                {
                    case AssessmentType.Item50:
                        roi = new Rectangle(180, 155, 810, 1125);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                    case AssessmentType.Item60:
                        roi = new Rectangle(180, 155, 810, 1125);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                    case AssessmentType.Item70:
                        roi = new Rectangle(180, 155, 810, 1125);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                    case AssessmentType.Item80:
                        roi = new Rectangle(180, 155, 810, 1125);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                    case AssessmentType.Item90:
                        roi = new Rectangle(180, 155, 810, 1110);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                    case AssessmentType.Item100:
                        roi = new Rectangle(180, 155, 810, 1110);
                        blockSize = 51;
                        param1 = 15;
                        erodeI = 3;
                        dilateI = 3;
                        minRectSide = 10;
                        maxRectSide = 50;
                        break;
                }

                anchor = new Anchor(roi.Width, roi.Height, PartialDB.GetAssessment(type));
                anchorDiag = new AnchorDiagnostics(anchor);
                imageBoxFrameGrabber = frameGrabber;
                grabber = new Capture(filename);

                currentFrame = grabber.QueryFrame();
                crop = currentFrame.Copy(roi);
                gray = crop.Convert<Gray, byte>();

                thres = new Image<Gray, byte>(new Size(gray.Width, gray.Height));

                CvInvoke.cvAdaptiveThreshold(gray.Ptr, thres.Ptr, 255, ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, THRESH.CV_THRESH_BINARY_INV, blockSize, param1);

                erode = thres.Erode(erodeI);
                dilate = erode.Dilate(dilateI);

                List<Geometry.Shade> filtered1 = new List<Geometry.Shade>();
                using (MemStorage storage = new MemStorage())
                {
                    Contour<Point> contours = dilate.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, RETR_TYPE.CV_RETR_TREE, storage);
                    while (contours != null)
                    {
                        Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);
                        if (currentContour.BoundingRectangle.Width > minRectSide &&
                            currentContour.BoundingRectangle.Width < maxRectSide &&
                            currentContour.BoundingRectangle.Height > minRectSide &&
                            currentContour.BoundingRectangle.Height < maxRectSide)
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
