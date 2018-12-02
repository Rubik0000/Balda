using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class TrieTree
    {       
        private ILettersSet _lettSet;
        private TrieTree[] _next;
        public String Word { get; private set; } = null;
        
        public TrieTree(ILettersSet set)
        {
            _lettSet = set;
            _next = new TrieTree[set.Count];
        }

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

        public bool Add(string word)
        {
            if (!CorrectWord(word))
            {
                return true;
            }
            return Add(word, 0);
        }

        public void Clear()
        {
            _next = new TrieTree[_lettSet.Count];
        }

        public TrieTree GetNodeByLett(char lett)
        {
            int ind = _lettSet.GetIndex(lett);
            return _next[ind];
        }

        private bool Find(string word, int ind)
        {
            if (word.Length == ind)
            {
                return Word != null;
            }                        
            int curr = _lettSet.GetIndex(word[ind]);
            return _next[curr] != null && _next[curr].Find(word, ind + 1);            
        }

        public bool Find(string word)
        {
            return Find(word, 0);
        }

        public bool CorrectWord(string word)
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
    }
}
