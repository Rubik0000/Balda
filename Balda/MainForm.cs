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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var commonTrie = new TrieTree(RusLetters.Instance);
            var revTrie = new TrieTree(RusLetters.Instance);

            var b = new Balda(RusLetters.Instance, 3);
            b.FeelDictionary(@"..\..\..\Dictionary\word_rus.txt");
            b.SetWord("бал");
            var words = b.FindWords(4);

            //FeelTrees(commonTrie, revTrie, @"..\..\..\Dictionary\word_rus.txt");
            //trie.LoadFromFile(@"..\..\..\Dictionary\word_rus.txt");
            //bool found = commonTrie.Find("буклет");
            var d = new WordsDictionaryFromFile(@"..\..\..\Dictionary\word_rus.txt", true);            
            var b1 = new BaldaGame("балда", d, RusLetters.Instance);
            //b._matr[1, 1] = new Letter('р', 1, 1);
            //var s = b.FindWords(1, 1);            
            var ls = b1.GetWordsOnStep(1);
            //b.GetWordsOnStepRec(1, 1, ls);
        }

        private void FeelTrees(TrieTree normal, TrieTree inverted, string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine().Replace("\n", "");
                    normal.Add(word);
                    for (int i = 0; i < word.Length; ++i)
                    {
                        string subStr = word.Substring(0, word.Length - i);
                        string reversed = new string(subStr.Reverse().ToArray());
                        inverted.Add(reversed);
                    }
                }
            }
        }
    }
}
