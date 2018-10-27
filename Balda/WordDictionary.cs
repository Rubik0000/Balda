using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Balda
{
    /// <summary>
    /// Provides a dictionary form a text file
    /// </summary>
    class WordsDictionaryFromFile : IWordsDictionary
    {
        /// <summary>A list of words</summary>
        //private List<string> _dict;
        private HashSet<string> _dict;
        /// <summary>
        /// Construtor creates a dictionary from a text file
        /// set 'isSorted' to true if you are sure that the file is sorted
        /// </summary>
        /// <param name="filePath">A file with words</param>
        /// <param name="isSorted">Whether file is sorted</param>
        public WordsDictionaryFromFile(string filePath, bool isSorted = false)
        {
            var words = GetFileText(filePath);
            _dict = new HashSet<string>(words);
            //if (!isSorted) _dict.Sort();
        }

        /// <summary>
        /// Gets file internals by line
        /// </summary>
        /// <param name="filePath">The file</param>
        /// <returns>A collection of file strings</returns>
        private IEnumerable<string> GetFileText(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var words = reader.ReadToEnd().Split(
                    new string[] { Environment.NewLine }, 
                    StringSplitOptions.RemoveEmptyEntries);
                return words;
            }
        }

        /// <summary>
        /// <see cref="IWordsDictionary.Contain(string)"/>
        /// </summary>        
        public bool Contain(string word) =>
            //_dict.BinarySearch(word.ToLower()) >= 0;       
            _dict.Contains(word.ToLower());
    }
}
