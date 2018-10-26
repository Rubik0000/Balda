using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class BaldaGame : IBaldaGame
    {
        public Letter[,] _matr;
        private IWordsDictionary _dictionary;
        private ILettersSet _lettSet;
        private int _len;

        public BaldaGame(string startWord, IWordsDictionary dict, ILettersSet lettSet)
        {
            _len = startWord.Length;
            _matr = new Letter[_len, _len];
            int midle = _len / 2;
            for (int i = 0; i < _len; ++i)
            {
                for(int j = 0; j < _len; ++j)
                {
                    if (i == midle)
                    {
                        _matr[i, j] = new Letter(startWord[j], i, j);
                    }
                    else
                    {
                        _matr[i, j] = new Letter(Letter.EmptyCharacter, i, j);
                    }
                }
            }
            _dictionary = dict;
            _lettSet = lettSet;
        }

        /// <summary>
        /// Checks whether the given letters lays on the diagonal
        /// </summary>
        /// <param name="source"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        static private bool IsDiagonal(Letter source, Letter check)
        {
            bool angleCol = check.ColInd == source.ColInd - 1 ||
                check.ColInd == source.ColInd + 1;
            return check.RowInd == source.RowInd - 1 && angleCol ||
                check.RowInd == source.RowInd + 1 && angleCol;
        }

        private void FindWordsRec(
            int row, 
            int col, 
            IWordBuffer word, 
            IList<IWordBuffer> listWords)
        {
            var letter = _matr[row, col];
            word.AddLast(letter);
            letter.Looked = true;
            if (_dictionary.Contain(word.ToString()))
            {
                listWords.Add(word.Clone());
            }
            for (int i = row > 0 ? row - 1 : 0; i <= (row < _len - 1 ? row + 1 : row); ++i)
            {
                for (int j = col > 0 ? col - 1 : 0; j <= (col < _len - 1 ? col + 1 : col); ++j)
                {
                    if (_matr[i, j].Character != Letter.EmptyCharacter &&
                        !_matr[i, j].Looked && !IsDiagonal(letter, _matr[i, j]))                        
                    {
                        FindWordsRec(i, j, word, listWords);
                    }
                }
            }
            word.Remove(letter);
            letter.Looked = false;
        }

        /// <summary>
        /// Find all words starting with the letter that has given indexes
        /// </summary>
        /// <param name="row">The row index</param>
        /// <param name="col">The column index</param>
        /// <returns>The list of found words</returns>
        public IEnumerable<IWordBuffer> FindWords(int row, int col)
        {
            var listWord = new List<IWordBuffer>();
            FindWordsRec(row, col, new BaldaWordBuffer(), listWord);
            return listWord;
        }
    }
}
