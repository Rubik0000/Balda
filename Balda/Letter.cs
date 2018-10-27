using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balda
{
    class Letter : ICloneable
    {
        static public char EmptyCharacter { get; private set; } = '\0';

        public char Character { get; private set; }

        public int RowInd { get; private set; }

        public int ColInd { get; private set; }

        public bool Looked { get; set; } = false;

        public Letter(char character, int row, int col)
        {
            Character = character;
            RowInd = row;
            ColInd = col;
        }

        public bool HasSameIndexes(Letter let) =>
            this.RowInd == let.RowInd && this.ColInd == let.ColInd;

        public object Clone() =>
            new Letter(this.Character, this.RowInd, this.ColInd);
    }
}
