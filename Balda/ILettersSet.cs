using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    interface ILettersSet : IEnumerable<char>
    {
        char this[int ind] { get; }

        int Count { get; }
    }
}
