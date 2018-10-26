using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// An interface to create a word
    /// </summary>
    interface IWordBuffer : IEnumerable<Letter>
    {
        /// <summary>
        /// The length of the word
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Adds letter at the end of the word
        /// </summary>
        /// <param name="lett"></param>
        /// <returns></returns>
        bool AddLast(Letter lett);

        /// <summary>
        /// Removes letter
        /// </summary>
        /// <param name="lett"></param>
        /// <returns></returns>
        bool Remove(Letter lett);

        IWordBuffer Clone();
    }
}
