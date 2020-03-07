﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MassChecker.Anchors;
using MassChecker.Geometry;

namespace MassChecker.Diagnostics
{
    internal class AnchorDiagnostics
    {
        private static Anchor anchor;
        private static Bgr ansColorCorrect = new Bgr(0, 255, 0);
        private static Bgr ansColorIncorrect = new Bgr(0, 0, 255);
        private static Bgr ansColorInvalid = new Bgr(0, 255, 255);
        private static Bgr rectColorTrue = new Bgr(0, 255, 0);
        private static Bgr rectColorFalse = new Bgr(0, 0, 255);
        private static MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 1d, 1d);
        private static bool isDrawing;

        internal AnchorDiagnostics(Anchor _anchor)
        {
            anchor = _anchor;
            font.thickness = 3;
        }

        private void DrawRect(Image<Bgr, byte> image, Rect rect, Bgr paint)
        {
            image.Draw(new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height), paint, 1);
        }

        private void Draw(Image<Bgr, byte> image, bool drawAnchorRect, bool drawPaperBorder, bool drawItemBorder, bool drawSetBorders, bool drawRawShades, bool drawAnchorShades, bool drawResults)
        {
            if (anchor.PaperParser.MainReady)
            {
                int xSetPos = anchor.SetParser.MainRect.TL.X + (int)((anchor.SetParser.MainRect.BL.X - anchor.SetParser.MainRect.TL.X) * 0.75);
                int ySetPos = anchor.SetParser.MainRect.TL.Y + (int)((anchor.SetParser.MainRect.BL.Y - anchor.SetParser.MainRect.TL.Y) * 0.75);
                
                if (drawSetBorders)
                {
                    image.DrawPolyline(anchor.SetParser.GetBorders(), false, rectColorTrue, 1);
                }

                if (drawResults && anchor.IsResultReady)
                {
                    switch (anchor.AnswerParser.ParserResult)
                    {
                        case Models.AnswerParserResultType.DisabledSetShaded:
                            image.Draw("Disabled set shaded", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorIncorrect);
                            break;
                        case Models.AnswerParserResultType.SetMultishaded:
                            image.Draw("Multi set shaded", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorIncorrect);
                            break;
                        case Models.AnswerParserResultType.SetUnshaded:
                            image.Draw("No set shaded", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorIncorrect);
                            break;
                        case Models.AnswerParserResultType.Undefined:
                            image.Draw("Undefined", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorIncorrect);
                            break;
                        default:
                            switch (anchor.SetParser.SetParserResult)
                            {
                                case SetParserResult.SetA:
                                    image.Draw("Set A", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorCorrect);
                                    break;
                                case SetParserResult.SetB:
                                    image.Draw("Set B", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorCorrect);
                                    break;
                                case SetParserResult.SetC:
                                    image.Draw("Set C", ref font, new System.Drawing.Point(xSetPos, ySetPos), ansColorCorrect);
                                    break;
                            }
                            foreach (ShadeItemKey s in anchor.ShadeItemKeys)
                            {
                                if (s == null) continue;
                                switch (anchor.AnswerParser.AnswerResults[s.ItemNumber - 1])
                                {
                                    case Models.PaperKeyResultType.Correct:
                                        image.Draw(
                                            s.KeyChar,
                                            ref font,
                                            new System.Drawing.Point(s.Shade.Bl.X, s.Shade.Bl.Y),
                                            ansColorCorrect);
                                        break;
                                    case Models.PaperKeyResultType.Incorrect:
                                        image.Draw(
                                            s.KeyChar,
                                            ref font,
                                            new System.Drawing.Point(s.Shade.Bl.X, s.Shade.Bl.Y),
                                            ansColorIncorrect);
                                        break;
                                    default:
                                        image.Draw(
                                            s.KeyChar,
                                            ref font,
                                            new System.Drawing.Point(s.Shade.Bl.X, s.Shade.Bl.Y),
                                            ansColorInvalid);
                                        break;
                                }
                            }
                            if (anchor.AnswerParser.AssessmentResult == Checkmate.Solvers.AssessmentResult.Passed)
                            {
                                image.Draw(
                                    "Score: " +
                                    anchor.AnswerParser.CorrectPoints.ToString() + "/" +
                                    anchor.AnswerParser.TotalPoints.ToString() + " Passed",
                                    ref font,
                                    new System.Drawing.Point(10, 30),
                                    ansColorCorrect);
                            }
                            else
                            {
                                image.Draw(
                                    "Score: " +
                                    anchor.AnswerParser.CorrectPoints.ToString() + "/" +
                                    anchor.AnswerParser.TotalPoints.ToString() + " Failed",
                                    ref font,
                                    new System.Drawing.Point(10, 30),
                                    ansColorIncorrect);
                            }
                            break;
                    }
                }

                if (drawPaperBorder)
                {
                    image.DrawPolyline(anchor.PaperParser.GetBorders(), false, rectColorTrue, 1);
                }

                if (drawItemBorder)
                {
                    foreach (ItemParser itemParser in anchor.ItemParsers)
                    {
                        image.DrawPolyline(itemParser.GetBorders(), false, rectColorTrue, 1);
                    }
                }
            }

            if (drawRawShades)
            {
                foreach (Shade s in anchor.RawShades)
                {
                    DrawRect(image, s.BoundingRect, rectColorFalse);
                }
            }

            if (drawAnchorShades)
            {
                if (anchor.PaperParser.MainReady)
                {
                    DrawRect(image, anchor.PaperParser.TLPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.TLPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.TRPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.HLPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.HRPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.CLPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.CRPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.BLPoint.BoundingRect, rectColorTrue);
                    DrawRect(image, anchor.PaperParser.BRPoint.BoundingRect, rectColorTrue);
                }
                else
                {
                    foreach (Shade s in anchor.RawShades)
                    {
                        if (!drawRawShades && (
                            anchor.AnchorRect.AnchorTL.Contains(s.Center) ||
                            anchor.AnchorRect.AnchorTR.Contains(s.Center) ||
                            anchor.AnchorRect.AnchorBL.Contains(s.Center) ||
                            anchor.AnchorRect.AnchorBR.Contains(s.Center)))
                            DrawRect(image, s.BoundingRect, rectColorFalse);
                    }
                }
            }

            if (drawAnchorRect)
            {
                DrawRect(image, anchor.AnchorRect.AnchorTL, anchor.PaperParser.MainReady ? rectColorTrue : rectColorFalse);
                DrawRect(image, anchor.AnchorRect.AnchorTR, anchor.PaperParser.MainReady ? rectColorTrue : rectColorFalse);
                DrawRect(image, anchor.AnchorRect.AnchorBL, anchor.PaperParser.MainReady ? rectColorTrue : rectColorFalse);
                DrawRect(image, anchor.AnchorRect.AnchorBR, anchor.PaperParser.MainReady ? rectColorTrue : rectColorFalse);
            }
        }

        internal void DrawNormal(Image<Bgr, byte> image)
        {
            if (isDrawing) return;
            isDrawing = true;
            Draw(image, false, false, false, false, false, true, true);
            isDrawing = false;
        }

        internal void DrawDiagnostics(Image<Bgr, byte> image)
        {
            if (isDrawing) return;
            isDrawing = true;
            Draw(image, true, true, true, true, true, true, true);
            isDrawing = false;
        }
    }
}