using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// An interface provides functions to work with words in a dictionary
    /// </summary>
    interface IWordsDictionary
    {
        /// <summary>
        /// Checks whether a dictionary contains a word
        /// </summary>
        /// <param name="word">The word</param>
        /// <returns>True if it contains else - false</returns>
        bool Contain(string word);
    }
}
