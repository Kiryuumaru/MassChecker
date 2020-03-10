using MassChecker.Models;
using MassChecker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassChecker.Forms
{
    public partial class AnswerKeyItem60 : Form
    {
        public AssessmentSet assessmentSet;
        public Assessment assessment;

        public AnswerKeyItem60()
        {
            InitializeComponent();
            assessment = PartialDB.GetAssessment(AssessmentType.Item60);
            comboBoxSet.SelectedIndex = 0;
            numericUpDownPassingRate.Value = (decimal)assessment.PassingRate;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (assessmentSet == AssessmentSet.SetB) assessment.SetBEnabled = checkBoxEnabled.Checked;
            else if (assessmentSet == AssessmentSet.SetC) assessment.SetCEnabled = checkBoxEnabled.Checked;
            AssessmentItem[] assessmentItems = Assessment.CreateItems(assessment.AssessmentType);
            assessmentItems[0] = new AssessmentItem(keyHolder1.Get());
            assessmentItems[1] = new AssessmentItem(keyHolder2.Get());
            assessmentItems[2] = new AssessmentItem(keyHolder3.Get());
            assessmentItems[3] = new AssessmentItem(keyHolder4.Get());
            assessmentItems[4] = new AssessmentItem(keyHolder5.Get());
            assessmentItems[5] = new AssessmentItem(keyHolder6.Get());
            assessmentItems[6] = new AssessmentItem(keyHolder7.Get());
            assessmentItems[7] = new AssessmentItem(keyHolder8.Get());
            assessmentItems[8] = new AssessmentItem(keyHolder9.Get());
            assessmentItems[9] = new AssessmentItem(keyHolder10.Get());
            assessmentItems[10] = new AssessmentItem(keyHolder11.Get());
            assessmentItems[11] = new AssessmentItem(keyHolder12.Get());
            assessmentItems[12] = new AssessmentItem(keyHolder13.Get());
            assessmentItems[13] = new AssessmentItem(keyHolder14.Get());
            assessmentItems[14] = new AssessmentItem(keyHolder15.Get());
            assessmentItems[15] = new AssessmentItem(keyHolder16.Get());
            assessmentItems[16] = new AssessmentItem(keyHolder17.Get());
            assessmentItems[17] = new AssessmentItem(keyHolder18.Get());
            assessmentItems[18] = new AssessmentItem(keyHolder19.Get());
            assessmentItems[19] = new AssessmentItem(keyHolder20.Get());
            assessmentItems[20] = new AssessmentItem(keyHolder21.Get());
            assessmentItems[21] = new AssessmentItem(keyHolder22.Get());
            assessmentItems[22] = new AssessmentItem(keyHolder23.Get());
            assessmentItems[23] = new AssessmentItem(keyHolder24.Get());
            assessmentItems[24] = new AssessmentItem(keyHolder25.Get());
            assessmentItems[25] = new AssessmentItem(keyHolder26.Get());
            assessmentItems[26] = new AssessmentItem(keyHolder27.Get());
            assessmentItems[27] = new AssessmentItem(keyHolder28.Get());
            assessmentItems[28] = new AssessmentItem(keyHolder29.Get());
            assessmentItems[29] = new AssessmentItem(keyHolder30.Get());
            assessmentItems[30] = new AssessmentItem(keyHolder31.Get());
            assessmentItems[31] = new AssessmentItem(keyHolder32.Get());
            assessmentItems[32] = new AssessmentItem(keyHolder33.Get());
            assessmentItems[33] = new AssessmentItem(keyHolder34.Get());
            assessmentItems[34] = new AssessmentItem(keyHolder35.Get());
            assessmentItems[35] = new AssessmentItem(keyHolder36.Get());
            assessmentItems[36] = new AssessmentItem(keyHolder37.Get());
            assessmentItems[37] = new AssessmentItem(keyHolder38.Get());
            assessmentItems[38] = new AssessmentItem(keyHolder39.Get());
            assessmentItems[39] = new AssessmentItem(keyHolder40.Get());
            assessmentItems[40] = new AssessmentItem(keyHolder41.Get());
            assessmentItems[41] = new AssessmentItem(keyHolder42.Get());
            assessmentItems[42] = new AssessmentItem(keyHolder43.Get());
            assessmentItems[43] = new AssessmentItem(keyHolder44.Get());
            assessmentItems[44] = new AssessmentItem(keyHolder45.Get());
            assessmentItems[45] = new AssessmentItem(keyHolder46.Get());
            assessmentItems[46] = new AssessmentItem(keyHolder47.Get());
            assessmentItems[47] = new AssessmentItem(keyHolder48.Get());
            assessmentItems[48] = new AssessmentItem(keyHolder49.Get());
            assessmentItems[49] = new AssessmentItem(keyHolder50.Get());
            assessmentItems[50] = new AssessmentItem(keyHolder51.Get());
            assessmentItems[51] = new AssessmentItem(keyHolder52.Get());
            assessmentItems[52] = new AssessmentItem(keyHolder53.Get());
            assessmentItems[53] = new AssessmentItem(keyHolder54.Get());
            assessmentItems[54] = new AssessmentItem(keyHolder55.Get());
            assessmentItems[55] = new AssessmentItem(keyHolder56.Get());
            assessmentItems[56] = new AssessmentItem(keyHolder57.Get());
            assessmentItems[57] = new AssessmentItem(keyHolder58.Get());
            assessmentItems[58] = new AssessmentItem(keyHolder59.Get());
            assessmentItems[59] = new AssessmentItem(keyHolder60.Get());
            assessment.SetItems(assessmentItems, assessmentSet);
            assessment.PassingRate = (double)numericUpDownPassingRate.Value;
            PartialDB.SetAssessment(assessment);
            MessageBox.Show("Answer key save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ComboBoxSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxSet.SelectedIndex)
            {
                case 0:
                    assessmentSet = AssessmentSet.SetA;
                    checkBoxEnabled.Checked = true;
                    checkBoxEnabled.Enabled = false;
                    break;
                case 1:
                    assessmentSet = AssessmentSet.SetB;
                    checkBoxEnabled.Checked = assessment.SetBEnabled;
                    checkBoxEnabled.Enabled = true;
                    break;
                case 2:
                    assessmentSet = AssessmentSet.SetC;
                    checkBoxEnabled.Checked = assessment.SetCEnabled;
                    checkBoxEnabled.Enabled = true;
                    break;
            }
            keyHolder1.Set(1, assessment.GetItems(assessmentSet)[0].Key);
            keyHolder2.Set(2, assessment.GetItems(assessmentSet)[1].Key);
            keyHolder3.Set(3, assessment.GetItems(assessmentSet)[2].Key);
            keyHolder4.Set(4, assessment.GetItems(assessmentSet)[3].Key);
            keyHolder5.Set(5, assessment.GetItems(assessmentSet)[4].Key);
            keyHolder6.Set(6, assessment.GetItems(assessmentSet)[5].Key);
            keyHolder7.Set(7, assessment.GetItems(assessmentSet)[6].Key);
            keyHolder8.Set(8, assessment.GetItems(assessmentSet)[7].Key);
            keyHolder9.Set(9, assessment.GetItems(assessmentSet)[8].Key);
            keyHolder10.Set(10, assessment.GetItems(assessmentSet)[9].Key);
            keyHolder11.Set(11, assessment.GetItems(assessmentSet)[10].Key);
            keyHolder12.Set(12, assessment.GetItems(assessmentSet)[11].Key);
            keyHolder13.Set(13, assessment.GetItems(assessmentSet)[12].Key);
            keyHolder14.Set(14, assessment.GetItems(assessmentSet)[13].Key);
            keyHolder15.Set(15, assessment.GetItems(assessmentSet)[14].Key);
            keyHolder16.Set(16, assessment.GetItems(assessmentSet)[15].Key);
            keyHolder17.Set(17, assessment.GetItems(assessmentSet)[16].Key);
            keyHolder18.Set(18, assessment.GetItems(assessmentSet)[17].Key);
            keyHolder19.Set(19, assessment.GetItems(assessmentSet)[18].Key);
            keyHolder20.Set(20, assessment.GetItems(assessmentSet)[19].Key);
            keyHolder21.Set(21, assessment.GetItems(assessmentSet)[20].Key);
            keyHolder22.Set(22, assessment.GetItems(assessmentSet)[21].Key);
            keyHolder23.Set(23, assessment.GetItems(assessmentSet)[22].Key);
            keyHolder24.Set(24, assessment.GetItems(assessmentSet)[23].Key);
            keyHolder25.Set(25, assessment.GetItems(assessmentSet)[24].Key);
            keyHolder26.Set(26, assessment.GetItems(assessmentSet)[25].Key);
            keyHolder27.Set(27, assessment.GetItems(assessmentSet)[26].Key);
            keyHolder28.Set(28, assessment.GetItems(assessmentSet)[27].Key);
            keyHolder29.Set(29, assessment.GetItems(assessmentSet)[28].Key);
            keyHolder30.Set(30, assessment.GetItems(assessmentSet)[29].Key);
            keyHolder31.Set(31, assessment.GetItems(assessmentSet)[30].Key);
            keyHolder32.Set(32, assessment.GetItems(assessmentSet)[31].Key);
            keyHolder33.Set(33, assessment.GetItems(assessmentSet)[32].Key);
            keyHolder34.Set(34, assessment.GetItems(assessmentSet)[33].Key);
            keyHolder35.Set(35, assessment.GetItems(assessmentSet)[34].Key);
            keyHolder36.Set(36, assessment.GetItems(assessmentSet)[35].Key);
            keyHolder37.Set(37, assessment.GetItems(assessmentSet)[36].Key);
            keyHolder38.Set(38, assessment.GetItems(assessmentSet)[37].Key);
            keyHolder39.Set(39, assessment.GetItems(assessmentSet)[38].Key);
            keyHolder40.Set(40, assessment.GetItems(assessmentSet)[39].Key);
            keyHolder41.Set(41, assessment.GetItems(assessmentSet)[40].Key);
            keyHolder42.Set(42, assessment.GetItems(assessmentSet)[41].Key);
            keyHolder43.Set(43, assessment.GetItems(assessmentSet)[42].Key);
            keyHolder44.Set(44, assessment.GetItems(assessmentSet)[43].Key);
            keyHolder45.Set(45, assessment.GetItems(assessmentSet)[44].Key);
            keyHolder46.Set(46, assessment.GetItems(assessmentSet)[45].Key);
            keyHolder47.Set(47, assessment.GetItems(assessmentSet)[46].Key);
            keyHolder48.Set(48, assessment.GetItems(assessmentSet)[47].Key);
            keyHolder49.Set(49, assessment.GetItems(assessmentSet)[48].Key);
            keyHolder50.Set(50, assessment.GetItems(assessmentSet)[49].Key);
            keyHolder51.Set(51, assessment.GetItems(assessmentSet)[50].Key);
            keyHolder52.Set(52, assessment.GetItems(assessmentSet)[51].Key);
            keyHolder53.Set(53, assessment.GetItems(assessmentSet)[52].Key);
            keyHolder54.Set(54, assessment.GetItems(assessmentSet)[53].Key);
            keyHolder55.Set(55, assessment.GetItems(assessmentSet)[54].Key);
            keyHolder56.Set(56, assessment.GetItems(assessmentSet)[55].Key);
            keyHolder57.Set(57, assessment.GetItems(assessmentSet)[56].Key);
            keyHolder58.Set(58, assessment.GetItems(assessmentSet)[57].Key);
            keyHolder59.Set(59, assessment.GetItems(assessmentSet)[58].Key);
            keyHolder60.Set(60, assessment.GetItems(assessmentSet)[59].Key);
        }
    }
}
