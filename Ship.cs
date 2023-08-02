using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public enum ShipOrientation
    {
        Horizontales,
        Verticales
    }
    public class Ship
    {
        public int Size { get; private set; }
        public ShipOrientation Orientation { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        private int hits;

        public Ship(int size)
        {
            Size = size;
            Orientation = ShipOrientation.Horizontales;
            Row = -1;
            Col = -1;
            hits = 0;
        }

        public bool IsDestroyed()
        {
            return hits >= Size;
        }

        public bool Hit(int row, int col)
        {
            if (Orientation == ShipOrientation.Horizontales)
            {
                if (row == Row && col >= Col && col < Col + Size)
                {
                    hits++;
                    return true;
                }
            }
            else
            {
                if (col == Col && row >= Row && row < Row + Size)
                {
                    hits++;
                    return true;
                }
            }

            return false;
        }
    }
}
