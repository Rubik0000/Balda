using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class Letter
    {
        static public char EmptyCharacter { get; private set; } = '\0';

        public char Character { get; set; }

        public int RowInd { get; set; }

        public int ColInd { get; set; }

        public bool Looked { get; set; } = false;

        public Letter(char character, int row, int col)
        {
            Character = character;
            RowInd = row;
            ColInd = col;
        }

        public bool HasSameIndexes(Letter let) =>
            this.RowInd == let.RowInd && this.ColInd == let.ColInd;       
    }
}
