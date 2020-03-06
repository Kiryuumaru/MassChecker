using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassChecker.Services
{
    public static partial class ML
    {
        public class ContourPair
        {
            public Contour<Point> C1 { get; private set; }
            public Contour<Point> C2 { get; private set; }
            public ContourPair (Contour<Point> c1, Contour<Point> c2)
            {
                C1 = c1;
                C2 = c2;
            }
            public bool Similar(ContourPair pair)
            {
                return (C1 == pair.C1 && C2 == pair.C2) || (C1 == pair.C2 && C2 == pair.C1);
            }

            internal double GetDistance()
            {
                double a = C2.BoundingRectangle.X + (C2.BoundingRectangle.Width / 2) - C1.BoundingRectangle.X + (C1.BoundingRectangle.Width / 2);
                double b = C2.BoundingRectangle.Y + (C2.BoundingRectangle.Height / 2) - C1.BoundingRectangle.Y + (C1.BoundingRectangle.Height / 2);

                return Math.Sqrt(a * a + b * b);
            }
        }

        public static class OMR
        {
            private static Rectangle roi = new Rectangle(80, 60, 370, 280);
            private static int blockSize = 11;
            private static double param1 = 15;
            private static int erodeI = 1;
            private static int dilateI = 1;

            private static Capture grabber;
            private static Image<Bgr, byte> currentFrame = null;
            private static Image<Bgr, byte> crop = null;
            private static Image<Gray, byte> gray = null;
            private static Image<Gray, byte> thres = null;
            private static Image<Gray, byte> erode = null;
            private static Image<Gray, byte> dilate = null;
            private static Image<Gray, byte> result = null;

            private static ImageBox imageBoxFrameGrabber;

            public static Action<string> Logger;

            public static void ProcessFile(ImageBox frameGrabber, string filename)
            {
                imageBoxFrameGrabber = frameGrabber;
                grabber = new Capture(filename);

                currentFrame = grabber.QueryFrame();
                crop = currentFrame.Copy(roi);
                gray = crop.Convert<Gray, byte>();

                thres = new Image<Gray, byte>(new Size(gray.Width, gray.Height));
                IntPtr srcPtr = gray.Ptr;
                IntPtr dstPtr = thres.Ptr;

                CvInvoke.cvAdaptiveThreshold(srcPtr, dstPtr, 255, ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, THRESH.CV_THRESH_BINARY_INV, blockSize, param1);

                erode = thres.Erode(erodeI);
                dilate = erode.Dilate(dilateI);

                using (MemStorage storage = new MemStorage())
                {
                    List<Contour<Point>> filtered1 = new List<Contour<Point>>();
                    Contour<Point> contours = dilate.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, RETR_TYPE.CV_RETR_TREE, storage);
                    while (contours != null)
                    {
                        Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);
                        if (currentContour.BoundingRectangle.Width > 3 &&
                            currentContour.BoundingRectangle.Width < 70 &&
                            currentContour.BoundingRectangle.Height > 3 &&
                            currentContour.BoundingRectangle.Height < 70 &&
                            currentContour.BoundingRectangle.X > 140 &&
                            currentContour.BoundingRectangle.X < 280)
                        {
                            filtered1.Add(currentContour);
                        }
                        contours = contours.HNext;
                    }
                }

                imageBoxFrameGrabber.Image = crop;
            }
        }
    }
}
