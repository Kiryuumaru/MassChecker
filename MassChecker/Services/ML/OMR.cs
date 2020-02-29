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
            private static readonly HaarCascade face = new HaarCascade("haarcascade_frontalface_default.xml");
            private static readonly List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
            private static readonly List<string> labels = new List<string>();
            private static readonly EventHandler frameGrabberHandler = new EventHandler(FrameGrabber);

            private static Capture grabber;
            private static Image<Bgr, byte> currentFrame = null;
            private static Image<Bgr, byte> resize = null;
            private static Image<Gray, byte> gray = null;
            private static Image<Gray, byte> thres = null;
            private static Image<Gray, byte> erode = null;
            private static Image<Gray, byte> dilate = null;
            private static Image<Gray, byte> result = null;

            private static ImageBox imageBoxFrameGrabber;

            public static Action<string> OnFaceRecognized;

            public static Action<string> Logger;
            public static bool Started { get; private set; } = false;
            public static bool ShowLabel { get; set; } = false;

            public static int DistanceDetected { get; private set; } = -1;

            public static void Init()
            {
                ML.Init();
                if (!Directory.Exists(Extension.ImagesDir)) Directory.CreateDirectory(Extension.ImagesDir);
                try
                {
                    foreach (string file in Directory.EnumerateFiles(Extension.ImagesDir, "*.bmp"))
                    {
                        string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                        if (!fileName.Contains("_")) continue;
                        string label = fileName.Substring(0, fileName.LastIndexOf("_"));
                        trainingImages.Add(new Image<Gray, byte>(Path.Combine(file)));
                        labels.Add(label);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    //MessageBox.Show("Nothing in binary database, please add at least a face(Simply train the prototype with the Add Face Button).", "Triained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            public static void StartCamera(ImageBox frameGrabber)
            {
                imageBoxFrameGrabber = frameGrabber;
                grabber = new Capture();
                grabber.QueryFrame();
                Application.Idle += frameGrabberHandler;
                Started = true;
            }

            public static void StartFile(ImageBox frameGrabber, string filename)
            {
                imageBoxFrameGrabber = frameGrabber;
                grabber = new Capture(filename);
                grabber.QueryFrame();
                Application.Idle += frameGrabberHandler;
                Started = true;
            }

            public static void Stop()
            {
                Started = false;
                Application.Idle -= frameGrabberHandler;
                if (grabber != null)
                {
                    grabber.Dispose();
                    grabber = null;
                }
            }

            private static void FrameGrabber(object sender, EventArgs e)
            {
                if (!Started) return;

                currentFrame = grabber.QueryFrame();
                if (currentFrame == null)
                {
                    Logger?.Invoke("End of video");
                    Stop();
                    return;
                }
                resize = currentFrame.Resize(420, 340, INTER.CV_INTER_CUBIC);
                gray = resize.Convert<Gray, byte>();
                thres = gray.ThresholdBinary(new Gray(230), new Gray(254));
                erode = thres.Erode(1);
                dilate = erode.Dilate(2);

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
                    List<ContourPair> filtered2 = new List<ContourPair>();
                    foreach (Contour<Point> c1 in filtered1)
                    {
                        foreach (Contour<Point> c2 in filtered1)
                        {
                            if (c1 != c2 &&
                                c1.BoundingRectangle.Y > c2.BoundingRectangle.Y - 5 &&
                                c1.BoundingRectangle.Y < c2.BoundingRectangle.Y + 5)
                            {
                                filtered2.Add(new ContourPair(c1, c2));
                            }
                        }
                    }
                    List<ContourPair> filtered3 = new List<ContourPair>();
                    foreach (ContourPair pair in filtered2)
                    {
                        if (!filtered3.Any(item => item.Similar(pair)))
                        {
                            filtered3.Add(pair);
                            resize.Draw(pair.C1.BoundingRectangle, new Bgr(255, 0, 0), 1);
                            resize.Draw(pair.C2.BoundingRectangle, new Bgr(0, 0, 255), 1);
                        }
                    }
                    if (filtered3.Count == 1)
                    {
                        double px = filtered3[0].GetDistance();
                        double px2 = px * 0.5;
                        double dis = -0.789272 * Math.Log(0.0137459 * px);
                        Logger?.Invoke("Px = " + px.ToString("0.00") +
                            " Dis = " + dis.ToString("0.00") + " meters");
                    }
                }

                imageBoxFrameGrabber.Image = resize;
            }
        }
    }
}
