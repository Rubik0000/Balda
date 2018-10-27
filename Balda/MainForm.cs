using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Balda
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var d = new WordsDictionaryFromFile(@"..\..\..\Dictionary\word_rus.txt", true);            
            var b = new BaldaGame("балда", d, RusLetters.Instance);
            //b._matr[1, 1] = new Letter('р', 1, 1);
            //var s = b.FindWords(1, 1);            
            var ls = b.GetWordsOnStep(2);
            //b.GetWordsOnStepRec(1, 1, ls);
        }
    }
}
