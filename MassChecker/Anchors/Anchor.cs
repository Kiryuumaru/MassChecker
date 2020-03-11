using Checkmate.Solvers;
using MassChecker.Geometry;
using MassChecker.Models;
using System.Collections.Generic;

namespace MassChecker.Anchors
{
    internal class Anchor
    {
        #region Properties

        internal int Width;
        internal int Height;
        internal Assessment Assessment;
        internal List<Shade> RawShades;
        internal AnchorRect AnchorRect;
        internal PaperParser PaperParser;
        internal SetParser SetParser;
        internal AnswerParser AnswerParser;
        internal List<ItemParser> ItemParsers;
        internal bool IsResultReady;

        internal List<ShadeItemKey> ShadeItemKeys { get; private set; }

        #endregion

        #region Constructors

        internal Anchor(int baseWidth, int baseHeight, Assessment assessment)
        {
            Width = baseWidth;
            Height = baseHeight;
            Assessment = assessment;

            ShadeItemKeys = new List<ShadeItemKey>();
            ItemParsers = new List<ItemParser>();

            switch (assessment.AssessmentType)
            {
                case AssessmentType.Item10:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 0.7047, 0, 0.225, 0.28, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.25, 0.004, 0.2);
                    SetParser = new SetParser(0.51, 0.5, 0.975, 1);
                    ItemParsers.Add(new ItemParser(0.05, 0.12, 0.447, 0.87, 1, 5));
                    ItemParsers.Add(new ItemParser(0.5475, 0.12, 0.945, 0.87, 6, 5));
                    break;
                case AssessmentType.Item20:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.383, 0, 0.225, 0.13, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.004, 0.2);
                    SetParser = new SetParser(0.51, 0.5, 0.975, 1);
                    ItemParsers.Add(new ItemParser(0.055, 0.03, 0.45, 0.96, 1, 10));
                    ItemParsers.Add(new ItemParser(0.5525, 0.03, 0.94, 0.96, 11, 10));
                    break;
                case AssessmentType.Item30:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.383, 0, 0.225, 0.13, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.004, 0.2);
                    SetParser = new SetParser(0.51, 0.5, 0.975, 1);
                    ItemParsers.Add(new ItemParser(0.0525, 0.05, 0.4494, 0.945, 1, 15));
                    ItemParsers.Add(new ItemParser(0.55, 0.05, 0.9425, 0.945, 16, 15));
                    break;
                case AssessmentType.Item40:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.4091, 0, 0.225, 0.085, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.0035, 0.2);
                    SetParser = new SetParser(0.65, 0.5, 0.96, 1);
                    ItemParsers.Add(new ItemParser(0.071, 0.0458, 0.33, 0.955, 1, 15));
                    ItemParsers.Add(new ItemParser(0.378, 0.0458, 0.6325, 0.955, 16, 15));
                    ItemParsers.Add(new ItemParser(0.677, 0.0458, 0.9375, 0.655, 31, 10));
                    break;
                case AssessmentType.Item50:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.4759, 0, 0.225, 0.081, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.0035, 0.2);
                    SetParser = new SetParser(0.65, 0.5, 0.96, 1);
                    ItemParsers.Add(new ItemParser(0.07, 0.05, 0.328, 0.95, 1, 20));
                    ItemParsers.Add(new ItemParser(0.377, 0.05, 0.634, 0.948, 21, 20));
                    ItemParsers.Add(new ItemParser(0.68, 0.05, 0.937, 0.495, 41, 10));
                    break;
                case AssessmentType.Item60:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.4759, 0, 0.225, 0.081, 0);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.0035, 0.2);
                    SetParser = new SetParser(0.65, 0.5, 0.96, 1);
                    ItemParsers.Add(new ItemParser(0.07, 0.05, 0.328, 0.95, 1, 20));
                    ItemParsers.Add(new ItemParser(0.377, 0.05, 0.634, 0.948, 21, 20));
                    ItemParsers.Add(new ItemParser(0.68, 0.05, 0.937, 0.95, 41, 20));
                    break;
                case AssessmentType.Item70:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.3497, 0, 0.225, 0.065, 75);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.003, 0.2);
                    SetParser = new SetParser(0.75, 0.45, 0.968, 1);
                    ItemParsers.Add(new ItemParser(0.047, 0.03, 0.232, 0.964, 1, 20));
                    ItemParsers.Add(new ItemParser(0.288, 0.03, 0.472, 0.964, 21, 20));
                    ItemParsers.Add(new ItemParser(0.53, 0.03, 0.71, 0.964, 41, 20));
                    ItemParsers.Add(new ItemParser(0.765, 0.03, 0.95, 0.5, 61, 10));
                    break;
                case AssessmentType.Item80:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.3497, 0, 0.225, 0.065, 75);
                    PaperParser = new PaperParser(AnchorRect, 0.25, 0.2, 0.003, 0.2);
                    SetParser = new SetParser(0.75, 0.45, 0.968, 1);
                    ItemParsers.Add(new ItemParser(0.047, 0.03, 0.232, 0.964, 1, 20));
                    ItemParsers.Add(new ItemParser(0.288, 0.03, 0.472, 0.964, 21, 20));
                    ItemParsers.Add(new ItemParser(0.53, 0.03, 0.71, 0.964, 41, 20));
                    ItemParsers.Add(new ItemParser(0.765, 0.03, 0.95, 0.964, 61, 20));
                    break;
                case AssessmentType.Item90:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.4344, 0, 0.225, 0.0575, 20);
                    PaperParser = new PaperParser(AnchorRect, 0.3, 0.25, 0.003, 0.2);
                    SetParser = new SetParser(0.75, 0.45, 0.968, 1);
                    ItemParsers.Add(new ItemParser(0.047, 0.03, 0.232, 0.964, 1, 25));
                    ItemParsers.Add(new ItemParser(0.288, 0.03, 0.472, 0.964, 26, 25));
                    ItemParsers.Add(new ItemParser(0.53, 0.03, 0.71, 0.964, 51, 25));
                    ItemParsers.Add(new ItemParser(0.765, 0.03, 0.95, 0.588, 76, 15));
                    break;
                case AssessmentType.Item100:
                    AnchorRect = new AnchorRect(baseWidth, baseHeight, 1.4344, 0, 0.225, 0.0575, 20);
                    PaperParser = new PaperParser(AnchorRect, 0.3, 0.25, 0.003, 0.2);
                    SetParser = new SetParser(0.75, 0.45, 0.968, 1);
                    ItemParsers.Add(new ItemParser(0.047, 0.03, 0.232, 0.964, 1, 25));
                    ItemParsers.Add(new ItemParser(0.288, 0.03, 0.472, 0.964, 26, 25));
                    ItemParsers.Add(new ItemParser(0.53, 0.03, 0.71, 0.964, 51, 25));
                    ItemParsers.Add(new ItemParser(0.765, 0.03, 0.95, 0.964, 76, 25));
                    break;
            }
        }

        #endregion

        #region Methods

        internal void Process(List<Shade> shades)
        {
            RawShades = shades;
            IsResultReady = false;
            PaperParser.Process(shades);
            ShadeItemKeys.Clear();
            if (PaperParser.MainReady)
            {
                foreach (ItemParser itemParser in ItemParsers)
                {
                    itemParser.Process(PaperParser);
                    ShadeItemKeys.AddRange(itemParser.ItemKeys);
                }
                SetParser.Process(PaperParser);
                PaperItem[] keys = new PaperItem[Assessment.GetTypeLength(Assessment.AssessmentType)];
                for (int i = 0; i < keys.Length; i++) keys[i] = new PaperItem(false, false, false, false, false);
                foreach (ShadeItemKey shadeItemKey in ShadeItemKeys)
                {
                    switch (shadeItemKey.Key)
                    {
                        case Key.A:
                            keys[shadeItemKey.ItemNumber - 1].A = true;
                            break;
                        case Key.B:
                            keys[shadeItemKey.ItemNumber - 1].B = true;
                            break;
                        case Key.C:
                            keys[shadeItemKey.ItemNumber - 1].C = true;
                            break;
                        case Key.D:
                            keys[shadeItemKey.ItemNumber - 1].D = true;
                            break;
                        case Key.E:
                            keys[shadeItemKey.ItemNumber - 1].E = true;
                            break;
                    }
                }
                AnswerParser = AnswerParser.Parse(Assessment, keys, SetParser.SetParserResult);
                IsResultReady = true;
            }
        }

        #endregion
    }
}