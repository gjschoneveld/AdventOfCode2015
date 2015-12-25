using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day25
{
    class Cell
    {
        public int row;
        public int column;

        public int number;
        public int code;
    }

    class Program
    {
        static Cell Next(Cell c)
        {
            int newRow;
            int newColumn;
            if (c.row == 1)
            {
                newRow = c.column + 1;
                newColumn = 1;
            }
            else
            {
                newRow = c.row - 1;
                newColumn = c.column + 1;
            }

            int newNumber = c.number + 1;
            int newCode = (int)(c.code * 252533L % 33554393);

            return new Cell
            {
                row = newRow,
                column = newColumn,
                number = newNumber,
                code = newCode
            };
        }

        static void Main(string[] args)
        {
            Cell current = new Cell
            {
                row = 1,
                column = 1,
                number = 1,
                code = 20151125
            };

            int neededRow = 2981;
            int neededColumn = 3075;

            while (current.row != neededRow || current.column != neededColumn)
            {
                current = Next(current);
            }

            Console.WriteLine("Answer 1: {0}", current.code);

            Console.ReadKey();
        }
    }
}
