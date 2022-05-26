using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;

namespace examen
{
    class Cell: TextBox
    {
        public int Row { get; set; }
        public int Collumn { get; set; }
        public int Value { get; set; }
        public Cell(int row, int collumn, int value)
        {
            Row = row;
            Collumn = collumn;
            Value = value;
        }
    }
}
