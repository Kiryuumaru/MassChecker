using System.Collections.Generic;
using MassChecker.Anchors;
using MassChecker.Models;

namespace Checkmate.Solvers
{
    internal enum AssessmentResult
    {
        Passed,
        Failed
    }

    internal class AnswerParser
    {
        #region Statics

        internal static AnswerParser Parse(Assessment assessment, PaperItem[] paperItems, SetParserResult setParserResult)
        {
            #region Set

            AnswerParserResultType resultType = AnswerParserResultType.Valid;
            AssessmentSet set = AssessmentSet.SetA;

            if (assessment.SetBEnabled || assessment.SetCEnabled)
            {
                switch (setParserResult)
                {
                    case SetParserResult.SetB:
                        if (assessment.SetBEnabled) set = AssessmentSet.SetB;
                        else resultType = AnswerParserResultType.DisabledSetShaded;
                        break;
                    case SetParserResult.SetC:
                        if (assessment.SetCEnabled) set = AssessmentSet.SetC;
                        else resultType = AnswerParserResultType.DisabledSetShaded;
                        break;
                    case SetParserResult.Unshaded:
                        resultType = AnswerParserResultType.SetUnshaded;
                        break;
                    case SetParserResult.Multishaded:
                        resultType = AnswerParserResultType.SetMultishaded;
                        break;
                }
            }

            if (resultType != AnswerParserResultType.Valid)
            {
                return new AnswerParser() { ParserResult = resultType };
            }

            #endregion

            #region Items

            AssessmentItem[] answerKeys = assessment.GetItems(set);
            if (answerKeys.Length != paperItems.Length)
            {
                return new AnswerParser() { ParserResult = AnswerParserResultType.Undefined };
            }
            PaperKeyResultType[] paperKeyResultTypes = new PaperKeyResultType[answerKeys.Length];
            for (int i = 0; i < answerKeys.Length; i++)
            {
                paperKeyResultTypes[i] = PaperKeyResultType.Correct;
                int ansNum = 0;
                ansNum += paperItems[i].A ? 1 : 0;
                ansNum += paperItems[i].B ? 1 : 0;
                ansNum += paperItems[i].C ? 1 : 0;
                ansNum += paperItems[i].D ? 1 : 0;
                ansNum += paperItems[i].E ? 1 : 0;
                if (ansNum == 0)
                {
                    paperKeyResultTypes[i] = PaperKeyResultType.Unshaded;
                }
                else if (ansNum > 1)
                {
                    paperKeyResultTypes[i] = PaperKeyResultType.Invalid;
                }
                else
                {
                    switch (answerKeys[i].Key)
                    {
                        case Key.A:
                            paperKeyResultTypes[i] = paperItems[i].A ? PaperKeyResultType.Correct : PaperKeyResultType.Incorrect;
                            break;
                        case Key.B:
                            paperKeyResultTypes[i] = paperItems[i].B ? PaperKeyResultType.Correct : PaperKeyResultType.Incorrect;
                            break;
                        case Key.C:
                            paperKeyResultTypes[i] = paperItems[i].C ? PaperKeyResultType.Correct : PaperKeyResultType.Incorrect;
                            break;
                        case Key.D:
                            paperKeyResultTypes[i] = paperItems[i].D ? PaperKeyResultType.Correct : PaperKeyResultType.Incorrect;
                            break;
                        case Key.E:
                            paperKeyResultTypes[i] = paperItems[i].E ? PaperKeyResultType.Correct : PaperKeyResultType.Incorrect;
                            break;
                    }
                }
            }

            #endregion

            #region Overall

            int correctItems = 0;
            int incorrectItems = 0;
            int invalidItems = 0;
            int unshadedItems = 0;
            int totalItems = 0;
            int correctPoints = 0;
            int incorrectPoints = 0;
            int invalidPoints = 0;
            int unshadedPoints = 0;
            int totalPoints = 0;

            for (int i = 0; i < paperKeyResultTypes.Length; i++)
            {
                switch (paperKeyResultTypes[i])
                {
                    case PaperKeyResultType.Correct:
                        correctItems++;
                        correctPoints += answerKeys[i].Points;
                        break;
                    case PaperKeyResultType.Incorrect:
                        incorrectItems++;
                        incorrectPoints += answerKeys[i].Points;
                        break;
                    case PaperKeyResultType.Invalid:
                        invalidItems++;
                        invalidPoints += answerKeys[i].Points;
                        break;
                    case PaperKeyResultType.Unshaded:
                        unshadedItems++;
                        unshadedPoints += answerKeys[i].Points;
                        break;
                }
                totalItems++;
                totalPoints += answerKeys[i].Points;
            }


            double percentage = 100 * ((double)correctPoints / totalPoints);

            AssessmentResult assessmentResult = percentage >= assessment.PassingRate ? AssessmentResult.Passed : AssessmentResult.Failed;

            return new AnswerParser()
            {
                ParserResult = AnswerParserResultType.Valid,
                AssessmentSet = set,
                Answers = paperItems,
                AnswerResults = paperKeyResultTypes,
                AssessmentResult = assessmentResult,
                CorrectItems = correctItems,
                IncorrectItems = incorrectItems,
                InvalidItems = invalidItems,
                UnshadedItems = unshadedItems,
                TotalItems = totalItems,
                CorrectPoints = correctPoints,
                IncorrectPoints = incorrectPoints,
                InvalidPoints = invalidPoints,
                UnshadedPoints = unshadedPoints,
                TotalPoints = totalPoints,
                Percentage = percentage
            };

            #endregion
        }

        internal static AnswerParser Parse(Assessment assessment, PaperItem[] paperItems, AssessmentSet assessmentSet)
        {
            switch (assessmentSet)
            {
                case AssessmentSet.SetA:
                    return Parse(assessment, paperItems, SetParserResult.SetA);
                case AssessmentSet.SetB:
                    return Parse(assessment, paperItems, SetParserResult.SetB);
                case AssessmentSet.SetC:
                    return Parse(assessment, paperItems, SetParserResult.SetC);
            }
            return null;
        }

        #endregion

        #region Properties

        internal AnswerParserResultType ParserResult { get; private set; }
        internal AssessmentSet AssessmentSet { get; private set; }
        internal PaperItem[] Answers { get; private set; }
        internal PaperKeyResultType[] AnswerResults { get; private set; }
        internal AssessmentResult AssessmentResult { get; private set; }
        internal int CorrectItems { get; private set; } = 0;
        internal int IncorrectItems { get; private set; } = 0;
        internal int InvalidItems { get; private set; } = 0;
        internal int UnshadedItems { get; private set; } = 0;
        internal int TotalItems { get; private set; } = 0;
        internal int CorrectPoints { get; private set; } = 0;
        internal int IncorrectPoints { get; private set; } = 0;
        internal int InvalidPoints { get; private set; } = 0;
        internal int UnshadedPoints { get; private set; } = 0;
        internal int TotalPoints { get; private set; } = 0;
        internal double Percentage { get; private set; } = 0;

        #endregion

        #region Initializers

        private AnswerParser()
        {
        }


        #endregion
    }
}