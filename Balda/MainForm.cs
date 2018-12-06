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

namespace Balda
{
    public partial class MainForm : Form
    {
        /// <summary>The file with dictionary</summary>
        private String fileName = @"..\..\..\Dictionary\word_rus.txt";
       
        private Balda _balda;

        public MainForm()
        {
            InitializeComponent();
            _balda = new Balda(RusLetters.Instance);
            _balda.FeelDictionary(fileName);
        }

        /// <summary>
        /// Event handler on find words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {            
            string initialWord =
                txtBxLett1.Text +
                txtBxLett2.Text +
                txtBxLett3.Text +
                txtBxLett4.Text +
                txtBxLett5.Text;
            _balda.SetWord(initialWord.ToLower());
            int step = Convert.ToInt32(nmrcStep.Value);
            var wordsByLvl = _balda.FindWords(step);
            var str = new StringBuilder();
            int count = 1;
            foreach(var words in wordsByLvl)
            {
                str.Append("Шаг: " + count++ + "\r\n");
                str.Append("---------------------------------\r\n");
                foreach (var wrd in words)
                {
                    str.Append(wrd + "\r\n");
                }
            }
            txtBxFoundWords.Text = str.ToString();
        }

        /// <summary>
        /// Event handler on text change in textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBx_TextChanged(object sender, EventArgs e)
        {
            var txtBx = (TextBox)sender;
            if (txtBx.Text.Length > 1)
            {
                txtBx.Text = txtBx.Text[0].ToString();
            }
        }
    }
}
