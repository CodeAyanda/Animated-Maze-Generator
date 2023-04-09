using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Generator
{
    public partial class Form1 : Form
    {
        public static int numCells = 20;
        public static int size = 30;
        public static Cell[,] cells = new Cell[numCells,numCells];
        Cell current;
        Cell next;
        Stack<Cell> visited = new Stack<Cell>();

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    cells[i,j] = new Cell(i, j);
                }
                
            }

            current = cells[0, 0];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            SolidBrush Blue = new SolidBrush(Color.Blue);

            SolidBrush Purple = new SolidBrush(Color.Purple);
            Graphics g = e.Graphics;
            for (int i = 0; i < numCells; i++)
            {
                for (int j = 0; j < numCells; j++)
                {
                    cells[i, j].Show(g);
                }
            }

            current.visited = true;

            next = current.CheckNeighbours();


            if (next != null)
            {
                next.visited = true;
                visited.Push(current);
                //remove walls
                Cell.RemoveWalls(current, next);

                current = next;
            }
            else if(visited.Count > 0)
            {
                current = visited.Pop();

            }

            g.FillRectangle(Blue, current.i*size, current.j * size, size, size);

        }
    }
}
