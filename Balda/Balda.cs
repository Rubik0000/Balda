using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class Balda
    {
        static private readonly char EMPTY = '\0';

        private TrieTree _commonTree;
        private TrieTree _invertedPrefixTree;
        private int _fieldSize;
        private char[,] _grid;
        private bool[,] _visited;
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

        private void GetPossiblewWords_Rec(
            char[,] grid, 
            int size,
            int currStep,
            int maxStep,
            ISet<string> lvl)
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
                        if (Find(grid, size, node, i, j, lvl))
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
            {
                return;
            }
            foreach(var gr in goodGrids)
            {
                GetPossiblewWords_Rec(gr, size, currStep + 1, maxStep, lvl);
            }
        }

        private bool Find(
            char[,] grid, 
            int size, 
            TrieTree tree,
            int x,
            int y,
            ICollection<string> coll)
        {
            bool wasAdded = false;
            if (tree.Word != null)
            {
                if (_commonTree.Find(tree.Word))
                {
                    coll.Add(tree.Word);
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
                        wasAdded = wasAdded || Find(grid, size, t, x, y, coll);
                }
            }

            if (x - 1 >= 0 && grid[x - 1, y] != EMPTY && !_visited[x - 1, y])
            {
                TrieTree node = tree.GetNodeByLett(grid[x - 1, y]);
                if (node != null)
                {
                    _visited[x - 1, y] = true;
                    wasAdded = wasAdded || Find(grid, size, node, x - 1, y, coll);
                    _visited[x - 1, y] = false;
                }
            }
            if (x + 1 < size && grid[x + 1, y] != EMPTY && !_visited[x + 1, y])
            {
                TrieTree node = tree.GetNodeByLett(grid[x + 1, y]);
                if (node != null)
                {
                    _visited[x + 1, y] = true;
                    wasAdded = wasAdded || Find(grid, size, node, x + 1, y, coll);
                    _visited[x + 1, y] = false;
                }
            }
            if (y - 1 >= 0 && grid[x, y - 1] != EMPTY && !_visited[x, y - 1])
            {
                TrieTree node = tree.GetNodeByLett(grid[x, y - 1]);
                if (node != null)
                {
                    _visited[x, y - 1] = true;
                    wasAdded = wasAdded || Find(grid, size, node, x, y - 1, coll);
                    _visited[x, y - 1] = false;
                }
            }
            if (y + 1 < size && grid[x, y + 1] != EMPTY && !_visited[x, y + 1])
            {
                TrieTree node = tree.GetNodeByLett(grid[x, y + 1]);
                if (node != null)
                {
                    _visited[x, y + 1] = true;
                    wasAdded = wasAdded || Find(grid, size, node, x, y + 1, coll);
                    _visited[x, y + 1] = false;
                }
            }
            return wasAdded;
        }

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

        public ICollection<string> FindWords(int step)
        {
            ISet<string> words = new HashSet<string>();
            GetPossiblewWords_Rec(_grid, _fieldSize, 1, step, words);
            return words;
        }

        public void SetWord(string word)
        {
            int row = _fieldSize / 2;
            for (int i = 0; i < _fieldSize; ++i)
                _grid[row, i] = word[i];
        }

        public void FeelDictionary(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                _commonTree.Clear();
                _invertedPrefixTree.Clear();
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine().Replace("\n", "");
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
