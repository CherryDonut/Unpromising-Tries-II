using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new GameForm());
        }
    }

    public class WonderBall
    {
        private Point position;
        private int size;
        private double jumpForce;

        public WonderBall(Point position, int size, double jumpForce)
        {
            this.position = position;
            this.size = size;
            this.jumpForce = jumpForce;
        }

        public int Size => size;
        public double JumpForce => jumpForce;

        public Point Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int Y
        {
            get
            {
                return position.Y;
            }

            set
            {
                position.Y = value;
            }
        }

        public int X
        {
            get
            {
                return position.X;
            }

            set
            {
                position.X = value;
            }
        }

    }

    public class Level
    {
        public List<Cell> firstFloor;
        public List<Cell> secondFloor;
        public List<Cell> FirstFloor => firstFloor;
        public List<Cell> SecondFloor => secondFloor;
        
        public Level()
        {
            this.firstFloor = new List<Cell>();
            this.secondFloor = new List<Cell>();
        }

        public Level (List<Cell> firstFloor, List<Cell> secondFloor)
        {
            this.firstFloor = firstFloor;
            this.secondFloor = secondFloor;
        }
    }

    public class Cell
    {
        public bool isBlock;
        public bool isDeath;
        public Brush brush;

        public bool IsBlock => isBlock;
        public bool IsDeath => isDeath;
        public Brush Brush => brush;

        public Cell(bool isBlock, bool isDeath, Brush brush)
        {
            this.isBlock = isBlock;
            this.isDeath = isDeath;
            this.brush = brush;
        }
    }

    public class GameForm : Form
    {
        public int height = 600;
        public int width = 600;
        public int sizeOfBlock = 30;

        public GameForm()
        {
            DoubleBuffered = true;
            Cursor.Hide();
            ClientSize = new Size(height, width);
            var indexOfLevel = 0;
            var gravity = 9.8;
            var speed = 0.0;

            var wonderBall = new WonderBall(new Point(200, 200), sizeOfBlock - 10, 0.9);
            wonderBall.Position = this.PointToClient(new Point(Cursor.Position.X, this.PointToScreen(wonderBall.Position).Y));

            var level = GetRandomLevel(width / sizeOfBlock, 0);

            var time = 0;
            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (sender, args) =>
            {
                time++;
                Invalidate();
            };
            timer.Start();

            Paint += (sender, args) =>
            {
                speed = speed  + (gravity - 0.01 * speed) * 0.1;
                wonderBall.Y = wonderBall.Y + (int)(speed * 0.1 + gravity * 0.01);

                if (wonderBall.Y > 600)
                {
                    wonderBall.Y = 0;
                    indexOfLevel++;
                    level = GetRandomLevel(width / sizeOfBlock, indexOfLevel / 5);
                }
                else if (wonderBall.Y <= 0 && speed < 0)
                {
                    speed = -speed * wonderBall.JumpForce;
                }

                wonderBall.X -= Math.Sign(wonderBall.X - this.PointToClient(Cursor.Position).X) * 2;

                if (wonderBall.X >= width)
                    wonderBall.X -= width;
                else if (wonderBall.X <= 0)
                    wonderBall.X += width;

                if (Math.Abs(height - 100 - wonderBall.Y - sizeOfBlock / 2) < 10 && level.FirstFloor[wonderBall.X / sizeOfBlock].IsBlock)
                {
                    if (level.FirstFloor[wonderBall.X / sizeOfBlock].IsDeath)
                    {
                        timer.Stop();
                        args.Graphics.DrawString("Game Over", new Font("Arial", 80), Brushes.Red, 0, 100);
                    }
                    else if (speed > 0)
                    {
                        speed = -speed * wonderBall.JumpForce;
                    }
                }

                if (Math.Abs(height - 350 - wonderBall.Y - sizeOfBlock / 2) < 10 && level.SecondFloor[wonderBall.X / sizeOfBlock].IsBlock)
                {
                    if (level.SecondFloor[wonderBall.X / sizeOfBlock].IsDeath)
                    {
                        timer.Stop();
                        args.Graphics.DrawString("Game Over", new Font("Arial", 80), Brushes.Red, 0, 100);
                    }
                    else if (speed > 0)
                    {
                        speed = -speed * wonderBall.JumpForce;
                    }
                }

                if (Math.Abs(height - 320 - wonderBall.Y - sizeOfBlock / 2) < 10 && level.SecondFloor[wonderBall.X / sizeOfBlock].IsBlock)
                {
                    if (level.SecondFloor[wonderBall.X / sizeOfBlock].IsDeath)
                    {
                        timer.Stop();
                        args.Graphics.DrawString("Game Over", new Font("Arial", 80), Brushes.Red, 0, 100);
                    }
                    else if (speed < 0)
                    {
                        speed = -speed * wonderBall.JumpForce;
                    }
                }

                var indexOfBlock = 0;
                foreach (var cell in level.FirstFloor)
                {
                    args.Graphics.FillRectangle(cell.Brush, indexOfBlock * sizeOfBlock, height - 100, sizeOfBlock, sizeOfBlock);
                    indexOfBlock++;
                }
                indexOfBlock = 0;
                foreach (var cell in level.SecondFloor)
                {
                    args.Graphics.FillRectangle(cell.Brush, indexOfBlock * sizeOfBlock, height - 350, sizeOfBlock, sizeOfBlock);
                    indexOfBlock++;
                }


                args.Graphics.FillEllipse(Brushes.Red,
                    wonderBall.Position.X - wonderBall.Size / 2,
                    wonderBall.Position.Y,
                    wonderBall.Size,
                    wonderBall.Size);

                args.Graphics.FillEllipse(Brushes.Yellow,
                    this.PointToClient(Cursor.Position).X - wonderBall.Size / 2,
                    wonderBall.Position.Y,
                    wonderBall.Size,
                    wonderBall.Size);

                args.Graphics.DrawString("Scores: " + ((double)(1 + indexOfLevel) / 2 * indexOfLevel).ToString(), new Font("Arial", 20), Brushes.Black, 0, 0);
            };
        }

        public static Level GetRandomLevel(int size, double complexity)
        {
            var level = new Level();
            var rnd = new Random();
            var firstFreeCell = rnd.Next(size);
            var secondFreeCell = rnd.Next(size);

            for (var i = 0; i < size; i++)
            {
                if (i == firstFreeCell)
                {
                    level.FirstFloor.Add(new Cell(false, false, Brushes.Gray));
                }
                else if (rnd.NextDouble() * complexity > 0.9)
                {
                    level.FirstFloor.Add(new Cell(true, true, Brushes.Red));
                }
                else
                {
                    level.FirstFloor.Add(new Cell(true, false, Brushes.Brown));
                }

                if (i == secondFreeCell || rnd.NextDouble() < 0.2)
                {
                    level.SecondFloor.Add(new Cell(false, false, Brushes.Gray));
                }
                else if (rnd.NextDouble() * complexity > 0.9)
                {
                    level.SecondFloor.Add(new Cell(true, true, Brushes.Red));
                }
                else
                {
                    level.SecondFloor.Add(new Cell(true, false, Brushes.Brown));
                }
            }
                    
            return level;
        }

    }
}
