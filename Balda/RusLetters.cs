﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class RusLetters : ILettersSet
    {
        static private int count = 32;
        static private char[] _rusLetts = new char[count];

        static public RusLetters Instance { get; private set; } = new RusLetters();

        protected RusLetters()
        {
            int i = 0;
            for (char ch = 'а'; ch <= 'я'; ++ch)
            {
                _rusLetts[i++] = ch;
            }
        }

        public int Count { get; private set; } = count;

        public char this[int ind]
        {
            get => _rusLetts[ind];
        }

        public IEnumerator<char> GetEnumerator()
        {
            foreach(char let in _rusLetts)
            {
                yield return let;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
