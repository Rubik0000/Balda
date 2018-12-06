using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// The balda game
    /// </summary>
    class Balda
    {
        /// <summary>The empty cell of the grid</summary>
        static private readonly char EMPTY = '\0';

        /// <summary>The tree with word from dicionary</summary>
        private TrieTree _commonTree;

        /// <summary>The tree with inverted prefixes of the words</summary>
        private TrieTree _invertedPrefixTree;

        /// <summary>The field size</summary>
        private int _fieldSize;

        /// <summary>The game field</summary>
        private char[,] _grid;

        /// <summary>The flags of visited letters</summary>
        private bool[,] _visited;

        /// <summary>The set of letters</summary>
        private ILettersSet _lettSet;

        public Balda(ILettersSet lettSer, int fieldSize = 5)
        {
            _commonTree = new TrieTree(lettSer);
            _invertedPrefixTree = new TrieTree(lettSer);
            _lettSet = lettSer;
            _fieldSize = fieldSize;
            _grid = GetEmptyGrid(fieldSize);
            _visited = new bool[fieldSize, fieldSize];
        }

        /// <summary>
        /// Gets an empty grid
        /// </summary>
        /// <param name="size">the size of the grid</param>
        /// <returns></returns>
        static private char[,] GetEmptyGrid(int size)
        {
            char[,] grid = new char[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    grid[i, j] = EMPTY;
                }
            }
            return grid;
        }

        /// <summary>
        /// Gets all possible words on current step
        /// </summary>
        /// <param name="grid">The game field</param>
        /// <param name="size">The size of the field</param>
        /// <param name="currStep">The current step</param>
        /// <param name="maxStep">The max step</param>
        /// <param name="words"></param>
        private void GetPossiblewWords_Rec(
            char[,] grid, 
            int size,
            int currStep,
            int maxStep,
            ISet<string>[] words)
        {            
            var goodGrids = new List<char[,]>();
            for (int i = 0; i < _fieldSize; ++i)
            {
                for (int j = 0; j < _fieldSize; ++j)
                {
                    if (!IsAdjacent(grid, size, i, j))
                        continue;
                    
                    foreach (char lett in _lettSet)
                    {
                        grid[i, j] = lett;
                        TrieTree node = _invertedPrefixTree.GetNodeByLett(lett);
                        if (node == null)
                            continue;
                        _visited[i, j] = true;
                        if (FindWords(grid, size, node, i, j, words[currStep - 1]))
                        {
                            char[,] copy = (char[,])grid.Clone();
                            goodGrids.Add(copy);
                        }
                        _visited[i, j] = false;
                        grid[i, j] = EMPTY;
                    }
                }
            }
            if (currStep + 1 > maxStep)            
                return;
            
            foreach(var gr in goodGrids)
            {
                GetPossiblewWords_Rec(gr, size, currStep + 1, maxStep, words);
            }
        }

        private bool FindWords(
            char[,] grid,
            int size,
            TrieTree tree,            
            int startX,
            int startY,
            ICollection<string> coll)
        {
            return FindWords(grid, size, tree, startX, startY, startX, startY, coll);
        }

        /// <summary>
        /// Finds possible words
        /// </summary>
        /// <param name="grid">The game field</param>
        /// <param name="size">The size of the field</param>
        /// <param name="tree">The subtree from which start is doing</param>
        /// <param name="x">Current row index</param>
        /// <param name="y">Current column index</param>
        /// <param name="startX">The row index of placed</param>
        /// <param name="startY">The column index of placed</param>
        /// <param name="words"></param>
        /// <returns>True if any word was found</returns>
        private bool FindWords(
            char[,] grid, 
            int size, 
            TrieTree tree,
            int x,
            int y,
            int startX,
            int startY,
            ICollection<string> words)
        {
            bool wasAdded = false;
            if (tree.Word != null)
            {
                if (_commonTree.Find(tree.Word))
                {
                    words.Add(tree.Word);
                    wasAdded = true;
                }

                if (_invertedPrefixTree.Find(tree.Word))
                {
                    TrieTree t = _commonTree; 
                    for (int i = tree.Word.Length - 1; i >= 0; --i)
                    {
                        t = t.GetNodeByLett(tree.Word[i]);
                        if (t == null)
                            break;
                    }
                    if (t != null)
                        wasAdded = wasAdded ||
                            FindWords(grid, size, t, startX, startY, startX, startY, words);
                }
            }

            if (x - 1 >= 0 && grid[x - 1, y] != EMPTY && !_visited[x - 1, y])
            {
                TrieTree node = tree.GetNodeByLett(grid[x - 1, y]);
                if (node != null)
                {
                    _visited[x - 1, y] = true;
                    wasAdded = wasAdded || 
                        FindWords(grid, size, node, x - 1, y, startX, startY, words);
                    _visited[x - 1, y] = false;
                }
            }
            if (x + 1 < size && grid[x + 1, y] != EMPTY && !_visited[x + 1, y])
            {
                TrieTree node = tree.GetNodeByLett(grid[x + 1, y]);
                if (node != null)
                {
                    _visited[x + 1, y] = true;
                    wasAdded = wasAdded || 
                        FindWords(grid, size, node, x + 1, y, startX, startY, words);
                    _visited[x + 1, y] = false;
                }
            }
            if (y - 1 >= 0 && grid[x, y - 1] != EMPTY && !_visited[x, y - 1])
            {
                TrieTree node = tree.GetNodeByLett(grid[x, y - 1]);
                if (node != null)
                {
                    _visited[x, y - 1] = true;
                    wasAdded = wasAdded || 
                        FindWords(grid, size, node, x, y - 1, startX, startY, words);
                    _visited[x, y - 1] = false;
                }
            }
            if (y + 1 < size && grid[x, y + 1] != EMPTY && !_visited[x, y + 1])
            {
                TrieTree node = tree.GetNodeByLett(grid[x, y + 1]);
                if (node != null)
                {
                    _visited[x, y + 1] = true;
                    wasAdded = wasAdded || 
                        FindWords(grid, size, node, x, y + 1, startX, startY, words);
                    _visited[x, y + 1] = false;
                }
            }
            return wasAdded;
        }

        /// <summary>
        /// Checks whether a cell is adjacent
        /// </summary>
        /// <param name="grid">The game field</param>
        /// <param name="size">The size of the field</param>
        /// <param name="i">The row index of the cell</param>
        /// <param name="j">The column index of the cell</param>
        /// <returns></returns>
        private bool IsAdjacent(char[,] grid, int size, int i, int j)
        {
            if (grid[i, j] != EMPTY)
                return false;

            bool res = false;
            if (i - 1 >= 0)
                res = _lettSet.GetIndex(grid[i - 1, j]) != -1;
            if (!res && i + 1 < size)
                res = _lettSet.GetIndex(grid[i + 1, j]) != -1;
            if (!res && j - 1 >= 0)
                res = _lettSet.GetIndex(grid[i, j - 1]) != -1;
            if (!res && j + 1 < size)
                res = _lettSet.GetIndex(grid[i, j + 1]) != -1;
            return res;
        }

        /// <summary>
        /// Gets all possible words on given step including
        /// </summary>
        /// <param name="step"></param>
        /// <returns>
        /// An array of found words by levels where index is a level - 1
        /// </returns>
        public ICollection<string>[] FindWords(int step)
        {
            ISet<string>[] wordsBylbl = new ISet<string>[step];
            for (int i = 0; i < step; ++i)
            {
                wordsBylbl[i] = new HashSet<string>();
            }
            GetPossiblewWords_Rec(_grid, _fieldSize, 1, step, wordsBylbl);
            return wordsBylbl;
        }

        /// <summary>
        /// Sets start word
        /// </summary>
        /// <param name="word"></param>
        public void SetWord(string word)
        {
            int row = _fieldSize / 2;
            for (int i = 0; i < _fieldSize; ++i)
                _grid[row, i] = word[i];
        }

        /// <summary>
        /// Loads words from a file
        /// </summary>
        /// <param name="fileName">The file with words</param>
        public void FeelDictionary(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                _commonTree.Clear();
                _invertedPrefixTree.Clear();
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine().Replace("\n", "");
                    if (word.Length < 3)
                        continue;

                    _commonTree.Add(word);
                    for (int i = 0; i < word.Length; ++i)
                    {
                        string subStr = word.Substring(0, word.Length - i);
                        string reversed = new string(subStr.Reverse().ToArray());
                        _invertedPrefixTree.Add(reversed);
                    }
                }
            }
        }
    }
}
