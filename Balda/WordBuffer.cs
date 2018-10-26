using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    /// <summary>
    /// A buffer for the game 'Balda'
    /// It cannot hold two letter with the same row and column indexes
    /// </summary>
    class BaldaWordBuffer : IWordBuffer
    {
        private List<Letter> _word;

        /// <summary>
        /// <see cref="IWordBuffer.Length"/>
        /// </summary>
        public int Length => _word.Count;

        /// <summary>
        /// Creates an empty buffer
        /// </summary>
        public BaldaWordBuffer()
        {
            _word = new List<Letter>();            
        }

        /// <summary>
        /// Creates a buffer from given collection
        /// </summary>
        /// <param name="chars"></param>
        public BaldaWordBuffer(IEnumerable<Letter> chars)
        {
            _word = new List<Letter>(chars);
        }

        /// <summary>
        /// <see cref="IWordBuffer.AddLast(Letter)"/>
        /// </summary>        
        public bool AddLast(Letter lett)
        {
            foreach(var lt in _word)
            {
                if (lt.HasSameIndexes(lett))
                    return false;
            }
            _word.Add(lett);
            return true;
        }
        
        /// <summary>
        /// <see cref="IWordBuffer.Remove(Letter)"/>
        /// </summary>
        public bool Remove(Letter lett) =>
            _word.Remove(lett);
        

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<Letter> GetEnumerator()
        {
            foreach(var let in _word)
            {
                yield return let;
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder(_word.Count);
            foreach (var let in _word)
            {
                str.Append(let.Character);
            }
            return str.ToString();
        }

        public IWordBuffer Clone() =>
            new BaldaWordBuffer(_word);
    }
}
