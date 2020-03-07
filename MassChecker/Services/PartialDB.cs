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
        public static Assessment GetAssessment()
        {
            if (!Directory.Exists(Extension.AssetsDir)) Directory.CreateDirectory(Extension.AssetsDir);
            if (!File.Exists(Extension.Blob)) File.WriteAllText(Extension.Blob, "");
            string assessment = File.ReadAllText(Extension.Blob);
            if (string.IsNullOrEmpty(assessment)) return new Assessment(AssessmentType.Item60, 75);
            return new Assessment(assessment);
        }

        public static void SetAssessment(Assessment assessment)
        {
            File.WriteAllText(Extension.Blob, assessment.Bloberize());
        }

        public static void ClearData()
        {
            File.WriteAllText(Extension.Blob, "");
        }
    }
}
