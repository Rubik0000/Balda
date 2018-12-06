using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// Represents an set of letters
    /// </summary>
    interface ILettersSet : IEnumerable<char>
    {
        /// <summary>
        /// Gets the letter by its order number
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        char this[int ind] { get; }

        /// <summary>
        /// The count letters in the set
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the order number of the letter
        /// </summary>
        /// <param name="lett"></param>
        /// <returns></returns>
        int GetIndex(char lett);
    }
}
