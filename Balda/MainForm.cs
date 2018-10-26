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
            //bool s = d.Contain("Шлих");
            //var wrd = new BaldaWordBuffer();
            //bool s = wrd.AddLast(new Letter('d', 0, 0));
            //s = wrd.AddLast(new Letter('o', 0, 2));
            //s = wrd.AddLast(new Letter('g', 5, 0));
            //s = wrd.AddLast(new Letter('r', 5, 0));
            //string str = wrd.ToString();
            var b = new BaldaGame("камаа", d, RusLetters.Instance);
            b._matr[1, 1] = new Letter('р', 1, 1);
            var s = b.FindWords(1, 1);
            string str = "";
            foreach (char l in RusLetters.Instance)
            {
                str += l;
            }
            str += "";
        }
    }
}
