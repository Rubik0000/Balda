using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// A trie tree class
    /// </summary>
    class TrieTree
    {
        /// <summary>The set of letters</summary>
        private ILettersSet _lettSet;

        /// <summary>The subtrees</summary>
        private TrieTree[] _next;

        /// <summary>The held word</summary>
        public String Word { get; private set; } = null;
        
        public TrieTree(ILettersSet set)
        {
            _lettSet = set;
            _next = new TrieTree[set.Count];
        }

        /// <summary>
        /// Checks whether a given word is correct
        /// </summary>
        /// <param name="word">The word it needs to check</param>
        /// <returns>true if the word is correct</returns>
        private bool CorrectWord(string word)
        {
            foreach (var ch in word)
            {
                if (_lettSet.GetIndex(ch) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds a given word into the tree starting at given index
        /// </summary>
        /// <param name="word"></param>
        /// <param name="ind">Start index</param>
        /// <returns>true if thw word was added successfuly</returns>
        private bool Add(string word, int ind)
        {
            if (word.Length == ind)
            {
                bool res = word == null;
                Word = word;
                return res;
            }
            int indLet = _lettSet.GetIndex(word[ind]);
            if (_next[indLet] == null)
            {
                _next[indLet] = new TrieTree(_lettSet);
            }
            return _next[indLet].Add(word, ind + 1);
        }

        /// <summary>
        /// Finds a given word starting at given index
        /// </summary>
        /// <param name="word"></param>
        /// <param name="ind">Start index</param>
        /// <returns>True if the word exists</returns>
        private bool Find(string word, int ind)
        {
            if (word.Length == ind)
            {
                return Word != null;
            }
            int curr = _lettSet.GetIndex(word[ind]);
            return _next[curr] != null && _next[curr].Find(word, ind + 1);
        }

        /// <summary>
        /// Adds a given word into the tree
        /// </summary>
        /// <param name="word"></param>
        /// <returns>true if thw word was added successfuly</returns>
        public bool Add(string word)
        {
            if (!CorrectWord(word))
            {
                return true;
            }
            return Add(word, 0);
        }

        /// <summary>
        /// Finds a given word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>True if the word exists</returns>
        public bool Find(string word)
        {
            return Find(word, 0);
        }

        /// <summary>
        /// Clears the tree
        /// </summary>
        public void Clear()
        {
            _next = new TrieTree[_lettSet.Count];
        }

        /// <summary>
        /// Gets the subtree that represents given letter
        /// </summary>
        /// <param name="lett"></param>
        /// <returns>The subtree or null</returns>
        public TrieTree GetNodeByLett(char lett)
        {
            int ind = _lettSet.GetIndex(lett);
            return _next[ind];
        }               
    }
}
