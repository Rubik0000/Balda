using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class WordBuffComparer : IEqualityComparer<IWordBuffer>
    {
        public bool Equals(IWordBuffer x, IWordBuffer y)
        {
            return x.CompareTo(y) == 0;
        }

        public int GetHashCode(IWordBuffer obj)
        {
            return obj.GetHashCode();
        }
    }

    class BaldaGame : IBaldaGame
    {
        public Letter[,] _matr;
        private IWordsDictionary _dictionary;
        private ILettersSet _lettSet;
        private int _size;

        public BaldaGame(string startWord, IWordsDictionary dict, ILettersSet lettSet)
        {
            _size = startWord.Length;
            _matr = new Letter[_size, _size];
            int midle = _size / 2;
            for (int i = 0; i < _size; ++i)
            {
                for(int j = 0; j < _size; ++j)
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

        static private void FindWordsRec(
            Letter[,] matr, 
            int size,
            int row, 
            int col, 
            IWordBuffer word, 
            ISet<IWordBuffer> listWords,
            IWordsDictionary dictionary)
        {
            if (row < 0 || row >= size || col < 0 || col >= size ||
                matr[row, col].Character == Letter.EmptyCharacter ||
                        matr[row, col].Looked) return;

            var letter = matr[row, col];
            word.AddLast(letter);
            letter.Looked = true;
            
            if (/*!listWords.Any(wrd => wrd.CompareTo(word) == 0)*/
                !listWords.Contains(word, new WordBuffComparer()) 
                && dictionary.Contain(word.ToString()))
            {
                listWords.Add((IWordBuffer)word.Clone());
            }
            //for (int i = row > 0 ? row - 1 : 0; i <= (row < size - 1 ? row + 1 : row); ++i)
            //{
            //    for (int j = col > 0 ? col - 1 : 0; j <= (col < size - 1 ? col + 1 : col); ++j)
            //    {
            //        if (matr[i, j].Character != Letter.EmptyCharacter &&
            //            !matr[i, j].Looked && !IsDiagonal(letter, matr[i, j]))                        
            //        {
            //            FindWordsRec(matr, size, i, j, word, listWords, dictionary);
            //        }
            //    }
            //}
            FindWordsRec(matr, size, row - 1, col, word, listWords, dictionary);
            FindWordsRec(matr, size, row, col + 1, word, listWords, dictionary);
            FindWordsRec(matr, size, row + 1, col, word, listWords, dictionary);
            FindWordsRec(matr, size, row, col - 1, word, listWords, dictionary);

            word.Remove(letter);
            letter.Looked = false;
        }



        /// <summary>
        /// Find all words starting with the letter that has given indexes
        /// </summary>
        /// <param name="row">The row index</param>
        /// <param name="col">The column index</param>
        /// <returns>The list of found words</returns>
        static public IEnumerable<IWordBuffer> FindWords(
            Letter[,] matr,
            int size,
            int row, 
            int col,
            ISet<IWordBuffer> listWord,
            IWordsDictionary dictionary)
        {
            //var listWord = new List<IWordBuffer>();
            FindWordsRec(matr, size, row, col, new BaldaWordBuffer(), listWord, dictionary);
            return listWord;
        }

        static private bool HasNeighbors(Letter[,] matr, int size, Letter lett)
        {
            int r = lett.RowInd;
            int c = lett.ColInd;
            return r - 1 >= 0 && matr[r - 1, c].Character != Letter.EmptyCharacter ||
                c + 1 < size && matr[r, c + 1].Character != Letter.EmptyCharacter ||
                r + 1 < size && matr[r + 1, c].Character != Letter.EmptyCharacter ||
                c - 1 >= 0 && matr[r, c - 1].Character != Letter.EmptyCharacter;
        }

        static private ISet<IWordBuffer> GetPossibleWords(
            Letter[,] matr, 
            int size, 
            ILettersSet lettSet, 
            IWordsDictionary dictionary,
            ISet<IWordBuffer> listWords)
        {            
            //var listWords = new List<IWordBuffer>();            
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (matr[i, j].Character == Letter.EmptyCharacter /*&&
                        HasNeighbors(matr, size, matr[i, j])*/)
                    {
                        foreach (var lett in lettSet)
                        {
                            //_matr[i, j].Character = lett;
                            matr[i, j] = new Letter(lett, matr[i, j].RowInd, matr[i, j].ColInd);
                            FindWords(matr, size, i, j, listWords, dictionary);
                            //_matr[i, j].Character = Letter.EmptyCharacter;
                            matr[i, j] = new Letter(Letter.EmptyCharacter, matr[i, j].RowInd, matr[i, j].ColInd);
                        }
                    }
                }
            }
            return listWords;
        }

        static private Letter[,] SetWordToMatr(Letter[,] matr, IWordBuffer word)
        {
            Letter[,] matrCopy = (Letter[,])matr.Clone();
            foreach(var lett in word)
            {
                matrCopy[lett.RowInd, lett.ColInd] = lett;
            }
            return matrCopy;
        }

        static private void GetWordsOnStepRec(
            Letter[,] matr,
            int size,
            int currStep, 
            int maxStep,
            IWordsDictionary dictionary,
            ILettersSet lettSet,
            ISet<IWordBuffer> listWords)
        {
            if (currStep > maxStep) return;
            var lsWords = GetPossibleWords(matr, size, lettSet, dictionary, listWords);            
            if (currStep + 1 > maxStep) return;

            Console.WriteLine(currStep);
            
            Letter[,] copyMatr = (Letter[,])matr.Clone();
            var curr = new IWordBuffer[listWords.Count];
            listWords.CopyTo(curr, 0);

            foreach (var word in curr)
            {
                GetWordsOnStepRec(
                    SetWordToMatr(matr, word),
                    size,
                    currStep + 1, 
                    maxStep,
                    dictionary,
                    lettSet,
                    listWords);
            }
        }

        public IEnumerable<IWordBuffer> GetWordsOnStep(int maxStep)
        {
            var ls = new HashSet<IWordBuffer>();
            GetWordsOnStepRec(_matr, _size, 1, maxStep, _dictionary, _lettSet, ls);
            return ls;
        }
    }
}
