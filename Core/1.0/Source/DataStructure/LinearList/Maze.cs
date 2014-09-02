using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.DataStructure.LinearList
{
    public class Maze
    {
        public enum Direction
        {
            Up = 0, Down, Left, Right
        }
        public class Cell
        {
            public Cell()
                : this(0, 0)
            { }
            public Cell(int x, int y)
            {
                this.X = x; this.Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                if (obj != null)
                {
                    Cell c = obj as Cell;
                    return this.X == c.X && this.Y == c.Y;
                }
                return false;
            }
        }

        private bool[,] map;
        public int Width { get; set; }
        public int Height { get; set; }

        public Maze(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.map = new bool[width, height];
        }

        public bool FindWay()
        {
            Stack<Cell> way = new Stack<Cell>();
            way.Push(Entrance);
            Cell c = way.Peek();
            Direction dir = Direction.Up;
            bool pass = false;
            while (way.Count > 0 && c != Exit)
            {
                dir = Direction.Up;
                pass = false;
                while ((int)dir < 4)
                {
                    int x, y;
                    if ((int)dir / 2 == 0)
                    {
                        x = 0;
                        y = (int)dir % 2 == 0 ? -1 : 1;
                    }
                    else
                    {
                        y = 0;
                        x = (int)dir % 2 == 0 ? -1 : 1;
                    }
                    if (!IsPass(c.X + x, c.Y + y) || way.Contains(new Cell(c.X + x, c.Y + y)))
                    {
                        dir = (Direction)((int)dir + 1);
                        continue;
                    }
                    c = new Cell(c.X + x, c.Y + y);
                    way.Push(c);
                    pass = true;
                    break;
                }
                if (!pass)
                {
                    way.Pop();
                }
            }
            return c == Exit;
        }

        private Cell[] ways;

        public Cell[] Ways
        {
            get { return ways; }
            set
            {
                ways = value;
                if (value != null && value.Length > 0)
                {
                    Entrance = value[0];
                    Exit = value[value.Length - 1];
                    foreach (Cell c in ways)
                    {
                        map[c.X, c.Y] = true;
                    }
                }
            }
        }

        public Cell Entrance { get; set; }
        public Cell Exit { get; set; }

        public bool IsPass(int x, int y)
        {
            if (x < 0 || x >= map.GetLength(0) ||
                y < 0 || y >= map.GetLength(1))
            {
                return false;
            }
            return map[x, y];
        }
    }
}
