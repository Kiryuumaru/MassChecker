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
    public partial class TypeChooser : Form
    {
        public AssessmentType AssessmentType { get; private set; }
        public bool HasChosen { get; private set; } = false;

        public TypeChooser()
        {
            InitializeComponent();
        }

        private void Button50_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item50;
            HasChosen = true;
            Close();
        }

        private void Button60_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item60;
            HasChosen = true;
            Close();
        }

        private void Button70_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item70;
            HasChosen = true;
            Close();
        }

        private void Button80_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item80;
            HasChosen = true;
            Close();
        }

        private void Button90_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item90;
            HasChosen = true;
            Close();
        }

        private void Button100_Click(object sender, EventArgs e)
        {
            AssessmentType = AssessmentType.Item100;
            HasChosen = true;
            Close();
        }
    }
}
