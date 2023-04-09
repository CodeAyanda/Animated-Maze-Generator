using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Maze_Generator
{
    public class Cell
    {
        public int i, j;
        int size = Form1.size;
        int numCells = Form1.numCells;
        bool[] walls = new bool[4];
        public bool visited;

        public Cell(int i, int j)
        {
            this.i = i;
            this.j = j;
            this.walls = new []{ true, true, true, true};
            this.visited = false;
        }

        public Cell EvaluateTop(Cell[,] array, int i, int j)
        {

            if(j-1 < 0)
            {
                return null;
            }
            return array[i, j-1];

        }
        public Cell EvaluateRight(Cell[,] array, int i, int j)
        {

            if (i + 1 >= Form1.numCells)
            {
                return null;
            }
            return array[i + 1, j];

        }

        public Cell EvaluateBottom(Cell[,] array, int i, int j)
        {

            if (j + 1 >= Form1.numCells)
            {
                return null;
            }
            return array[i, j + 1];

        }
        public Cell EvaluateLeft(Cell[,] array, int i, int j)
        {

            if (i - 1 < 0)
            {
                return null;
            }
            return array[i - 1, j];

        }

        public Cell CheckNeighbours()
        {
            List<Cell> neighbours = new List<Cell>();

            var top = EvaluateTop(Form1.cells, i, j);
            var right = EvaluateRight(Form1.cells, i, j);
            var bottom = EvaluateBottom(Form1.cells, i, j);
            var left = EvaluateLeft(Form1.cells, i, j);

            if (top != null && !top.visited)
            {
                neighbours.Add(top);
            }
            if (right != null && !right.visited)
            {
                neighbours.Add(right);
            }
            if (bottom != null && !bottom.visited)
            {
                neighbours.Add(bottom);
            }
            if (left != null && !left.visited)
            {
                neighbours.Add(left);
            }

            if (neighbours.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, neighbours.Count);

                return neighbours[index];

            }
            return null;

        }

        public static void RemoveWalls(Cell current, Cell next)
        {
            if(current.i - next.i < 0)
            {
                current.walls[1] = false;
                next.walls[3] = false;
            }

            if (current.i - next.i > 0)
            {
                current.walls[3] = false;
                next.walls[1] = false;
            }

            if (current.j - next.j < 0)
            {
                current.walls[2] = false;
                next.walls[0] = false;
            }

            if (current.j - next.j > 0)
            {
                current.walls[0] = false;
                next.walls[2] = false;
            }




        }

        public void Show(Graphics g)
        {
            Pen black = new Pen(Color.Black, 1);
            int x = i * size;
            int y = j * size;

            if (visited)
            {
                SolidBrush Purple = new SolidBrush(Color.Purple);
                g.FillRectangle(Purple, x, y, size, size);
            }


            //top, right, bottom, left
            if (walls[0] == true)
            {
                g.DrawLine(black, new Point(x, y), new Point(x + size, y));
            }
            if (walls[1] == true)
            {
                g.DrawLine(black, new Point(x+size, y), new Point(x + size, y + size));

            }
            if (walls[2] == true)
            {
                g.DrawLine(black, new Point(x, y+size), new Point(x + size, y + size));

            }
            if (walls[3] == true)
            {
                g.DrawLine(black, new Point(x, y), new Point(x, y + size));

            }
        }
    }
}
