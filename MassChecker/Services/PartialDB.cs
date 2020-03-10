using MassChecker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassChecker.Services
{
    public static class PartialDB
    {
        public static Assessment GetAssessment(AssessmentType assessmentType)
        {
            if (!Directory.Exists(Extension.AssetsDir)) Directory.CreateDirectory(Extension.AssetsDir);
            string filepath = "";
            switch (assessmentType)
            {
                case AssessmentType.Item50:
                    filepath = Extension.Item50;
                    break;
                case AssessmentType.Item60:
                    filepath = Extension.Item60;
                    break;
                case AssessmentType.Item70:
                    filepath = Extension.Item70;
                    break;
                case AssessmentType.Item80:
                    filepath = Extension.Item80;
                    break;
                case AssessmentType.Item90:
                    filepath = Extension.Item90;
                    break;
                case AssessmentType.Item100:
                    filepath = Extension.Item100;
                    break;
            }
            if (!string.IsNullOrEmpty(filepath))
            {
                if (!File.Exists(filepath)) File.WriteAllText(filepath, "");
                string assessment = File.ReadAllText(filepath);
                if (string.IsNullOrEmpty(assessment)) return new Assessment(assessmentType, 75);
                return new Assessment(assessment);
            }
            else return null;
        }

        public static void SetAssessment(Assessment assessment)
        {
            switch (assessment.AssessmentType)
            {
                case AssessmentType.Item50:
                    File.WriteAllText(Extension.Item50, assessment.Bloberize());
                    break;
                case AssessmentType.Item60:
                    File.WriteAllText(Extension.Item60, assessment.Bloberize());
                    break;
                case AssessmentType.Item70:
                    File.WriteAllText(Extension.Item70, assessment.Bloberize());
                    break;
                case AssessmentType.Item80:
                    File.WriteAllText(Extension.Item80, assessment.Bloberize());
                    break;
                case AssessmentType.Item90:
                    File.WriteAllText(Extension.Item90, assessment.Bloberize());
                    break;
                case AssessmentType.Item100:
                    File.WriteAllText(Extension.Item100, assessment.Bloberize());
                    break;
            }
        }

        public static void ClearData()
        {
            File.WriteAllText(Extension.Item50, "");
            File.WriteAllText(Extension.Item60, "");
            File.WriteAllText(Extension.Item70, "");
            File.WriteAllText(Extension.Item80, "");
            File.WriteAllText(Extension.Item90, "");
            File.WriteAllText(Extension.Item100, "");
        }
    }
}
